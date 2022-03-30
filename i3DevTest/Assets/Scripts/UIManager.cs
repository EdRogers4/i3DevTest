using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public CameraController scriptCameraController;
    public TextMeshProUGUI textLabel;
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
        
    }

    public void LabelScrollText(int part)
    {
        textLabel.text = namePart[part];
    }
}
