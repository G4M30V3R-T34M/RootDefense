using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RootRewards", menuName = "Scriptables/RootRewardsScriptableObject")]
public class RootRewardsScriptableObject : ScriptableObject
{
    public List<TurretType> rewards;
}
