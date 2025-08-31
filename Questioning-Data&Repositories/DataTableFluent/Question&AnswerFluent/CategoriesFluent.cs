namespace Questioning_Data_Repositories.DataTableFluent.Question_AnswerFluent
{
    public class CategoriesFluent : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable(nameof(Categories));

            #region Categories Table Facilities

            builder.HasKey(c => c.CategoryId);

            builder.HasIndex(c => c.CategoryId).IsUnique();

            builder.HasIndex(c => c.CategoryName);

            builder.Property(c => c.CategoryName)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

            #endregion
        }
    }
}
