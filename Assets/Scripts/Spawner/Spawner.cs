using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Transform _lookAtObject;

    public void Spawn(Player[] players)
    {
        List<SpawnPoint> availableSpawnPoints = _spawnPoints.ToList();

        for (int i = 0; i < players.Length; i++)
        {
            int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
            players[i].transform.position = availableSpawnPoints[spawnPointIndex].transform.position;
            players[i].transform.rotation = GetSpawnedPlayerRotation(availableSpawnPoints[spawnPointIndex].transform, _lookAtObject);
            availableSpawnPoints.Remove(availableSpawnPoints[spawnPointIndex]);
        }
    }

    private Quaternion GetSpawnedPlayerRotation(Transform spawnPoint, Transform lookAtObject)
    {
        Vector3 relativePosition = lookAtObject.position - spawnPoint.position;
        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        rotation = Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y, 0));

        return rotation;
    }
}
