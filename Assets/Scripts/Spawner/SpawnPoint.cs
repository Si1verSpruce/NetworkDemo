using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void Spawn(Player spawnedPlayer, Quaternion rotation, Transform parent, Color color)
    {
        Player player = Instantiate(spawnedPlayer, transform.position, rotation, parent);
        player.Init(color);
    }
}
