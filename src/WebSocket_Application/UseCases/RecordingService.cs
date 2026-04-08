using WebSocket_Domain.ServicesConnection;

namespace WebSocket_Application.UseCases;
public class RecordingService
{
    private readonly IObsService _obs;

    public RecordingService(IObsService obs)
    {
        _obs = obs;
    }

    public string Start()
    {
        if (!_obs.IsConnected())
            return "OBS offline";

        if (_obs.IsRecording())
            return "Já gravando";

        _obs.StartRecording();
        return "OK";
    }

    public string Stop()
    {
        if (!_obs.IsConnected())
            return "OBS offline";

        if (!_obs.IsRecording())
            return "Já parado";

        _obs.StopRecording();
        return "OK";
    }

    public void ChangeScene(string name)
    {
        _obs.SetScene(name);
    }
}
