using Microsoft.Extensions.Options;
using OBSWebsocketDotNet;

namespace WebSocket_Infrastructure.OBSConnect;
public class ObsConnection
{
    private readonly OBSWebsocket _obs;
    private readonly string _url;
    private readonly string _password;

    public ObsConnection(IOptions<OBSSettings> settings)
    {
        _obs = new OBSWebsocket();
        _url = settings.Value.Url;
        _password = settings.Value.Password;
    }

    public OBSWebsocket GetClient()
    {
        if (!_obs.IsConnected)
            _obs.Connect(_url, _password);

        return _obs;
    }

}
