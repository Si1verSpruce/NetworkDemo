using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NetworkManagerArena : NetworkManager
{
    public event UnityAction<NetworkConnectionToClient> PlayerConnected;
    public event UnityAction<NetworkConnectionToClient> PlayerDisconnected;

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        PlayerConnected?.Invoke(conn);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        PlayerDisconnected?.Invoke(conn);
        base.OnServerDisconnect(conn);
    }
}
