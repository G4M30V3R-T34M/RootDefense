using FeTo.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccornBulletController : PoolableObject
{
    [SerializeField]
    AccornScriptableObject accornSettings;
    private Transform target;
    private float damage;
    
    void Update()
    {
        if(Vector3.Distance(target.position + new Vector3(0, 1.54f, 0), transform.position) >= 0.01f) {
            transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, 1.54f, 0), accornSettings.speed * Time.deltaTime);
        } else {
            gameObject.SetActive(false);
        }
    }

    public void SetTarget(Transform targetTransform) {
        target = targetTransform;
    }

    public void SetAccornDamage(float turretDamage) {
        damage = turretDamage;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == (int)Layer.Enemy) {
            other.GetComponent<Enemy>().TakeDamage(damage);
            // TODO play sound
            gameObject.SetActive(false);
        }
    }
}
