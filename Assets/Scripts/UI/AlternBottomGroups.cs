using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternBottomGroups : MonoBehaviour
{
    [SerializeField]
    GameObject ForestBar;

    [SerializeField]
    GameObject RootsBar;

    [SerializeField] IntVariable gameMode;

    private void Start() {
        ForestBar.SetActive(true);
        RootsBar.SetActive(false);
    }

    public void AlternBars() {
        ForestBar.SetActive(gameMode.Value == (int)GameMode.DEFENSE);
        RootsBar.SetActive(gameMode.Value == (int)GameMode.TECHTREE);
    }
}
