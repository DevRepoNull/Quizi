namespace Questioning_Data_Repositories.DataTableFluent.User_RoleFluent
{
    public class UserFluent : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));

            #region User Table Facilities

            builder.HasKey(u => u.UserId);

            builder.HasIndex(u => new
            {
                u.FirstName,
                u.LastName,
                u.PhoneNumber
            });

            builder.HasIndex(u => new
            {
                u.UserId,
                u.Email,
                u.UserName
            }).IsUnique();

            builder.Property(u => u.UserId)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder.Property(u => u.UserProfile)
                .HasMaxLength(100)
                .IsUnicode();

            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder.Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false)
                .HasAnnotation(nameof(User.Email), true);

            builder.Property(u => u.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(u => u.UserName)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(u => u.Password)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(u => u.NationalCode)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.Property(u => u.IsDelete)
                .IsRequired();

            #endregion

            #region User Table Relations

            builder.HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.FkRoleId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
