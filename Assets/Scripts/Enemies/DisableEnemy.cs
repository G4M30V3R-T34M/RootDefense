using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject parent;

    public void DisableParent() {
        parent.SetActive(false);
    }
}
