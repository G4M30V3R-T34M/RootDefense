using FeTo.SOArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShowGameOverMessage : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    FloatReference waves;
    [SerializeField]
    FloatReference score;

    private void OnEnable() {
        text.SetText("Has sobrevivido " + waves.Value + " y has conseguido " + score.Value + " puntos.");
    }
}
