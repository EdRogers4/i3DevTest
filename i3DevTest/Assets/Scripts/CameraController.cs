using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cameraMain;
    public Transform cameraMainOrigin;
    public Transform targetTransform;
    private Transform zoomPoint;
    public bool isTargetSelected;
    private bool isCameraZoomed;
    private bool isRightClick;
    private Vector3 mousePosition;
    private Vector3 mousePositionPrevious;
    private float speedCameraOrbit;
    private float speedCameraRotate = 20.0f;
    private float speedCameraZoom = 15f;
    private float zoomRange = 3.5f;
    private float distanceZoomPoint;
    private float distanceOriginPoint;

    void Start()
    {
        mousePosition = new Vector3(Screen.currentResolution.width, 0f, 0f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectPart();
        }

        if (targetTransform != null && !isCameraZoomed)
        {
            ZoomCameraIn();
        }
        else if (targetTransform == null && distanceOriginPoint > 0.25f)
        {
            ZoomCameraOut();
        }

        if (Input.GetMouseButtonDown(1))
        {
            isRightClick = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightClick = false;
        }

        if (isRightClick && !isCameraZoomed)
        {
            OrbitCamera();
        }
        else if (isRightClick && isTargetSelected)
        {
            UnselectPart();
        }
        else if (speedCameraOrbit > 0f || speedCameraOrbit < 0f)
        {
            DecelerateCamera();
        }
    }

    private void SelectPart()
    {
        //Left click to select car part - Code Referenced: https://www.youtube.com/watch?v=fw7h3UBgNW4
        Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform.tag == "Car")
            {
                targetTransform = hit.transform;
                zoomPoint = hit.transform.GetComponent<Target>().zoomPoint;
                zoomRange = hit.transform.GetComponent<Target>().zoomRange;
                hit.transform.GetComponent<Target>().OnMouseExit();

                if (zoomPoint == null)
                {
                    zoomPoint = hit.transform;
                }

                if (!isTargetSelected)
                {
                    isTargetSelected = true;
                }
                else if (isTargetSelected && hit.transform != targetTransform)
                {
                    isCameraZoomed = false;
                }
                else if (isTargetSelected && hit.transform == targetTransform)
                {
                    UnselectPart();
                }
            }
            else if (isTargetSelected)
            {
                UnselectPart();
            }
        }
    }

    private void ZoomCameraIn()
    {
        //Zoom camera in on selected part
        distanceZoomPoint = Vector3.Distance(cameraMain.transform.position, zoomPoint.position);

        if (distanceZoomPoint > zoomRange)
        {
            float step = speedCameraZoom * Time.deltaTime;
            cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, zoomPoint.position, step);
            distanceOriginPoint = Vector3.Distance(cameraMain.transform.position, cameraMainOrigin.position);

            //Code Reference: https://docs.unity3d.com/ScriptReference/Vector3.RotateTowards.html
            Vector3 targetDirection = targetTransform.position - cameraMain.transform.position;
            float rotateStep = speedCameraRotate * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(cameraMain.transform.forward, targetDirection, rotateStep, 0.0f);
            cameraMain.transform.rotation = Quaternion.LookRotation(newDirection);
        }
        else
        {
            isCameraZoomed = true;
        }
    }

    private void ZoomCameraOut()
    {
        //Move camera back to origin
        float step = speedCameraZoom * Time.deltaTime;
        cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, cameraMainOrigin.position, step);

        //Code Reference: https://docs.unity3d.com/ScriptReference/Quaternion.RotateTowards.html
        float rotateStep = speedCameraRotate * 10f * Time.deltaTime;
        cameraMain.transform.rotation = Quaternion.RotateTowards(cameraMain.transform.rotation, cameraMainOrigin.rotation, rotateStep);
    }

    private void OrbitCamera()
    {
        //Hold right click to rotate camera in the direction of mouse
        mousePosition = Input.mousePosition;

        if (mousePosition.x > mousePositionPrevious.x)
        {
            speedCameraOrbit = 90.0f;
        }
        else if (mousePosition.x < mousePositionPrevious.x)
        {
            speedCameraOrbit = -90.0f;
        }

        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speedCameraOrbit);
        mousePositionPrevious = mousePosition;
    }

    private void DecelerateCamera()
    {
        //Decelerate camera rotation after releasing right click
        if (speedCameraOrbit > 0f || speedCameraOrbit < 0f)
        {
            if (speedCameraOrbit > 0f)
            {
                speedCameraOrbit -= 3.0f;
            }
            else if (speedCameraOrbit < 0f)
            {
                speedCameraOrbit += 3.0f;
            }
        }

        transform.RotateAround(transform.position, transform.up, Time.deltaTime * speedCameraOrbit);
    }

    private void UnselectPart()
    {
        isTargetSelected = false;
        isCameraZoomed = false;
        targetTransform = null;
    }
}
