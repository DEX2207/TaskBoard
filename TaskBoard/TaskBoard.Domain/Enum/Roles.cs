using NpgsqlTypes;

namespace TaskBoard.Domain.Enum;

public enum Roles
{
    [PgName("administrator")]
    administrator=0,
    [PgName("manager")]
    manager=1,
    [PgName("user")]
    user=2
}