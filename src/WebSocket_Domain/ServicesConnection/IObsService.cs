namespace WebSocket_Domain.ServicesConnection;
public interface IObsService
{
    public bool IsConnected();
    public bool IsRecording();
    public void StartRecording();
    public void StopRecording();
    public void SetScene(string name);
}
