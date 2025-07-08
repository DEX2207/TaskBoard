using Microsoft.EntityFrameworkCore;
using TaskBoard.Domain.Enum;
using TaskStatus = TaskBoard.Domain.Enum.TaskStatus;

namespace TaskBoard.Infrastructure.Configurations;

public class EnumConfigurations
{
    public static void ConfigureEnums(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<Roles>("public");
        modelBuilder.HasPostgresEnum<TaskStatus>("public");
    }
}