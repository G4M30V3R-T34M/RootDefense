using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathScriptableObject", menuName = "Scriptables/PathScriptableObject")]

public class PathScriptableObject : ScriptableObject
{
    public List<Vector3> path;
}
