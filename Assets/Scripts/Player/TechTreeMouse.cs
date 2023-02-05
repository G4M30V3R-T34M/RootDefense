using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TechTreeMouse : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject follower;

    void Update()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Debug.Log($"Mouse {mousePos.x}, {mousePos.y}, {mousePos.z}");

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        int layerMask = LayerMask.GetMask("Floor");
        if (Physics.Raycast(ray, out hit, 20, layerMask)) {
            follower.transform.position = hit.point;
        }
    }
}
