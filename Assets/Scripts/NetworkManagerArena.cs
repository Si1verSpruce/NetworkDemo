using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class NetworkManagerArena : NetworkManager
{
    [SerializeField] private Transform _playersContainer;

    public override void OnServerAddPlayer(NetworkConnectionToClient connection)
    {
        GameObject player = Instantiate(playerPrefab, _playersContainer);
        NetworkServer.AddPlayerForConnection(connection, player);

        // spawn ball if two players
        /*if (numPlayers == 2)
        {
            ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
            NetworkServer.Spawn(ball);
        }*/
    }
}
