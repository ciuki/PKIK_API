using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class IdentityDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        override public DbSet<User> Users => Set<User>();
        public DbSet<Poll> Polls => Set<Poll>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Vote> Votes => Set<Vote>();
        public DbSet<PollAllowed> PollAllowed => Set<PollAllowed>();
        public DbSet<PollModerator> PollModerators => Set<PollModerator>();
        public DbSet<VotingToken> VotingTokens => Set<VotingToken>();
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PollAllowed>().HasKey(x => new
            {
                x.UserId,
                x.PollId,
            });
            builder.Entity<PollModerator>().HasKey(x => new
            {
                x.UserId,
                x.PollId,
            });
        }
    }
}
