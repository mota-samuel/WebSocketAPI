using OBSWebsocketDotNet;

namespace WebSocket_Infrastructure.OBSConnect;
public class ObsConnection
{
    private readonly OBSWebsocket _obs;
    private readonly string _url;
    private readonly string _password;

    public ObsConnection(string url, string password)
    {
        _obs = new OBSWebsocket();
        _url = url;
        _password = password;
    }

    public OBSWebsocket GetClient()
    {
        if (!_obs.IsConnected)
            _obs.Connect(_url, _password);

        return _obs;
    }

}
