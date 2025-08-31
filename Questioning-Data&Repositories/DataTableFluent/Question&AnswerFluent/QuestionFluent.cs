namespace Questioning_Data_Repositories.DataTableFluent.Question_AnswerFluent
{
    public class QuestionFluent : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable(nameof(Question));

            #region Question Table Facilities

            builder.HasKey(q => q.QuestionId);

            builder.HasIndex(q => q.QuestionId).IsUnique();

            builder.HasIndex(q => q.QuestionText);

            builder.Property(q => q.QuestionId)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(q => q.QuestionText)
                .HasMaxLength(300)
                .IsRequired()
                .IsUnicode();

            #endregion

            #region Question Table Relations

            builder.HasOne(q => q.User)
                .WithMany(q => q.Questions)
                .HasForeignKey(q => q.FkUserId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
