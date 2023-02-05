using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WhompingWillowTurret : BaseTurret
{
    [SerializeField]
    GameObject leaves;

    private void Update() {
        if (targets.Count > 0) {
            transform.LookAt(targets.First().transform);
            transform.Rotate(0, 180, 0); // TODO Sorry, change rotation on anims
        }

        timeFromLastAttack += Time.deltaTime;
        if (CanAttack()) {
            animator.SetTrigger("Attack");
            timeFromLastAttack = 0;
        }
    }

    public void Upgrade() {
        base.Upgrade();
        leaves.GetComponent<ApplyWillowDamage>().SetDamage(currentSettings.damage);
    }

}
