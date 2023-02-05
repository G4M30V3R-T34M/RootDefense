using FeTo.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

public class BaseTurret : PoolableObject
{
    [Header("Scriptable Levels")]
    [SerializeField] 
    private TurretScriptableObject level1Config;
    [SerializeField] 
    private TurretScriptableObject level2Config;
    [SerializeField] 
    private TurretScriptableObject level3Config;

    [Header("Own Components")]
    [SerializeField]
    protected SphereCollider turretCollider;
    [SerializeField]
    protected Animator animator;

    private int currentLevel = 1;
    private int maxLevel = 3;
    protected TurretScriptableObject currentSettings;

    Dictionary<int, TurretScriptableObject> confiByLevel = new Dictionary<int, TurretScriptableObject>();

    protected List<GameObject> targets = new List<GameObject>();
    protected float timeFromLastAttack;

    private void Awake() {
        // Init level dictionary
        InitLevelDict();
        currentSettings = confiByLevel[currentLevel];
    }

    protected void Start() {
        turretCollider.radius = currentSettings.range;
        timeFromLastAttack = currentSettings.cooldown;
    }

    private void InitLevelDict() {
        confiByLevel.Add(1, level1Config);
        confiByLevel.Add(2, level2Config);
        confiByLevel.Add(3, level3Config);
    }

    // This will be a suscribed to an event
    public void Upgrade() {
        if (CanBeUpgraded()) {
            currentLevel += 1;
            currentSettings = confiByLevel[currentLevel];
        }
    }

    public bool CanBeUpgraded() {
        return currentLevel < maxLevel;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == (int)Layer.Enemy) {
            targets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == (int)Layer.Enemy) {
            targets.Remove(other.gameObject);
        }
    }

    protected bool CanAttack() {
        return timeFromLastAttack >= currentSettings.cooldown && targets.Count > 0;
    }
}
