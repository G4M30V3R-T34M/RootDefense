using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternBottomGroups : MonoBehaviour
{
    [SerializeField]
    GameObject ForestBar;

    [SerializeField]
    GameObject RootsBar;

    [SerializeField] IntReference gameMode;

    private void Start() {
        ForestBar.SetActive(true);
        RootsBar.SetActive(false);
    }

    public void AlternBars() {
        ForestBar.SetActive(gameMode == (int)GameMode.DEFENSE);
        RootsBar.SetActive(gameMode == (int)GameMode.TECHTREE);
    }
}
