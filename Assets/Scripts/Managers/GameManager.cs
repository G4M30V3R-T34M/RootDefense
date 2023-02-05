using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    GameObject defenseManager;
    [SerializeField] 
    GameObject techTreeManager;

    [SerializeField] IntVariable gameMode;

    private void Start() {
        techTreeManager.SetActive(false);
    }

    public void GameModeChanged() {
        defenseManager.SetActive(gameMode.Value == (int)GameMode.DEFENSE);
        techTreeManager.SetActive(gameMode.Value == (int)GameMode.TECHTREE);
    }
}
