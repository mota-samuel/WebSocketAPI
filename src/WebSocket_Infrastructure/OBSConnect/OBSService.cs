using WebSocket_Domain.ServicesConnection;
using WebSocket_Infrastructure.OBSConnect;


namespace WebSocket_Infrastructure.OBSServices;
public class OBSService : IObsService
{
    private readonly ObsConnection _connection;

    public OBSService(ObsConnection connection)
    {
        _connection = connection;
    }

    public bool IsConnected()
        => _connection.GetClient().IsConnected;

    public bool IsRecording()
        => _connection.GetClient().GetRecordStatus().IsRecording;

    public void StartRecording()
        => _connection.GetClient().StartRecord();

    public void StopRecording()
        => _connection.GetClient().StopRecord();

    public void SetScene(string name)
        => _connection.GetClient().SetCurrentProgramScene(name);
}
