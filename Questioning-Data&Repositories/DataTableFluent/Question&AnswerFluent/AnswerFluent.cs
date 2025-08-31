namespace Questioning_Data_Repositories.DataTableFluent.Question_AnswerFluent
{
    public class AnswerFluent : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.ToTable(nameof(Answer));

            #region Answer Table Facilties

            builder.HasKey(a => a.AnswerId);

            builder.HasIndex(a => a.AnswerId).IsUnique();

            builder.HasIndex(a => new
            {
                a.AnswerText,
                a.TestCorrectOption,
                a.TruOrFalseAnswer
            });

            builder.Property(a => a.AnswerId)
                .HasDefaultValue("NEWID()")
                .ValueGeneratedOnAdd();

            builder.Property(a => a.AnswerTypes)
                .IsRequired();

            builder.Property(a => a.TestOptionOne)
                .IsRequired(false)
                .HasMaxLength(200)
                .IsUnicode();

            builder.Property(a => a.TestOptionTwo)
                .IsRequired(false)
                .HasMaxLength(200)
                .IsUnicode();

            builder.Property(a => a.TestOptionThree)
                .IsRequired(false)
                .HasMaxLength(200)
                .IsUnicode();

            builder.Property(a => a.TestOptionFour)
                .IsRequired(false)
                .HasMaxLength(200)
                .IsUnicode();

            builder.Property(a => a.TruOrFalseAnswer)
                .HasDefaultValue("false");

            builder.Property(a => a.AnswerText)
                .HasMaxLength(500)
                .IsRequired(false)
                .IsUnicode();

            #endregion

            #region Answer Table Relations

            builder.HasOne(a => a.Question)
                .WithMany(a => a.Answers)
                .HasForeignKey(a => a.FkQuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion
        }
    }
}
