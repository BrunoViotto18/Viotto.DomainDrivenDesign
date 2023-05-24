using Microsoft.EntityFrameworkCore;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests.Contexts;

using Models;


internal class Context : DbContext
{
    public DbSet<Teacher> Teacher { get; set; }
    public DbSet<Class> Class { get; set; }
    public DbSet<Student> Student { get; set; }

    public Context(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Teacher>(builder =>
        {
            builder.ToTable("Teacher", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            builder.HasMany(x => x.Classes)
                .WithMany(x => x.Teachers)
                .UsingEntity<TeacherClass>(
                    "TeacherClass",
                    r => r.HasOne(x => x.Class)
                        .WithMany()
                        .HasForeignKey(x => x.ClassId),
                    l => l.HasOne(x => x.Teacher)
                        .WithMany()
                        .HasForeignKey(x => x.TeacherId),
                    j => j.HasKey(x => x.Id)
                );
        });

        modelBuilder.Entity<Class>(builder =>
        {
            builder.ToTable("Class", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();
        });

        modelBuilder.Entity<Student>(builder =>
        {
            builder.ToTable("Student", "dbo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            builder.HasOne(x => x.Class)
                .WithMany(x => x.Students)
                .HasForeignKey(x => x.ClassId);
        });
    }
}
