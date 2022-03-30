using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public CameraController scriptCameraController;
    public TextMeshProUGUI textLabel;
    public Target[] scriptTarget;
    public Target currentTarget;
    public GameObject[] selectIcon;
    public string[] namePart;
    private Animator animator;
    private bool isLabel;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (scriptCameraController.isTargetSelected && !isLabel)
        {
            isLabel = true;
            animator.SetBool("isLabel", true);
        }
        else if (!scriptCameraController.isTargetSelected && isLabel)
        {
            isLabel = false;
            animator.SetBool("isLabel", false);
        }
    }

    public void ButtonPartSelect(int part)
    {
        if (currentTarget != null)
        {
            currentTarget.OnUnselect();
            selectIcon[currentTarget.partID].SetActive(false);
        }

        currentTarget = scriptTarget[part];
        scriptCameraController.targetTransform = scriptTarget[part].transform;
        scriptTarget[part].OnSelect();
        selectIcon[currentTarget.partID].SetActive(true);

        if (scriptTarget[part].zoomPoint != null)
        {
            scriptCameraController.zoomPoint = scriptTarget[part].zoomPoint;
        }
        else
        {
            scriptCameraController.zoomPoint = scriptTarget[part].transform;
        }

        scriptCameraController.PositionCamera();
        LabelScrollText(part); 
    }

    public void LabelScrollText(int part)
    {
        textLabel.text = namePart[part];
    }
}
