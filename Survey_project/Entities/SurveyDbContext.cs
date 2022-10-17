using Microsoft.EntityFrameworkCore;

namespace Survey_project.Entities
{
    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext(DbContextOptions<SurveyDbContext> options)
           : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<PasswordResetToken> PasswordResetToken { get; set; }
        public DbSet<Verification> Verification { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<ImageUrlDetail> ImageUrlDetail { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<UserAnswer> UserAnswer { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("ConnectionStrings:DefaultConnection");
            }
        }
    }
}
