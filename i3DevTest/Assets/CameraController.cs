using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isRightClick;
    private Vector3 mousePosition;
    private Vector3 mousePositionPrevious;
    public float speedCameraRotate;

    void Start()
    {
        mousePosition = new Vector3(Screen.currentResolution.width, 0f, 0f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightClick = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightClick = false;
        }

        if (isRightClick) //Hold right click to rotate camera in the direction of mouse
        {
            mousePosition = Input.mousePosition;

            if (mousePosition.x > mousePositionPrevious.x)
            {
                speedCameraRotate = 90.0f;
            }
            else if (mousePosition.x < mousePositionPrevious.x)
            {
                speedCameraRotate = -90.0f;
            }

            transform.RotateAround(transform.position, transform.up, Time.deltaTime * speedCameraRotate);
            mousePositionPrevious = mousePosition;
        }
        else //Decelerate camera rotation after releasing right click
        {
            if (speedCameraRotate > 0f || speedCameraRotate < 0f)
            {
                if (speedCameraRotate > 0f)
                {
                    speedCameraRotate -= 3.0f;
                }
                else if (speedCameraRotate < 0f)
                {
                    speedCameraRotate += 3.0f;
                }
            }

            transform.RotateAround(transform.position, transform.up, Time.deltaTime * speedCameraRotate);
        }
    }
}
