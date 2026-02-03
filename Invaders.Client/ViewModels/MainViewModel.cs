using Invaders.Client.Network;
using Invaders.Client.ViewModels;
using Invaders.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

public class MainViewModel : BaseViewModel
{
    private ClientConnection _connection;
    private ClientAuthService _auth;
    private ClientFileService _files;

    public ObservableCollection<FileViewModel> Files { get; set; } = new();
    public string Logs { get => _logs; set => Set(ref _logs, value); }
    private string _logs = "";
    public string Status { get => _status; set => Set(ref _status, value); }
    private string _status = "Disconnected";

    public MainViewModel()
    {
        _connection = new ClientConnection();
        _auth = new ClientAuthService(_connection);
        _files = new ClientFileService(_connection);

        Logs = "[SYSTEM] Client ready\n";
    }

    public async Task Connect()
    {
        await _connection.ConnectAsync("127.0.0.1", 9000);
        Status = "Connected";
        AddLog("[NET] Connected to server");
    }

    public async Task Login()
    {
        var result = await _auth.Login("TestUser", Environment.MachineName);
        Status = $"Logged in as {result.Nick} ({result.Role})";
        AddLog("[AUTH] Login successful");
    }

    public async Task RefreshFiles()
    {
        var list = await _files.RequestFileList();
        Files.Clear();
        foreach (var f in list)
            Files.Add(new FileViewModel(f));
        AddLog("[FILES] File list refreshed");
    }

    private void AddLog(string text)
    {
        Logs += text + "\n";
    }
}
