using FeTo.SOArchitecture;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    EnemySpawnerScriptableObject spawnerSettings;

    [SerializeField]
    FloatVariable waves;

    private int spawnedEnemies;
    private Coroutine spawnCoroutine;

    public GameEvent SpawnNormalEnemy;
    public GameEvent SpawnFastEnemy;
    public GameEvent SpawnSlowEnemy;

    private float totalEnemyPercents;

    private void Start() {
        spawnedEnemies = 0;
        totalEnemyPercents = spawnerSettings.slowEnemyPerc + spawnerSettings.normalEnemyPerc + spawnerSettings.fastEnemyPerc;
        spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine() {
        while (true) {
            spawnedEnemies = 0;
            while(spawnedEnemies < spawnerSettings.numberOfEnemies) {
                RaiseSpawnEvent();
                spawnedEnemies += 1;
                yield return new WaitForSeconds(spawnerSettings.timeBetweenSpawn);
            }
            yield return new WaitForSeconds(spawnerSettings.timeBetweenWave);
            waves.ApplyChange(1);
        }
    }

    private void RaiseSpawnEvent() {
        float percent = Random.Range(0.0f, totalEnemyPercents);
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
