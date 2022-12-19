using Application.DTOs.Vote;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Core.Repositories;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IPollRepository _pollRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public VoteService(IVoteRepository voteRepository, IPollRepository pollRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _voteRepository = voteRepository;
            _pollRepository = pollRepository;
            _contextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<VoteInfoDTO> Get(int pollId)
        {
            string? userId = GetCurrentUserId();

            Poll poll = await _pollRepository.GetBySpecAsync(new PollSpecification(x => x.Id == pollId));
            if (poll is null)
            {
                throw new ObjectNotFoundException("This poll does not exists");
            }
            if (!poll.IsActive)
            {
                if (poll.StartDate is not null && poll.StartDate > DateTime.Now)
                {
                    throw new AccessForbiddenException("This poll has not started yet");
                }
                throw new AccessForbiddenException("This poll has already ended");
            }
            if (!poll.ResultsArePublic && poll.CreatedBy != userId && !(poll.Moderators?.Any(x => x.UserId == userId) ?? false))
            {
                throw new AccessForbiddenException("You are not allowed to view this poll results");
            }
            List<Vote> votes = new();
            ICollection<VoteSummaryDTO> voteQuestions = new List<VoteSummaryDTO>();
            foreach (var question in poll.Questions)
            {

                var votesInQuestion = await _voteRepository.GetByLambdaAsync(x => x.QuestionId == question.Id);
                votes.AddRange(votesInQuestion);
                if (question.Type == QuestionType.Open)
                {
                    continue;
                }
                var test = new VoteSummaryDTO
                {
                    QuestionId = question.Id,
                    QuestionText = question.Text,
                    QuestionType = question.Type,
                    Answers = votesInQuestion.GroupBy(x => x.Answer).Select(group => new AnswerVotingSummaryDTO { AnswerId = group.Key.Id, AnswerText = group.Key.Text, Count = group.Count() }).ToList()
                };
                voteQuestions.Add(test);
            }

            ICollection<VoteBaseDTO> baseAnswers = _mapper.Map<ICollection<VoteBaseDTO>>(votes);
            return new VoteInfoDTO { BaseAnswers = baseAnswers, VoteQuestions = voteQuestions };
        }

        public async Task VoteAggregate(VoteAggregateDTO votes)
        {
            string? userId = GetCurrentUserId();
            Poll poll = await _pollRepository.GetBySpecAsync(new PollSpecification(x => x.Id == votes.PollId));
            if (poll is null)
            {
                throw new ObjectNotFoundException("This poll does not exists");
            }
            if (!poll.AllowAnonymous && userId is null)
            {
                throw new AccessForbiddenException("You must be logged in to vote in this poll");
            }
            if (!poll.IsActive)
            {
                if (poll.StartDate is not null && poll.StartDate > DateTime.Now)
                {
                    throw new AccessForbiddenException("This poll has not started yet");
                }
                throw new AccessForbiddenException("This poll has already ended");
            }
            if (poll.PollType == PollType.Protected)
            {
                if (votes.VotingToken is null)
                {
                    throw new AccessForbiddenException("For this poll voting token is needed");
                }
                if (!poll.VotingTokens.Any(x => x.Token == votes.VotingToken))
                {
                    throw new ObjectNotFoundException("Provided token is not eligible for this poll");
                }
                userId = null;
            }
            if (userId is not null && (await _voteRepository.GetByLambdaAsync(x => x.UserId == userId && x.Question.PollId == votes.PollId)).Any())
            {
                throw new ObjectAlreadyExistsException("You already voted in this poll");
            }
            foreach (var vote in votes.Votes)
            {
                var question = poll.Questions?.FirstOrDefault(x => x.Id == vote.QuestionId);
                if (question is null)
                {
                    throw new ObjectNotFoundException($"Question with id {vote.QuestionId} does not exists in this poll");
                }
                if (question.Type == QuestionType.Open && vote.AnswerText is null)
                {
                    if (vote.AnswerId is not null)
                    {
                        throw new ObjectValidationException("Open question requires null AnswerId");
                    }
                    throw new ObjectValidationException("Open question requires non null AnswerText");
                }
                else if (question.Type != QuestionType.Open)
                {
                    if (vote.AnswerId is null)
                    {
                        if (vote.AnswerText is not null)
                        {
                            throw new ObjectValidationException("Not open question requires null AnswerText");
                        }
                        throw new ObjectValidationException("Not open question requires non null AnswerId");
                    }
                    var answer = question.Answers?.FirstOrDefault(x => x.Id == vote.AnswerId);
                    if (answer is null)
                    {
                        throw new ObjectNotFoundException($"Answer with id {vote.AnswerId} does not exists in question {vote.QuestionId}");
                    }
                }
                await Vote(vote, userId);
            }

            await _voteRepository.SaveAsync();

            if (poll.PollType == PollType.Protected)
            {
                var token = poll.VotingTokens.First(x => x.Token == votes.VotingToken);
                poll.VotingTokens.Remove(token);
                await _pollRepository.UpdateAsync(poll);
            }
        }
        private async Task Vote(VoteCreateDTO vote, string? userId)
        {
            Vote newVote = _mapper.Map<Vote>(vote);
            newVote.UserId = userId;
            await _voteRepository.AddWithoutSavingAsync(newVote);
        }

        private string? GetCurrentUserId()
        {
            var identity = (ClaimsIdentity?)_contextAccessor.HttpContext?.User.Identity;
            string? userId = null;
            if (identity is not null && identity.IsAuthenticated)
            {
                userId = identity.Claims?.FirstOrDefault(
                    x => x.Type.Contains("nameidentifier")
                    )?.Value;
            }
            return userId;
        }
    }
}
