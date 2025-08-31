namespace Questioning_Data_Repositories.DataTableFluent.User_RoleFluent
{
    public class RoleFluent : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role));

            #region Role Table Facilities

            builder.HasKey(r => r.RoleId);

            builder.HasIndex(r => r.RoleActive);

            builder.HasIndex(r => new
            {
                r.RoleId,
                r.RoleName
            }).IsUnique();

            builder.Property(r => r.RoleId)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(r => r.RoleName)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder.Property(r => r.RoleDescription)
                .HasMaxLength(200)
                .IsRequired()
                .IsUnicode();

            builder.Property(r => r.RoleActive)
                .IsRequired();

            #endregion
        }
    }
}
