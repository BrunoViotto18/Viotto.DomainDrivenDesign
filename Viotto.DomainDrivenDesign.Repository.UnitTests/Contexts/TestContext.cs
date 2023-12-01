using Microsoft.EntityFrameworkCore;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Contexts;

using Models;


public class TestContext : DbContext
{
    public DbSet<Test> Test { get; set; }


    public TestContext()
    {
    }

    public TestContext(Func<DbContextOptionsBuilder, DbContextOptionsBuilder> build)
        : base(build(new DbContextOptionsBuilder()).Options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Test>(builder =>
        {
            builder.ToTable("Test", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(200)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(x => x.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .HasConversion(
                    dateOnly => new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day),
                    dateTime => new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day)
                )
                .IsRequired();

            builder.Property(x => x.LuckyNumber)
                .HasColumnName("LuckyNumber")
                .IsRequired();
        });
    }
}
