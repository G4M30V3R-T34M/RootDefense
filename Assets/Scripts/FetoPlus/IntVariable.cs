using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "FeToPlus/IntVariable")]
public class IntVariable : ScriptableObject
{

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public int Value;

    public void SetValue(int value) {
        Value = value;
    }

    public void SetValue(IntVariable intVariable) {
        Value = intVariable.Value;
    }

    public void ApplyChange(int amount) {
        Value += amount;
    }

    public void ApplyChange(IntVariable amount) {
        Value += amount.Value;
    }
}
