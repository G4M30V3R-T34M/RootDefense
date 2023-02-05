using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MainMenu() {
        SceneManager.LoadScene(0);
    }

    public static void GamePlay() {
        SceneManager.LoadScene(1);
    }
}
