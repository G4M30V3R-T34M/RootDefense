using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretScriptableObject", menuName = "Scriptables/TurretScriptableObject", order = 1)]
public class TurretScriptableObject : ScriptableObject
{
    [Header("Common configuration")]
    public TurretType turretType;
    public int damage;
    public int range;
    public TileType validTile;

    [Header("Time configurations in seconds")]
    public float slowDown;
    [Tooltip("For the attack")]
    public float cooldown;
    [Tooltip("For the effect of the attack")]
    public float hitEach;

    [Header("Area configurations")]
    public bool areaEffect;
    public int areaRange;
}