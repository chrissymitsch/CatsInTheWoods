using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float zoomSpeed;
    public float orthographicSizeMin;
    public float orthographicSizeMax;
    public float fovMin;
    public float fovMax;
    public bool lockCursor;
    public float mouseSensibilitaet = 10;
    public Transform target;
    public float dstFromTarget = 2;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = 0.12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation = new Vector3(0, 0, 0);

    private Camera myCamera;

    float yaw0;
    float pitch0;
    float yaw1;
    float pitch1;
    float mouseX;
    float mouseY;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GetComponent<Camera>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        if (myCamera.orthographic)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                myCamera.orthographicSize += zoomSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                myCamera.orthographicSize -= zoomSpeed;
            }
            myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, orthographicSizeMin, orthographicSizeMax);
        }
        else
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                myCamera.fieldOfView += zoomSpeed;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                myCamera.fieldOfView -= zoomSpeed;
            }
            myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView, fovMin, fovMax);
        }
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            yaw0 = yaw1;
            pitch0 = pitch1;
        }
        if (Input.GetMouseButton(0))
        {
            yaw0 += Input.GetAxis("Mouse X") * mouseSensibilitaet;
            pitch0 -= Input.GetAxis("Mouse Y") * mouseSensibilitaet;
            pitch0 = Mathf.Clamp(pitch0, pitchMinMax.x, pitchMinMax.y);
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch0, yaw0), ref rotationSmoothVelocity, rotationSmoothTime);
        }
        if (Input.GetMouseButton(1))
        {
            yaw1 += Input.GetAxis("Mouse X") * mouseSensibilitaet;
            pitch1 -= Input.GetAxis("Mouse Y") * mouseSensibilitaet;
            pitch1 = Mathf.Clamp(pitch1, pitchMinMax.x, pitchMinMax.y);
            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch1, yaw1), ref rotationSmoothVelocity, rotationSmoothTime);
        }
        else
        {
            transform.LookAt(target);
        }
        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * dstFromTarget;
    }
}
