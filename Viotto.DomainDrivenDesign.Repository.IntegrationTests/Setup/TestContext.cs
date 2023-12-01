using Microsoft.EntityFrameworkCore;

namespace Viotto.DomainDrivenDesign.Repository.IntegrationTests;

internal class TestContext : DbContext
{
    private readonly string _connectionString;

    public TestContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TestModel>(model =>
        {
            model.ToTable("TestModel", "dbo");

            model.HasKey(x => x.Id);

            model.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            model.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(256)
                .IsUnicode(false)
                .IsRequired();

            model.Property(x => x.BirthDate)
                .HasColumnName("BirthDate")
                .IsRequired();
            
            model.Property(x => x.LuckyNumber)
                .HasColumnName("LuckyNumber")
                .IsRequired();
        });
    }
}
