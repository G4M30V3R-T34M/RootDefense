using UnityEngine;
using UnityEngine.Events;
using FeTo.SOArchitecture;

public class HealthManager : MonoBehaviour
{
    public delegate void NoHealthAction();
    public UnityEvent NoHealth;

    public GameEvent enemyDead;

    private float health;

    public void SetUp(float initialHealth) {
        health = initialHealth;
    }

    public float GetCurrentHealth() {
        return health;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0 && NoHealth != null) {
            enemyDead.Raise();
            NoHealth.Invoke();
        }
    }
}