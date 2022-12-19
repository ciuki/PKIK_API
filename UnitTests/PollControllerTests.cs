using Application.DTOs.Answer;
using Application.DTOs.Poll;
using Application.DTOs.Question;
using Core.Enums;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class PollControllerTests
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public PollControllerTests()
        {
            var _application = new Application();
            _client = _application.CreateClient();
        }

        [Fact]
        public async Task ShouldCreatePoll()
        {
            PollCreateDTO pollCreateDTO = new();
            pollCreateDTO.StartDate = null;
            pollCreateDTO.EndDate = null;
            pollCreateDTO.AllowAnonymous = true;
            pollCreateDTO.ResultsArePublic = true;
            pollCreateDTO.Name = "Test poll";
            pollCreateDTO.PollType = PollType.Public;
            pollCreateDTO.Questions = new List<QuestionCreateDTO>
            {
                new QuestionCreateDTO()
                {
                    Text = "Test question",
                    Answers = new List<AnswerCreateDTO>
                {
                    new AnswerCreateDTO()
                    {
                        Text = "Test answer"
                    }
                }
                }
            };
            var response = await _client.PostAsJsonAsync("/api/poll", pollCreateDTO);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var responseText = await response.Content.ReadAsStringAsync();
            PollBaseDTO poll = JsonConvert.DeserializeObject<PollBaseDTO>(responseText);

            poll.Should().NotBeNull();
            poll.Name.Should().Be(pollCreateDTO.Name);
        }

        [Fact]
        public async Task ShouldGetPoll()
        {
            var response = await _client.GetAsync("/api/poll/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseText = await response.Content.ReadAsStringAsync();
            PollBaseDTO poll = JsonConvert.DeserializeObject<PollBaseDTO>(responseText);

            poll.Should().NotBeNull();
            poll.Id.Should().Be(1);
        }

        [Fact]
        public async Task ShouldGetAllPoll()
        {
            var response = await _client.GetAsync("/api/poll");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseText = await response.Content.ReadAsStringAsync();
            List<PollLiteDTO> polls = JsonConvert.DeserializeObject<List<PollLiteDTO>>(responseText);

            polls.Should().NotBeNull();
            polls.Should().HaveCountGreaterOrEqualTo(50);
        }

    }
}