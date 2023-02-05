using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternBottomGroups : MonoBehaviour
{
    [SerializeField]
    GameObject ForestBar;

    [SerializeField]
    GameObject RootsBar;

    private void Start() {
        ForestBar.SetActive(true);
        RootsBar.SetActive(false);
    }

    public void AlternBars() {
        if(ForestBar.activeInHierarchy == true) {
            ForestBar.SetActive(false);
            RootsBar.SetActive(true);
        } else {
            ForestBar.SetActive(true);
            RootsBar.SetActive(false);
        }
    }
}
