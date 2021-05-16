using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace netcoreapi.Models
{
    public partial class mmthapiContext : DbContext
    {
        public mmthapiContext()
        {
        }

        public mmthapiContext(DbContextOptions<mmthapiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ACode> ACodes { get; set; }
        public virtual DbSet<BCode> BCodes { get; set; }
        public virtual DbSet<CCode> CCodes { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Mobile> Mobiles { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<RoCase> RoCases { get; set; }
        public virtual DbSet<RoMessage> RoMessages { get; set; }
        public virtual DbSet<RoUser> RoUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=phoebe.hms-cloud.com;Database=mmthapi;user id=sa;password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ACode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("a_code");

                entity.Property(e => e.ACode1).HasColumnName("A_CODE");

                entity.Property(e => e.ADesc).HasColumnName("A_DESC");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFIED_BY");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFIED_ON");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(2)
                    .HasColumnName("STATUS_CODE");
            });

            modelBuilder.Entity<BCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("b_code");

                entity.Property(e => e.BCode1).HasColumnName("b_CODE");

                entity.Property(e => e.BDesc).HasColumnName("b_DESC");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFIED_BY");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFIED_ON");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(2)
                    .HasColumnName("STATUS_CODE");
            });

            modelBuilder.Entity<CCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("c_code");

                entity.Property(e => e.CCode1).HasColumnName("C_CODE");

                entity.Property(e => e.CDesc).HasColumnName("C_DESC");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFIED_BY");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFIED_ON");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(2)
                    .HasColumnName("STATUS_CODE");
            });

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("incident");

                entity.Property(e => e.CaseId).HasColumnName("case_id");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("messages");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasColumnName("image_url");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.ProfilePhoto)
                    .HasMaxLength(150)
                    .HasColumnName("profile_photo");

                entity.Property(e => e.SenderId)
                    .HasMaxLength(50)
                    .HasColumnName("sender_id");

                entity.Property(e => e.SenderName)
                    .HasMaxLength(150)
                    .HasColumnName("sender_name");

                entity.Property(e => e.Text)
                    .HasMaxLength(500)
                    .HasColumnName("text");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");
            });

            modelBuilder.Entity<Mobile>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("mobiles");

                entity.Property(e => e.MobileId).HasColumnName("mobile_id");

                entity.Property(e => e.Uid)
                    .HasMaxLength(50)
                    .HasColumnName("uid");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ratings");

                entity.Property(e => e.CaseId).HasColumnName("case_id");

                entity.Property(e => e.Comment)
                    .HasMaxLength(250)
                    .HasColumnName("comment");

                entity.Property(e => e.RatingsId).HasColumnName("ratings_id");

                entity.Property(e => e.Score)
                    .HasColumnType("decimal(2, 0)")
                    .HasColumnName("score");
            });

            modelBuilder.Entity<RoCase>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ro_case");

                entity.Property(e => e.ACode)
                    .HasMaxLength(10)
                    .HasColumnName("A_CODE");

                entity.Property(e => e.BCode)
                    .HasMaxLength(10)
                    .HasColumnName("B_CODE");

                entity.Property(e => e.CCode)
                    .HasMaxLength(10)
                    .HasColumnName("C_CODE");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.CaseID).HasColumnName("CaseID");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFIED_BY");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFIED_ON");

                entity.Property(e => e.Dealer).HasColumnName("Dealer");

                entity.Property(e => e.OutAddress).HasColumnName("OUT_ADDRESS");

                entity.Property(e => e.OutChasno).HasColumnName("OUT_CHASNO");

                entity.Property(e => e.OutCmpcde).HasColumnName("OUT_CMPCDE");

                entity.Property(e => e.OutCusname).HasColumnName("OUT_CUSNAME");

                entity.Property(e => e.OutCustDate).HasColumnName("OUT_CUST_DATE");

                entity.Property(e => e.OutCustype).HasColumnName("OUT_CUSTYPE");

                entity.Property(e => e.OutEngno).HasColumnName("OUT_ENGNO");

                entity.Property(e => e.OutExpiryDate).HasColumnName("OUT_EXPIRY_DATE");

                entity.Property(e => e.OutIdno).HasColumnName("OUT_IDNO");

                entity.Property(e => e.OutKiloLast).HasColumnName("OUT_KILO_LAST");

                entity.Property(e => e.OutLastDate).HasColumnName("OUT_LAST_DATE");

                entity.Property(e => e.OutLicense).HasColumnName("OUT_LICENSE");

                entity.Property(e => e.OutMobile).HasColumnName("OUT_MOBILE");

                entity.Property(e => e.OutModel).HasColumnName("OUT_MODEL");

                entity.Property(e => e.OutOffcde).HasColumnName("OUT_OFFCDE");

                entity.Property(e => e.OutPrdcde).HasColumnName("OUT_PRDCDE");

                entity.Property(e => e.OutProvince).HasColumnName("OUT_PROVINCE");

                entity.Property(e => e.OutRoStatus).HasColumnName("OUT_RO_STATUS");

                entity.Property(e => e.OutRocode).HasColumnName("OUT_ROCODE");

                entity.Property(e => e.OutRodate).HasColumnName("OUT_RODATE");

                entity.Property(e => e.OutRotime).HasColumnName("OUT_ROTIME");

                entity.Property(e => e.OutWarrantyDate).HasColumnName("OUT_WARRANTY_DATE");

                entity.Property(e => e.OutZipcode).HasColumnName("OUT_ZIPCODE");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(6)
                    .HasColumnName("STATUS_CODE");
            });

            modelBuilder.Entity<RoMessage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ro_messages");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(150)
                    .HasColumnName("image_url");

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFIED_BY");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFIED_ON");

                entity.Property(e => e.ProfilePhoto)
                    .HasMaxLength(250)
                    .HasColumnName("profile_photo");

                entity.Property(e => e.SenderId)
                    .HasMaxLength(50)
                    .HasColumnName("sender_id");

                entity.Property(e => e.SenderName)
                    .HasMaxLength(150)
                    .HasColumnName("sender_name");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(2)
                    .HasColumnName("STATUS_CODE");

                entity.Property(e => e.Text)
                    .HasMaxLength(500)
                    .HasColumnName("text");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasColumnName("time");
            });

            modelBuilder.Entity<RoUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ro_user");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .HasColumnName("CREATED_BY");

                entity.Property(e => e.CreatedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("CREATED_ON");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(150)
                    .HasColumnName("first_name");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .HasColumnName("last_name");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .HasColumnName("MODIFIED_BY");

                entity.Property(e => e.ModifiedOn)
                    .HasColumnType("datetime")
                    .HasColumnName("MODIFIED_ON");

                entity.Property(e => e.ProfilePhotoUrl)
                    .HasMaxLength(250)
                    .HasColumnName("profile_photo_url");

                entity.Property(e => e.StatusCode)
                    .HasMaxLength(2)
                    .HasColumnName("STATUS_CODE");

                entity.Property(e => e.Token)
                    .HasMaxLength(50)
                    .HasColumnName("token");

                entity.Property(e => e.UserMail)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("user_mail");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("users");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .HasColumnName("mobile");

                entity.Property(e => e.MobileId).HasColumnName("mobile_id");

                entity.Property(e => e.MobileToken)
                    .HasMaxLength(150)
                    .HasColumnName("mobile_token");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.ProfilePhoTo)
                    .HasMaxLength(150)
                    .HasColumnName("profile_pho_to");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
