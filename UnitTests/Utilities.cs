using Core.Entities;
using Core.Enums;
using Faker;
using Infrastructure.Data;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class Utilities
    {
        public static void InitializeDbForTests(IdentityDbContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.Polls.AddRange(GetSeedingPolls());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(IdentityDbContext db)
        {
            db.Users.RemoveRange(db.Users);
            db.Polls.RemoveRange(db.Polls);
            InitializeDbForTests(db);
        }

        private static List<User> GetSeedingUsers()
        {
            List<User> users = new();
            for (int i = 1; i <= 50; i++)
            {
                var user = new User
                {
                    Id = $"{i}",
                    FirstName = Name.First(),
                    LastName = Name.Last()
                };
                user.Email = Internet.Email($"{user.FirstName} {user.LastName}");
                user.UserName = user.Email;
                users.Add(user);
            }
            return users;
        }

        private static List<Poll> GetSeedingPolls()
        {
            List<Poll> polls = new();
            for (int i = 1; i <= 50; i++)
            {
                var poll = new Poll
                {
                    Id = i,
                    Name = Name.Middle(),
                    CreatedBy = null,
                    AllowAnonymous = true,
                    IsActive = i % 2 == 1,
                    ResultsArePublic = true,
                    PollType = PollType.Public,
                    StartDate = i > 25 ? DateTime.Now.AddDays(-1) : DateTime.Now.AddDays(1),
                    EndDate = i % 2 == 1 ? DateTime.Now.AddDays(-1) : DateTime.Now.AddDays(1),
                    Questions = new List<Question>()
                    {
                        new Question()
                        {
                            Id=i,
                            PollId=i,
                            Text=Country.Name(),
                            Type = QuestionType.Closed,
                            Answers = new List<Answer>()
                            {
                                new Answer()
                                {
                                    Id = i,
                                    QuestionId = i,
                                    Text=Address.City(),
                                }
                            }
                        }
                    }
                };
                polls.Add(poll);
            }
            return polls;
        }

    }

}
