using Microsoft.EntityFrameworkCore;
using ragther.entity;

namespace ragther.data.Concrete.EFCore
{
    public class RagtherDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<TodoCondition> TodoConditions { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendshipCondition> FriendshipConditions { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<MailUpdate> MailUpdates { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<NoticeType> NoticeTypes { get; set; }
        public DbSet<ProfileDetail> ProfileDetails { get; set; }
        public DbSet<Remind> Reminds { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagsOfInterest> TagsOfInterests { get; set; }
        public DbSet<TodoTag> TodoTags { get; set; }
        public DbSet<TokenCondition> TokenConditions { get; set; }
        public DbSet<WorkWith> WorkWiths { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder){
            optionBuilder.UseMySql("Server=localhost;Database=ragther;Uid=root;Pwd=123456789;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<Like>()
            .HasKey(c => new {c.TodoId, c.UserId} );
            
            modelBuilder.Entity<Remind>()
            .HasKey(c => new {c.TodoId, c.UserId} );
            
            modelBuilder.Entity<WorkWith>()
            .HasKey(c => new {c.TodoId, c.UserId} );

            modelBuilder.Entity<TagsOfInterest>()
            .HasKey(c => new {c.TagId, c.UserId} );

            modelBuilder.Entity<TodoTag>()
            .HasKey(c => new {c.TodoId, c.TagId} );
        }
    }
}