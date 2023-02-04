using System.Collections.Generic;
using UnityEngine;

using Unity.Services.Core;
using Unity.Services.Authentication;

using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using UnityEditor.PackageManager;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using FeTo.ObjectPool;

[RequireComponent(typeof(HealthManager))]
public class Enemy : PoolableObject
{
    [SerializeField]
    private EnemyScriptableObject enemySettings;

    HealthManager healthManager;

    protected void Start() {
        healthManager = GetComponent<HealthManager>();
        healthManager.SetUp(enemySettings.health);
    }

    public void DieAction() {
        //TODO play anim
    }
}
