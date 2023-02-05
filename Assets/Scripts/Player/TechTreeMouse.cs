using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using FeTo.SOArchitecture;
using FeTo.ObjectPool;

public class TechTreeMouse : MonoBehaviour
{
    [SerializeField] Map map;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject follower;

    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI extraRewardText;
    [SerializeField] Image extraRewardImage;

    [SerializeField] FloatVariable rootsResource;
    [SerializeField] FloatVariable treesResource;

    [SerializeField] Sprite squirelSprite, brambleSprite, mushroomSprite, whoopingwillowSprite;

    [SerializeField] ObjectPool rootsPool;

    private GameObject previousGO, currentGO;

    private void Start() {
        EmptyValues();
    }

    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        int layerMask = LayerMask.GetMask("Floor");
        if (Physics.Raycast(ray, out hit, 20, layerMask)) {
            if (previousGO != hit.collider.gameObject) {
                TileController tile = hit.collider.gameObject.GetComponent<TileController>();
                SetCostValues(tile);
                previousGO = hit.collider.gameObject;
                follower.SetActive(true);
                follower.transform.position = tile.transform.position + new Vector3(0, -.2f, 0);
            }
        } else {
            if (previousGO != null) {
                EmptyValues();
                previousGO = null;
                follower.SetActive(false);
            }
        }
    }

    public void PlayerClick() {
        if (previousGO == null) { return; }
        TileController tile = previousGO.GetComponent<TileController>();
        if (tile.HasRoot || !map.IsReachable(tile.Row, tile.Column)) { return; }
        TileController closest = map.GetClosestReachable(tile.Row, tile.Column);
        if (closest.DistanceToTree + 1 > rootsResource.Value) { return; }

        rootsResource.ApplyChange(-1 * (closest.DistanceToTree + 1));

        RootController root = (RootController) rootsPool.GetNext();
        root.SetOriginAndDestination(closest, tile);
        root.ReadyRoot();
        root.gameObject.SetActive(true);
        root.GrowRoot();
    }

    private void EmptyValues() {
        costText.SetText("");
        extraRewardText.gameObject.SetActive(false);
        extraRewardImage.gameObject.SetActive(false);
    }

    private void SetCostValues(TileController tile) {
        if (tile.HasRoot) {
            costText.SetText("---");
        } else {
            TileController closestTile = map.GetClosestReachable(tile.Row, tile.Column);
            if (closestTile != null) {
                int cost = closestTile.DistanceToTree + 1; 
                costText.SetText(cost.ToString());
            } else {
                costText.SetText("---");
            }
        }

        if (tile.HasReward) {
            extraRewardText.gameObject.SetActive(true);
            switch (tile.Reward) {
                case TurretType.Squirrel:
                    extraRewardImage.sprite = squirelSprite;
                    break;
                case TurretType.Bramble:
                    extraRewardImage.sprite = brambleSprite;
                    break;
                case TurretType.Toadstool:
                    extraRewardImage.sprite = mushroomSprite;
                    break;
                case TurretType.WhompingWillow:
                    extraRewardImage.sprite = whoopingwillowSprite;
                    break;
            }
            extraRewardImage.gameObject.SetActive(true);
        } else {
            extraRewardText.gameObject.SetActive(false);
            extraRewardImage.gameObject.SetActive(false);
        }
    }

}
