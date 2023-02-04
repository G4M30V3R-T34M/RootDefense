using FeTo.SOArchitecture;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerScriptableObject", menuName = "Scriptables/EnemySpawnerScriptableObject", order = 3)]

public class EnemySpawnerScriptableObject : ScriptableObject
{
    [Header("Time related variables")]
    public float timeBetweenSpawn;
    public float timeBetweenWave;

    [Header("Enemy configuration")]
    public float numberOfEnemies;
    public float normalEnemyPerc;
    public float fastEnemyPerc;
    public float slowEnemyPerc;
}
