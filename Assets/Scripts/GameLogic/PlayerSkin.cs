using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public MeshFilter meshFilter;
    private void OnEnable()
    {
        if (PlayerData.currentTail != null)
            trailRenderer.material = PlayerData.currentTail;
        else
            Debug.LogWarning("no renderer");

        if (PlayerData.currentShape != null)
            meshFilter.mesh = PlayerData.currentShape;
        else
            Debug.LogWarning("no renderer");
    }
}
