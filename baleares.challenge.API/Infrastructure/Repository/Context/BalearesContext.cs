using Microsoft.EntityFrameworkCore;
using baleares.challenge.API.model.contacts;
using baleares.challenge.API.model.users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace baleares.challenge.API.Infrastructure.Repository.Context;

public class BalearesContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<Contact> Contact { get; set; }

    public BalearesContext(DbContextOptions<BalearesContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0760944DCA");
            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053410F5826C").IsUnique();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3214EC07C6EE024E");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.PhoneWork).HasMaxLength(15);
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Contacts__UserID__3D5E1FD2");
        });
    }
}