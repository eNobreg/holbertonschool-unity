using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerBase : MonoBehaviour
{

    public float cameraMoveSpeed = 120.0f;
    public GameObject cameraFollowObj;
    Vector3 followPos;
    public float clampAngle = 80.0f;
    public float inputSens = 150.0f;
    public GameObject cameraObj;
    public GameObject player;
    public Vector3 respawPos;
    public float CamDistanceX;
    public float CamDistanceZ;
    public float camDistanceY;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;

    private float rotY = 0.0f;
    private float rotX = 0.0f;
    public bool isInverted;

    public bool animationPlaying = true;
    void Start()
    {
        respawPos = transform.position;
        respawPos.y += 15;
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("isInverted") == 1)
            isInverted = true;
        else
            isInverted = false;

        if (Input.GetMouseButton(1) && !animationPlaying)
        {
            float inputX = Input.GetAxis("RightStickHorizontal");
            float inputZ = Input.GetAxis("RightStickVertical");

            mouseX = Input.GetAxis("Mouse X");
            if (isInverted)
                mouseY = Input.GetAxis("Mouse Y") * -1;
            else
                mouseY = Input.GetAxis("Mouse Y");

            finalInputX = inputX + mouseX;
            finalInputZ = inputZ + mouseY;


            rotY += finalInputX * inputSens * Time.deltaTime;
            rotX += finalInputZ * inputSens * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        Transform target = cameraFollowObj.transform;
        // move towards game object

        float step = cameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
