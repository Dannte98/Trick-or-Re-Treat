using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] float _spawnTime;
    [SerializeField] Transform[] _spawnPoints;

    float _spawnTimer;

    void Start() => _spawnTimer = _spawnTime;


    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if(_spawnTimer <= 0.0f)
        {
            int index = Random.Range(0, _spawnPoints.Length);
            Instantiate(_enemyPrefab, _spawnPoints[index].position, Quaternion.identity);
            _spawnTimer = _spawnTime;
        }
    }
}
