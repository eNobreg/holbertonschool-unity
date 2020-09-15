using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float cameraSpeed = 2.0f;
    public bool afterStart = true;

    // Update is called once per frame
    void Update () 
    {
        if (afterStart)
        {
            var tCameraPosn = transform.localPosition;
            transform.Translate((Vector2.right * (Time.deltaTime * cameraSpeed)));
        }
    }
}
