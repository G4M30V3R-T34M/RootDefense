using FeTo.ObjectPool;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SquirrelTurret : BaseTurret
{
    [SerializeField]
    Transform accornSpawner;
    [SerializeField]
    ObjectPool accornPool;

    public void SetAccornPool(ObjectPool injectedPool) {
        accornPool = injectedPool;
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

    private void SpawnAccorn() {
        if (targets.Count > 0) {
            AccornBulletController element = (AccornBulletController)accornPool.GetNext();
            element.transform.position = accornSpawner.transform.position;
            element.SetTarget(targets.First().transform);
            element.SetAccornDamage(currentSettings.damage);
            element.gameObject.SetActive(true);

        }
    }
}
