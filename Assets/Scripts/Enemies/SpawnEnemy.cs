using FeTo.ObjectPool;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    ObjectPool EnemyPool;
    public void DoSpawnEnemy() {
        Enemy element = (Enemy)EnemyPool.GetNext();
        element.transform.position = this.transform.position;
        element.gameObject.SetActive(true);
    }
}
