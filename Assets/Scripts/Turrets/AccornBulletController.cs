using FeTo.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccornBulletController : PoolableObject
{
    [SerializeField]
    AccornScriptableObject accornSettings;
    private Transform target;
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, accornSettings.speed * Time.deltaTime);
    }

    public void SetTarget(Transform targetTransform) {
        target = targetTransform;
    }
}
