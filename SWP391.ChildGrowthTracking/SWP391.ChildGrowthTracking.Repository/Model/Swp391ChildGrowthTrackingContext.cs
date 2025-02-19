using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SWP391.ChildGrowthTracking.Repository.Model;

public partial class Swp391ChildGrowthTrackingContext : DbContext
{
    public Swp391ChildGrowthTrackingContext()
    {
    }

    public Swp391ChildGrowthTrackingContext(DbContextOptions<Swp391ChildGrowthTrackingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alert> Alerts { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Child> Childs { get; set; }

    public virtual DbSet<ConsultationRequest> ConsultationRequests { get; set; }

    public virtual DbSet<ConsultationResponse> ConsultationResponses { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<GrowthRecord> GrowthRecords { get; set; }

    public virtual DbSet<MembershipPackage> MembershipPackages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<RatingFeedback> RatingFeedbacks { get; set; }

    public virtual DbSet<UserMembership> UserMemberships { get; set; }

    public virtual DbSet<Useraccount> Useraccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=SQL1002.site4now.net;Initial Catalog=db_ab2ea3_swp391;User Id=db_ab2ea3_swp391_admin;Password=sa123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.HasKey(e => e.AlertId).HasName("PK__ALERT__4B8FB03A8B746EA8");

            entity.ToTable("ALERT");

            entity.Property(e => e.AlertId).HasColumnName("alert_id");
            entity.Property(e => e.AlertDate)
                .HasColumnType("datetime")
                .HasColumnName("alert_date");
            entity.Property(e => e.AlertType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("alert_type");
            entity.Property(e => e.ChildId).HasColumnName("child_id");
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false)
                .HasColumnName("is_read");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");

            entity.HasOne(d => d.Child).WithMany(p => p.Alerts)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__ALERT__child_id__5EBF139D");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__BLOG__2975AA2811696720");

            entity.ToTable("BLOG");

            entity.Property(e => e.BlogId).HasColumnName("blog_id");
            entity.Property(e => e.Category)
                .HasMaxLength(1)
                .HasColumnName("category");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("datetime")
                .HasColumnName("modified_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Tags)
                .HasMaxLength(255)
                .HasColumnName("tags");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.ChildId).HasName("PK__CHILDS__015ADC057930BCAB");

            entity.ToTable("CHILDS");

            entity.Property(e => e.ChildId).HasColumnName("child_id");
            entity.Property(e => e.Allergies)
                .HasColumnType("text")
                .HasColumnName("allergies");
            entity.Property(e => e.BirthHeight).HasColumnName("birth_height");
            entity.Property(e => e.BirthWeight).HasColumnName("birth_weight");
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("blood_type");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Relationship)
                .HasMaxLength(1)
                .HasColumnName("relationship");
            entity.Property(e => e.Status)
                .HasColumnType("text")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Children)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CHILDS__user_id__619B8048");

