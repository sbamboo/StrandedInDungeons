using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmooth : MonoBehaviour
{
    //Define values
    public Transform Target;
    public Transform Camera;
    public Vector2 offset;
    public bool doSmooth = true;
    public float smoothCameraSpeed = 0.1f;

    // Called on start
    void Start()
    {
        //Clone y and x position while keeping z position
        Camera.transform.position = new Vector3((Target.position.x + offset.x), (Target.position.y + offset.y), Camera.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (doSmooth)
        {
            //Smooth camera
            Vector3 finalPosition = new Vector3((Target.position.x + offset.x), (Target.position.y + offset.y), Camera.transform.position.z);
            Vector3 lerpPosition = Vector3.Lerp(Camera.transform.position, finalPosition, smoothCameraSpeed * Time.fixedDeltaTime);
            Camera.transform.position = lerpPosition;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!doSmooth)
        {
            //Non Smooth camera
            //Clone y and x position while keeping z position
            Camera.transform.position = new Vector3((Target.position.x + offset.x), (Target.position.y + offset.y), Camera.transform.position.z);
        }
    }
}
