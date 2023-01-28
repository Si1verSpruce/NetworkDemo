using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private Player _spawnedPlayer;
    [SerializeField] private int _playerCount;
    [SerializeField] private Transform _playersContainer;
    [SerializeField] private Transform _lookAtObject;
    [SerializeField] private Color[] _playerColors;

    public void Spawn()
    {
        List<SpawnPoint> availableSpawnPoints = _spawnPoints.ToList();
        List<Color> availableColors = _playerColors.ToList();

        for (int i = 0; i < _playerCount; i++)
        {
            int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
            int colorIndex = Random.Range(0, availableColors.Count);
            Quaternion spawnedPlayerRotation = GetSpawnedPlayerRotation(availableSpawnPoints[spawnPointIndex].transform, _lookAtObject);
            availableSpawnPoints[spawnPointIndex].Spawn(_spawnedPlayer, spawnedPlayerRotation, _playersContainer, availableColors[colorIndex]);
            availableSpawnPoints.Remove(availableSpawnPoints[spawnPointIndex]);
            availableColors.Remove(availableColors[colorIndex]);
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