            entity.HasMany(d => d.Records).WithMany(p => p.Children)
                .UsingEntity<Dictionary<string, object>>(
                    "Childrecord",
                    r => r.HasOne<GrowthRecord>().WithMany()
                        .HasForeignKey("RecordId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHILDRECORD_GROWTH_RECORDS"),
                    l => l.HasOne<Child>().WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CHILDRECORD_CHILDS"),
                    j =>
                    {
                        j.HasKey("ChildId", "RecordId").HasName("PK__CHILDREC__CAA62748938F6EA2");
                        j.ToTable("CHILDRECORD");
                        j.IndexerProperty<int>("ChildId").HasColumnName("child_id");
                        j.IndexerProperty<int>("RecordId").HasColumnName("record_id");
                    });
        });

        modelBuilder.Entity<ConsultationRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__CONSULTA__18D3B90F420C4375");

            entity.ToTable("CONSULTATION_REQUEST");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.Attachments)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("attachments");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.ChildId).HasColumnName("child_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("request_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.Urgency)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("urgency");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Child).WithMany(p => p.ConsultationRequests)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__CONSULTAT__child__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.ConsultationRequests)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__CONSULTAT__user___6383C8BA");
        });

        modelBuilder.Entity<ConsultationResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseId).HasName("PK__CONSULTA__EBECD8968CBA7DE5");

            entity.ToTable("CONSULTATION_RESPONSE");

            entity.Property(e => e.ResponseId).HasColumnName("response_id");
            entity.Property(e => e.Attachments)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("attachments");
            entity.Property(e => e.Content)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("content");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("diagnosis");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.ResponseDate)
                .HasColumnType("datetime")
                .HasColumnName("response_date");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ConsultationResponses)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__CONSULTAT__docto__6477ECF3");

            entity.HasOne(d => d.Request).WithMany(p => p.ConsultationResponses)
                .HasForeignKey(d => d.RequestId)
                .HasConstraintName("FK__CONSULTAT__reque__656C112C");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__DOCTOR__F39935646EB79A15");

            entity.ToTable("DOCTOR");

            entity.HasIndex(e => e.PhoneNumber, "UQ__DOCTOR__A1936A6BAE74D6D6").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__DOCTOR__AB6E616434AC6FFC").IsUnique();

            entity.HasIndex(e => e.UserId, "UQ__DOCTOR__B9BE370E6B617718").IsUnique();

            entity.HasIndex(e => e.LicenseNumber, "UQ__DOCTOR__D482A003C6474315").IsUnique();

            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.Biography)
                .HasColumnType("text")
                .HasColumnName("biography");
            entity.Property(e => e.Degree)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("degree");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Hospital)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hospital");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("license_number");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.Specialization)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("specialization");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithOne(p => p.Doctor)
                .HasForeignKey<Doctor>(d => d.UserId)
                .HasConstraintName("FK_DOCTOR_USERACCOUNT");
        });

        modelBuilder.Entity<GrowthRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__GROWTH_R__BFCFB4DD29A90086");

            entity.ToTable("GROWTH_RECORDS");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.Bmi).HasColumnName("bmi");
            entity.Property(e => e.HeadCircumference).HasColumnName("head_circumference");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Month)
                .HasColumnType("datetime")
                .HasColumnName("month");
            entity.Property(e => e.Notes)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("notes");
            entity.Property(e => e.Old).HasColumnName("old");
            entity.Property(e => e.RecordedByUser).HasColumnName("recorded_by_user");
            entity.Property(e => e.UpperArmCircumference).HasColumnName("upper_arm_circumference");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<MembershipPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__MEMBERSH__63846AE8AA6D5DF2");

            entity.ToTable("MEMBERSHIP_PACKAGES");

            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DurationMonths).HasColumnName("duration_months");
            entity.Property(e => e.Features)
                .HasColumnType("text")
                .HasColumnName("features");
            entity.Property(e => e.MaxChildrenAllowed).HasColumnName("max_children_allowed");
            entity.Property(e => e.PackageName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("package_name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__PAYMENT__ED1FC9EAD5389578");

            entity.ToTable("PAYMENT");

            entity.HasIndex(e => e.TransactionId, "UQ__PAYMENT__85C600AE67243688").IsUnique();

            entity.HasIndex(e => e.Membershipid, "UQ__PAYMENT__86AB370E37898AC5").IsUnique();

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Membershipid).HasColumnName("membershipid");
            entity.Property(e => e.PaymentAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("payment_amount");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("success")
                .HasColumnName("status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Membership).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.Membershipid)
                .HasConstraintName("FK_PAYMENT_USER_MEMBERSHIP");
        });

        modelBuilder.Entity<RatingFeedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__RATING_F__7A6B2B8C902C0665");

            entity.ToTable("RATING_FEEDBACK");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.Comment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.FeedbackDate)
                .HasColumnType("datetime")
                .HasColumnName("feedback_date");
            entity.Property(e => e.FeedbackType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("feedback_type");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.RatingFeedbacks)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__RATING_FE__docto__68487DD7");

            entity.HasOne(d => d.User).WithMany(p => p.RatingFeedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RATING_FE__user___693CA210");
        });

        modelBuilder.Entity<UserMembership>(entity =>
        {
            entity.HasKey(e => e.Membershipid).HasName("PK__USER_MEM__86AB370FFEB4838B");

            entity.ToTable("USER_MEMBERSHIP");

            entity.Property(e => e.Membershipid).HasColumnName("membershipid");
            entity.Property(e => e.CouponCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("coupon_code");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.PackageId).HasColumnName("package_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.SubscriptionStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active")
                .HasColumnName("subscription_status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Package).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__USER_MEMB__packa__6A30C649");

            entity.HasOne(d => d.User).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__USER_MEMB__user___6B24EA82");
        });

        modelBuilder.Entity<Useraccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERACCO__B9BE370FE6B91909");

            entity.ToTable("USERACCOUNT");

            entity.HasIndex(e => e.PhoneNumber, "UQ__USERACCO__A1936A6BF533A720").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__USERACCO__AB6E61641A2C578B").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastLogin)
                .HasColumnType("datetime")
                .HasColumnName("last_login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_picture");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("registration_date");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("active")
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
