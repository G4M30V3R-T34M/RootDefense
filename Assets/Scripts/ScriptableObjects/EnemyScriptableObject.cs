using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "Scriptables/EnemyScriptableObject", order = 2)]
public class EnemyScriptableObject : ScriptableObject
{
    public EnemyType enemyType;
    public float damage;
    public float speed;
    public float health;
    public int reward;
    public int points;
}
