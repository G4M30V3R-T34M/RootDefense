using UnityEngine;

using FeTo.ObjectPool;

[RequireComponent(typeof(HealthManager))]
public class Enemy : PoolableObject
{
    [SerializeField]
    private EnemyScriptableObject enemySettings;

    HealthManager healthManager;

    protected void Start() {
        healthManager = GetComponent<HealthManager>();
        healthManager.SetUp(enemySettings.health);
    }

    public void DieAction() {
        //TODO play anim
    }
}
