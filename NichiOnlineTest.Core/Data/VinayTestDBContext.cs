using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NichiOnlineTest.Core.Data
{
    public partial class VinayTestDBContext : DbContext
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public VinayTestDBContext()
        {
        }
        public static void SetHttpContextAccessor(IHttpContextAccessor accessor)
        {
            _httpContextAccessor = accessor;
        }

        public virtual DbSet<NisCategory> NisCategory { get; set; }
        public virtual DbSet<NisQuestionAnswers> NisQuestionAnswers { get; set; }
        public virtual DbSet<NisQuestions> NisQuestions { get; set; }
        public virtual DbSet<NisRoleClaims> NisRoleClaims { get; set; }
        public virtual DbSet<NisRoles> NisRoles { get; set; }
        public virtual DbSet<NisSubcategory> NisSubcategory { get; set; }
        public virtual DbSet<NisUserAnswers> NisUserAnswers { get; set; }
        public virtual DbSet<NisUserClaims> NisUserClaims { get; set; }
        public virtual DbSet<NisUserLogins> NisUserLogins { get; set; }
        public virtual DbSet<NisUserRoles> NisUserRoles { get; set; }
        public virtual DbSet<NisUserTestActivity> NisUserTestActivity { get; set; }
        public virtual DbSet<NisUsers> NisUsers { get; set; }
        public virtual DbSet<NisUsertokens> NisUsertokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=VINAY-STG-SRV;Database=NichiOnlineTestDevelop;user id=sa;password=Admin@123;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<NisCategory>(entity =>
            {
                entity.ToTable("NIS_CATEGORY");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalMarks).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<NisQuestionAnswers>(entity =>
            {
                entity.ToTable("NIS_QUESTION_ANSWERS");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Marks).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.NisQuestionAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NIS_QUESTION_ANSWERS_NIS_QUESTIONS");
            });

            modelBuilder.Entity<NisQuestions>(entity =>
            {
                entity.ToTable("NIS_QUESTIONS");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AnswerImage).HasColumnName("Answer_Image");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.QuestionImage).HasColumnName("Question_Image");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.NisQuestions)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NIS_QUESTIONS_NIS_CATEGORY");

                entity.HasOne(d => d.Subcategory)
                    .WithMany(p => p.NisQuestions)
                    .HasForeignKey(d => d.SubcategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NIS_QUESTIONS_NIS_SUBCATEGORY");
            });

            modelBuilder.Entity<NisRoleClaims>(entity =>
            {
                entity.ToTable("NIS_ROLE_CLAIMS");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.NisRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<NisRoles>(entity =>
            {
                entity.ToTable("NIS_ROLES");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<NisSubcategory>(entity =>
            {
                entity.ToTable("NIS_SUBCATEGORY");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Marks).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.NisSubcategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NIS_SUBCATEGORY_NIS_CATEGORY");
            });

            modelBuilder.Entity<NisUserAnswers>(entity =>
            {
                entity.ToTable("NIS_USER_ANSWERS");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AnswerText).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Question_Answers).HasColumnName("Question_Answers");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.NisUserAnswers)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NIS_USER_ANSWERS_NIS_QUESTIONS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NisUserAnswers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NIS_USER_ANSWERS_NIS_USERS");
            });

            modelBuilder.Entity<NisUserClaims>(entity =>
            {
                entity.ToTable("NIS_USER_CLAIMS");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NisUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<NisUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.ToTable("NIS_USER_LOGINS");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NisUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<NisUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.ToTable("NIS_USER_ROLES");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.NisUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NisUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<NisUserTestActivity>(entity =>
            {
                entity.ToTable("NIS_USER_TEST_ACTIVITY");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EndDateTime).HasColumnType("datetime");

                entity.Property(e => e.RunningDateTime).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NisUserTestActivity)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NIS_USER_TEST_ACTIVITY_NIS_USERS");
            });

            modelBuilder.Entity<NisUsers>(entity =>
            {
                entity.ToTable("NIS_USERS");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(128);

                entity.Property(e => e.LastName).HasMaxLength(128);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<NisUsertokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.ToTable("NIS_USERTOKENS");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.NisUsertokens)
                    .HasForeignKey(d => d.UserId);
            });
        }
    }
}
