using FeTo.ObjectPool;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    ObjectPool enemyPool;
    public void DoSpawnEnemy() {
        Enemy element = (Enemy)enemyPool.GetNext();
        element.gameObject.transform.position = this.transform.position;
        element.gameObject.GetComponent<Rigidbody>().detectCollisions = true;
        element.gameObject.SetActive(true);
    }
}
