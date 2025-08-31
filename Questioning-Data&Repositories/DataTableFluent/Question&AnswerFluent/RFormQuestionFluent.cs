namespace Questioning_Data_Repositories.DataTableFluent.Question_AnswerFluent
{
    public class RFormQuestionFluent : IEntityTypeConfiguration<RFormQuestion>
    {
        public void Configure(EntityTypeBuilder<RFormQuestion> builder)
        {
            builder.ToTable(nameof(RFormQuestion));

            #region Table Facilities

            builder.HasKey(fq => fq.FormQuestionId);

            builder.HasIndex(fq => new
            {
                fq.FkFormId,
                fq.FkQuestionId,
                fq.FormQuestionId
            }).IsUnique();

            builder.Property(fq => fq.FormQuestionId)
                .HasDefaultValueSql("NEWID()")
                .ValueGeneratedOnAdd();

            #endregion

            #region Table Relations

            builder.HasOne(fq => fq.Form)
                .WithMany(fq => fq.RFormQuestions)
                .HasForeignKey(fq => fq.FkFormId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasOne(fq => fq.Question)
                .WithMany(fq => fq.RFormQuestions)
                .HasForeignKey(fq => fq.FkQuestionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            #endregion
        }
    }
}
