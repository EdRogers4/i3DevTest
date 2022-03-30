using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Scripts")]
    public CameraController scriptCameraController;
    public SoundManager scriptSoundManager;

    [Header("Text")]
    public TextMeshProUGUI textLabel;
    public GameObject[] selectIcon;
    public string[] namePart;
    private float textSpeed = 0.025f;
    private Animator animator;
    private bool isLabel;

    [Header("Targets")]
    public Target[] scriptTarget;
    public Target currentTarget;

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

        scriptSoundManager.PlaySoundButton();
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
        StartCoroutine(AnimateText(part)); 
    }

    public IEnumerator AnimateText(int part) //Code Reference: https://www.youtube.com/watch?v=Z8efnBeXHeQ
    {
        textLabel.text = "";
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < namePart[part].Length + 1; i++)
        {
            textLabel.text = namePart[part].Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
