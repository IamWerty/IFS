namespace Invaders.Shared.Enums;

public enum PacketType
{
    Login,
    FileList,
    FileDownload,
    FileUpload,
    Command,
    LogRequest,
    Error,
    Success,
    AuthRequest,
    AuthResponse,

    FileListRequest,
    FileListResponse,

    FileDownloadRequest,
    FileDeleteRequest
}
