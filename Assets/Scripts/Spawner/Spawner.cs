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

    private void Awake()
    {
        Spawn(_spawnPoints, _spawnedPlayer, _playerCount, _playerColors);
    }

    private void Spawn(SpawnPoint[] spawnPoints, Player spawnedPlayer, int playerCount, Color[] colors)
    {
        List<SpawnPoint> availableSpawnPoints = spawnPoints.ToList();
        List<Color> availableColors = colors.ToList();

        for (int i = 0; i < playerCount; i++)
        {
            int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
            int colorIndex = Random.Range(0, availableColors.Count);
            Quaternion spawnedPlayerRotation = GetSpawnedPlayerRotation(availableSpawnPoints[spawnPointIndex].transform, _lookAtObject);
            availableSpawnPoints[spawnPointIndex].Spawn(spawnedPlayer, spawnedPlayerRotation, _playersContainer, availableColors[colorIndex]);
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
