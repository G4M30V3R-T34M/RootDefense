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
    [SerializeField] private int distanceToTree = -1;

    Animator animator;
    private const string TRIGGER_CONSUME = "Consume";

    private int row, column;

    public bool HasRoot { get { return hasRoot; } }
    public bool HasTurret { get { return hasTurret; } }
    public bool HasReward { get { return hasReward; } }
    public int DistanceToTree { get { return distanceToTree; } }
    public int Row { get { return row; } }
    public int Column { get { return column; } }
    public TurretType Reward { get { return reward; } }


    private void Awake() {
        rewardIndicator.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void SetReward(TurretType turretType) {
        hasReward = true;
        rewardIndicator.SetActive(true);
        reward = turretType;
    }

    private void GetReward() {
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

    public void StartConnect() {
        hasRoot = true;
    }

    public void FinishConnect(int distance) {
        distanceToTree = distance;
        GetReward();
    }

    public void SetCoordinates(int row, int col) {
        this.row = row;
        this.column = col;
    }
}
