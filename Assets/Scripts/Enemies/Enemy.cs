using UnityEngine;

using FeTo.ObjectPool;
using FeTo.SOArchitecture;

[RequireComponent(typeof(HealthManager))]
public class Enemy : PoolableObject
{
    [SerializeField]
    private EnemyScriptableObject enemySettings;

    HealthManager healthManager;

    [SerializeField]
    private FloatVariable score;
    [SerializeField]
    private FloatVariable ResourcesToGrowRoots;

    protected void Start() {
        healthManager = GetComponent<HealthManager>();
        healthManager.SetUp(enemySettings.health);
    }

    public void DieAction() {
        score.ApplyChange(enemySettings.points);
        ResourcesToGrowRoots.ApplyChange(enemySettings.reward);
        // implement anim or something
    }
}
