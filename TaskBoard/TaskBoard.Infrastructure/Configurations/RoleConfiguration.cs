using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Configurations;

public class RoleConfiguration:IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => new { r.ProjectId, r.UserId });
        
        builder.HasOne(r => r.Project)
            .WithMany(p => p.Roles)
            .HasForeignKey(r => r.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Roles)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(r => r.Roles)
            .HasColumnType("roles")
            .IsRequired();
    }
}