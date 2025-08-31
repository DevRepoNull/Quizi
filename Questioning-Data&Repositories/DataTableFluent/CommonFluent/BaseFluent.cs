namespace Questioning_Data_Repositories.DataTableFluent.CommonFluent
{
    public class BaseFluent : IEntityTypeConfiguration<BaseEntity>
    {
        public void Configure(EntityTypeBuilder<BaseEntity> builder)
        {
            //builder.ToTable(nameof(BaseEntity));

            #region BaseEntity Table Facilities

            builder.Property(b => b.CreateDate)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(b => b.UpdateDate)
                .IsRequired(false)
                .HasDefaultValueSql("GETUTCDATE()");

            #endregion
        }
    }
}
