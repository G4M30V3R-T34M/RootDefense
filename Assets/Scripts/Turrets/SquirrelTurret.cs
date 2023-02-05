using FeTo.ObjectPool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SquirrelTurret : BaseTurret
{
    [SerializeField]
    SphereCollider turretCollider;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Transform accornSpawner;
    [SerializeField]
    ObjectPool accornPool;

    float timeFromLastAttack;

    List<GameObject> targets = new List<GameObject>();

    private void Start() {
        turretCollider.radius = currentSettings.range;
        timeFromLastAttack = currentSettings.cooldown;
    }

    public void SetAccornPool(ObjectPool injectedPool) {
        accornPool = injectedPool;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layer.Enemy) {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        print("Exit");
        if(other.gameObject.layer == (int)Layer.Enemy) {
            targets.Remove(other.gameObject);
        }
    }

    private void Update() {
        if(targets.Count > 0) {
            transform.LookAt(targets.First().transform);
            transform.Rotate(0, 180, 0); // TODO Sorry, change rotation on anims
        }
        timeFromLastAttack += Time.deltaTime;
        if(CanAttack()) {
            animator.SetTrigger("Attack");
            timeFromLastAttack = 0;
        }
    }

    private bool CanAttack() {
        return timeFromLastAttack >= currentSettings.cooldown && targets.Count > 0;
    }

    private void SpawnAccorn() {
        AccornBulletController element = (AccornBulletController)accornPool.GetNext();
        element.transform.position = accornSpawner.transform.position;
        element.SetTarget(targets.First().transform);
        element.SetAccornDamage(currentSettings.damage);
        element.gameObject.SetActive(true);
    }
}
