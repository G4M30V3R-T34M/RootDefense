using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class TechTreeMouse : MonoBehaviour
{
    [SerializeField] Map map;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject follower;

    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI extraRewardText;
    [SerializeField] Image extraRewardImage;

    [SerializeField] Sprite squirelSprite, brambleSprite, mushroomSprite, whoopingwillowSprite;

    private GameObject previousGO, currentGO;

    private void Start() {
        EmptyValues();
    }

    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Debug.Log($"Mouse {mousePos.x}, {mousePos.y}, {mousePos.z}");

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        int layerMask = LayerMask.GetMask("Floor");
        if (Physics.Raycast(ray, out hit, 20, layerMask)) {
            if (previousGO != hit.collider.gameObject) {
                TileController tile = hit.collider.gameObject.GetComponent<TileController>();
                SetCostValues(tile);
            }
            follower.transform.position = hit.point;
        } else {
            if (previousGO != null) {
                EmptyValues();
                previousGO = null;
            }
        }
    }

    private void EmptyValues() {
        costText.SetText("");
        extraRewardText.gameObject.SetActive(false);
        extraRewardImage.gameObject.SetActive(false);
    }

    private void SetCostValues(TileController tile) {

        TileController closestTile = map.GetClosestReachable(tile.Row, tile.Column);
        if (closestTile != null) {
            int cost = closestTile.DistanceToTree + 1; 
            costText.SetText(cost.ToString());
        } else {
            costText.SetText("---");
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
