using UnityEngine;

using FeTo.ObjectPool;
using FeTo.SOArchitecture;
using System.Linq;
using JetBrains.Annotations;

[RequireComponent(typeof(HealthManager))]
public class Enemy : PoolableObject
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameEvent gameOverEvent;

    [Header("Scriptables settings")]
    [SerializeField]
    EnemyScriptableObject enemySettings;
    [SerializeField]
    PathScriptableObject enemyPath;

    private Vector3 target;
    private int currentTarget;

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

    private void OnEnable() {
        currentTarget = 0;
        target = enemyPath.path[0];
        transform.LookAt(target);
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

    private void Update() {
        if (Vector3.Distance(transform.position, target) <= 0) {
            SetNextTarget();
        }
        transform.position = Vector3.MoveTowards(transform.position, target, enemySettings.speed * Time.deltaTime);
    }

    private void SetNextTarget() {
        currentTarget += 1;
        if(currentTarget >= enemyPath.path.Count) {
            gameOverEvent.Raise();
        } else {
            target = enemyPath.path[currentTarget];
            transform.LookAt(target);
        }
    }
}
