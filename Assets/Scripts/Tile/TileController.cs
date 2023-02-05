using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FeTo.SOArchitecture;

public class TileController : MonoBehaviour
{
    [SerializeField] private bool hasRoot;
    [SerializeField] private bool hasTurret;
    [SerializeField] FloatVariable resourcesToGrowTrees;
    [SerializeField] FloatVariable squirelMaxLevel, mushroomMaxLevel, brambleMaxLevel, whoopingwhillowMaxLevel;

    [SerializeField] GameObject rewardIndicator;

    private const int RESOURE_REWARD = 1;

    private bool hasReward = false;
    private TurretType reward;

    Animator animator;
    private const string TRIGGER_CONSUME = "Consume";

    public bool HasRoot { get { return hasRoot; } }
    public bool HasTurret { get { return hasTurret; } }
    public bool HasReward { get { return hasReward; } }

    private void Awake() {
        rewardIndicator.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void SetReward(TurretType turretType) {
        hasReward = true;
        rewardIndicator.SetActive(true);
        reward = turretType;
    }

    public void GetReward() {
        resourcesToGrowTrees.ApplyChange(RESOURE_REWARD);

        if (hasReward) {
            UpgradeTurretLevel();
            animator.SetTrigger(TRIGGER_CONSUME);
        }
    }

    private void  UpgradeTurretLevel() {
        switch (reward) {
            case TurretType.Squirrel:
                squirelMaxLevel.ApplyChange(1);
                break;
            case TurretType.Toadstool:
                mushroomMaxLevel.ApplyChange(1);
                break;
            case TurretType.Bramble:
                brambleMaxLevel.ApplyChange(1);
                break;
            case TurretType.WhompingWillow:
                whoopingwhillowMaxLevel.ApplyChange(1);
                break;
        }
    }
}
