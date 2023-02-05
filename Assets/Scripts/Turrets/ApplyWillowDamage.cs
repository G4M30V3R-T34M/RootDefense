using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyWillowDamage : MonoBehaviour
{
    float damage = 1;
    public void SetDamage(float parentDamage) {
        damage = parentDamage;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == (int)Layer.Enemy) {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
