using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopTime : MonoBehaviour
{
    public void Stop() {
        Time.timeScale = 0;
    }

    private void OnDestroy() {
        Time.timeScale = 1;
    }
}
