using FeTo.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelSpawner : MonoBehaviour
{
    [SerializeField]
    ObjectPool squirrelPool;
    [SerializeField]
    ObjectPool accornPool;
    
    public void SpawnSquirrel(Transform tile) {
        SquirrelTurret element = (SquirrelTurret)squirrelPool.GetNext();
        element.gameObject.transform.position = tile.position;
        element.SetAccornPool(accornPool);
        element.gameObject.SetActive(true);
    }

}
