using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public CameraController scriptCameraController;
    public int partID;
    public int colorVariant;
    public float zoomRange;
    public Transform zoomPoint;
    public Material materialLights;
    public Material materialLightsGlow;
    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<Renderer>();
    }

    private void OnMouseEnter() //Code Reference: https://www.youtube.com/watch?v=fw7h3UBgNW4
    {
        if (!scriptCameraController.isTargetSelected)
        {
            if (colorVariant == 2)
            {
                meshRenderer.material = materialLights;
            }

            meshRenderer.material.color = new Color(0.7843f, 1.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseExit() //Code Reference: https://www.youtube.com/watch?v=fw7h3UBgNW4
    {
        if (colorVariant == 0)
        {
            meshRenderer.material.color = Color.white;
        }
        else if (colorVariant == 1)
        {
            meshRenderer.material.color = new Color(0.4980f, 0.4980f, 0.4980f, 1.0f);
        }
        else if (colorVariant == 2)
        {
            meshRenderer.material = materialLightsGlow;
        }
    }

    public void OnSelect()
    {

    }
}
