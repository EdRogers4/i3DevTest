using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Scripts")]
    public CameraController scriptCameraController;
    public Outline scriptOutline; //Asset Provided By: https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488

    [Header("Part Data")]
    public bool isSelected;
    public int partID;

    [Header("Camera Data")]
    public float zoomRange;
    public Transform zoomPoint;

    [Header("Renderer")]
    public int colorVariant;
    public Material materialLights;
    public Material materialLightsGlow;
    private Renderer meshRenderer;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<Renderer>();
    }

    public void OnSelect()
    {
        isSelected = true;

        if (colorVariant != 2)
        {
            meshRenderer.material.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            meshRenderer.material = materialLightsGlow;
        }
    }

    public void OnUnselect()
    {
        isSelected = false;
        OnMouseExit();
    }

    private void OnMouseEnter() //Code Reference: https://www.youtube.com/watch?v=fw7h3UBgNW4
    {
        scriptOutline.enabled = true;
    }

    public void OnMouseExit() //Code Reference: https://www.youtube.com/watch?v=fw7h3UBgNW4
    {
        scriptOutline.enabled = false;

        if (!isSelected)
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
    }
}
