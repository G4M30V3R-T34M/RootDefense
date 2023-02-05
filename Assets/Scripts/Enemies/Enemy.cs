using UnityEngine;

using FeTo.ObjectPool;
using FeTo.SOArchitecture;

[RequireComponent(typeof(HealthManager))]
public class Enemy : PoolableObject
{
    [SerializeField]
    private EnemyScriptableObject enemySettings;
    [SerializeField]
    Animator animator;

    HealthManager healthManager;
    Rigidbody rb;

    [Header("Float Variables")]
    [SerializeField]
    private FloatVariable score;
    [SerializeField]
    private FloatVariable ResourcesToGrowRoots;

    protected void Start() {
        rb = GetComponent<Rigidbody>();
        healthManager = GetComponent<HealthManager>();
        healthManager.SetUp(enemySettings.health);
    }

    public void TakeDamage(float damage) {
        healthManager.TakeDamage(damage);
    }

    public void DieAction() {
        animator.SetTrigger("Die");
        rb.detectCollisions = false;
        score.ApplyChange(enemySettings.points);
        ResourcesToGrowRoots.ApplyChange(enemySettings.reward);
        // implement anim or something
    }
}
