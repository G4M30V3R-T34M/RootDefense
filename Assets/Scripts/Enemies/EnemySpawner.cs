using FeTo.SOArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    EnemySpawnerScriptableObject spawnerSettings;

    private int spawnedEnemies;
    private Coroutine spawnCoroutine;

    public GameEvent SpawnNormalEnemy;
    public GameEvent SpawnFastEnemy;
    public GameEvent SpawnSlowEnemy;

    private float totalEnemyPercents;

    private void Start() {
        spawnedEnemies = 0;
        totalEnemyPercents = spawnerSettings.slowEnemyPerc + spawnerSettings.normalEnemyPerc + spawnerSettings.fastEnemyPerc;
        Debug.Log(totalEnemyPercents);
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine() {
        while (true) {
            spawnedEnemies = 0;
            yield return new WaitForSeconds(spawnerSettings.timeBetweenSpawn);
            while(spawnedEnemies < spawnerSettings.numberOfEnemies) {
                RaiseSpawnEvent();
                yield return new WaitForSeconds(spawnerSettings.timeBetweenSpawn);
            }
        }
    }

    private void RaiseSpawnEvent() {
        float percent = Random.Range(0.0f, totalEnemyPercents);
        Debug.Log(percent);
        if(percent < spawnerSettings.normalEnemyPerc) {
            SpawnNormalEnemy.Raise();
        } else if (percent < spawnerSettings.normalEnemyPerc + spawnerSettings.fastEnemyPerc) {
            SpawnFastEnemy.Raise();
        } else {
            SpawnSlowEnemy.Raise();
        }
    }

    private void OnDisable() {
        StopCoroutine(spawnCoroutine);
    }

}
