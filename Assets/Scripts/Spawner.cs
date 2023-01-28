using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private GameObject _spawnedGameObject;
    [SerializeField] private int _gameObjectsCount;
    [SerializeField] private Transform _playersContainer;
    [SerializeField] private Transform _lookAtObject;

    private void Awake()
    {
        Spawn(_spawnPoints, _spawnedGameObject, _gameObjectsCount);
    }

    private void Spawn(SpawnPoint[] spawnPoints, GameObject spawnedGameObject, int _gameObjectsCount)
    {
        List<SpawnPoint> availableSpawnPoints = spawnPoints.ToList();

        for (int i = 0; i < _gameObjectsCount; i++)
        {
            int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
            Quaternion spawnedObjectRotation = GetSpawnedGameObjectRotation(availableSpawnPoints[spawnPointIndex].transform, _lookAtObject);
            availableSpawnPoints[spawnPointIndex].Spawn(spawnedGameObject, spawnedObjectRotation, _playersContainer);
            availableSpawnPoints.Remove(availableSpawnPoints[spawnPointIndex]);
        }
    }

    private Quaternion GetSpawnedGameObjectRotation(Transform spawnPoint, Transform lookAtObject)
    {
        Vector3 relativePosition = lookAtObject.position - spawnPoint.position;
        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        rotation = Quaternion.Euler(new Vector3(0, rotation.eulerAngles.y, 0));

        return rotation;
    }
}
