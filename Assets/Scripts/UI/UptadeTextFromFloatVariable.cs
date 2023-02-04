using FeTo.SOArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UptadeTextFromFloatVariable : MonoBehaviour
{
    [SerializeField]
    FloatVariable floatVariable;
    [SerializeField]
    float initialValue;
    [SerializeField]
    TextMeshProUGUI displayText;
    [SerializeField]
    float refreshTextTime;

    float timeFromLastRefresh;
    float currentValue;

    private void Start() {
        floatVariable.SetValue(initialValue);
        currentValue = initialValue;
        displayText.SetText(floatVariable.Value.ToString());
        timeFromLastRefresh = 0f;
    }

    private void Update() {
        timeFromLastRefresh += Time.deltaTime;
        if (timeFromLastRefresh >= refreshTextTime) {
            if(currentValue != floatVariable.Value) {
                currentValue = floatVariable.Value;
                displayText.SetText(floatVariable.Value.ToString());
            }
            timeFromLastRefresh = 0;
        }
    }
}
