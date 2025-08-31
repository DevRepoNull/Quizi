namespace Questioning_Data_Repositories.DataTableFluent.Question_AnswerFluent
{
    public class UserAnswerFluent : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.ToTable(nameof(UserAnswer));

            #region UserAnswer Table Facilites

            builder.HasKey(ua => ua.UAnswerId);

            builder.HasIndex(ua => new
            {
                ua.FkUserId,
                ua.FkQuestionId,
            }).IsUnique();

            builder.Property(ua => ua.UAnswerId)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(ua => ua.DescriptiveAnswerText)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(400);

            builder.Property(ua => ua.SelectedOption)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(ua => ua.FkQuestionId)
                .IsRequired();

            builder.Property(ua => ua.FkAnswerId)
                .IsRequired(false);

            builder.Property(ua => ua.FkUserId)
                .IsRequired();

            builder.Property(ua => ua.AnswerDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder.Property(ua => ua.CreateDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(ua => ua.UpdateDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .ValueGeneratedOnUpdate();

            #endregion

            #region UserAnswer Table Facilites

            builder.HasOne(ua => ua.User)
                .WithMany(ua => ua.UserAnswers)
                .HasForeignKey(ua => ua.FkUserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.Answer)
                .WithMany(ua => ua.UserAnswers)
                .HasForeignKey(ua => ua.FkAnswerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.Question)
                .WithMany(ua => ua.UserAnswers)
                .HasForeignKey(ua => ua.FkQuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.Form)
                .WithMany(ua => ua.UserAnswers)
                .HasForeignKey(ua => ua.FkFormId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
