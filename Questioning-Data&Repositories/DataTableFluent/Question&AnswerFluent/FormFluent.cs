namespace Questioning_Data_Repositories.DataTableFluent.Question_AnswerFluent
{
    public class FormFluent : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.ToTable(nameof(Form));

            #region From Table Facilities

            builder.HasKey(f => f.FormId);

            builder.HasIndex(f => new
            {
                f.FormTitle,
                f.Description,
                f.IsActive,
            });

            builder.HasIndex(f => f.FormId).IsUnique();

            builder.Property(f => f.FormId)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(f => f.FormTitle)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

            builder.Property(f => f.Description)
                .HasMaxLength(500)
                .IsRequired()
                .IsUnicode();

            builder.Property(f => f.IsActive)
                .IsRequired();

            builder.Property(f => f.StartExamTime)
                .IsRequired();

            builder.Property(f => f.EndExamTime)
                .IsRequired();

            builder.Property(f => f.StartExamDate)
                .IsRequired();

            builder.Property(f => f.EndExamDate)
                .IsRequired();

            #endregion

            #region Form Table Relations

            builder.HasOne(f => f.User)
                .WithMany(f => f.Forms)
                .HasForeignKey(f => f.FkUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(q => q.Categories)
                .WithMany(q => q.Forms)
                .HasForeignKey(q => q.FKCategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
