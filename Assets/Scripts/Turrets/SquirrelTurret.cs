using FeTo.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelTurret : BaseTurret
{
    [SerializeField]
    SphereCollider turretCollider;
    [SerializeField]
    ObjectPool AccornPool; // this must be inyected by squirrel spawner

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
            //Shoot bullet from object pool
            timeFromLastAttack = 0;
        }
    }

    private bool CanAttack() {
        return timeFromLastAttack >= currentSettings.cooldown && Targets.Count > 0;
    }
}
