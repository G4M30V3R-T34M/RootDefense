using FeTo.ObjectPool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SquirrelTurret : BaseTurret
{
    [SerializeField]
    SphereCollider turretCollider;
    [SerializeField]
    Transform accornSpawner;
    [SerializeField]
    ObjectPool accornPool; // this must be inyected by squirrel spawner

    float timeFromLastAttack;

    List<GameObject> Targets = new List<GameObject>();

    private void Start() {
        turretCollider.radius = currentSettings.range;
        timeFromLastAttack = currentSettings.cooldown;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layer.Enemy) {
            Targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == (int)Layer.Enemy) {
            Targets.Remove(other.gameObject);
        }
    }

    private void Update() {
        timeFromLastAttack += Time.deltaTime;
        if(CanAttack()) {
            // play animation
            timeFromLastAttack = 0;
        }
    }

    private bool CanAttack() {
        return timeFromLastAttack >= currentSettings.cooldown && Targets.Count > 0;
    }

    private void SpawnAccorn() {
        AccornBulletController element = (AccornBulletController)accornPool.GetNext();
        element.transform.position = accornSpawner.transform.position;
        element.GetComponent<AccornBulletController>().SetTarget(Targets.First().transform);
        element.gameObject.SetActive(true);
    }
}
