using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FeTo.ObjectPool;
using FeTo.SOArchitecture;

public class RootController : PoolableObject
{
    [SerializeField] FloatReference growthRate;
    TileController originTile, destinationTile;

    public void SetOriginAndDestination (TileController origin, TileController destination) {
        originTile = origin;
        destinationTile = destination;
    }

    public void ReadyRoot() {
        transform.LookAt(new Vector3(
            destinationTile.transform.position.x,
            -0.2f,
            destinationTile.transform.position.z));
        transform.localScale = Vector3.zero;
    }

    public void GrowRoot() {
        destinationTile.StartConnect();
        StartCoroutine(DoGrowRoot());
    }

    IEnumerator DoGrowRoot() {
        float scaleIdx = 0f;
        while (scaleIdx < 1f) {
            scaleIdx += growthRate * Time.deltaTime;
            transform.localScale = Vector3.one * scaleIdx;
            yield return null;
        }

        destinationTile.FinishConnect(originTile.DistanceToTree + 1);
    }


}
