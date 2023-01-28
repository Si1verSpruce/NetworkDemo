using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void Spawn(GameObject spawnedGameObject, Quaternion rotation, Transform parent)
    {
        Instantiate(spawnedGameObject, transform.position, rotation, parent);
    }
}
