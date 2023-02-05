using FeTo.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhopingWillowSpawner : MonoBehaviour
{
    [SerializeField]
    ObjectPool willowPool;

    public void SpawnSquirrel(Transform tile) {
        WhompingWillowTurret element = (WhompingWillowTurret) willowPool.GetNext();
        element.gameObject.transform.position = tile.position;
        element.gameObject.SetActive(true);
    }

}