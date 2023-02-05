using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using FeTo.SOArchitecture;
using FeTo.ObjectPool;
using System.Diagnostics;

public class DefenseMouse : MonoBehaviour
{
    [SerializeField] Map map;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject follower;

    [SerializeField] FloatVariable rootsResource;
    [SerializeField] FloatVariable treesResource;

    [SerializeField] SquirrelSpawner squirrelSpawner;
    [SerializeField] WhopingWillowSpawner willowSpawner;

    private GameObject previousGO;

    private SelectedActions currentAction = SelectedActions.Squirrel;

    void Update() {
        Vector3 mousePos = Mouse.current.position.ReadValue();

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        int layerMask = LayerMask.GetMask("Floor");
        if (Physics.Raycast(ray, out hit, 20, layerMask)) {
            if (previousGO != hit.collider.gameObject) {
                TileController tile = hit.collider.gameObject.GetComponent<TileController>();
                previousGO = hit.collider.gameObject;
                follower.SetActive(true);
                follower.transform.position = tile.transform.position + new Vector3(0, -.2f, 0);
            }
        }
        else {
            if (previousGO != null) {
                previousGO = null;
                follower.SetActive(false);
            }
        }
    }

    public void SquirrelSelected() {
        currentAction = SelectedActions.Squirrel;
    }
    
    public void WhompingSelected() {
        currentAction = SelectedActions.WhompingWillow;
    }

    public void PlayerClick() {
        if (previousGO == null) { return; }
        TileController tile = previousGO.GetComponent<TileController>();
        if (tile.GetTileType == TileType.PathTile || tile.HasTurret) {
            return;
        }
        if (treesResource.Value <= 0) {
            return;
        }

        switch(currentAction){
            case SelectedActions.Squirrel:
                squirrelSpawner.SpawnSquirrel(tile.transform);
                break;
            case SelectedActions.WhompingWillow:
                willowSpawner.SpawnWhompingWillow(tile.transform);
                break;
        }

        treesResource.ApplyChange(-1);
        tile.PlaceTurret();
    }
}
