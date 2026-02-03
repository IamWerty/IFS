namespace Invaders.Shared.Enums;

[Flags]
public enum Permissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Delete = 4,
    Execute = 8,
    Upload = 16,
    Download = 32,
    TerminalAccess = 64,
    LogsAccess = 128,
    ManageUsers = 256
}
