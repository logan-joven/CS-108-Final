using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera and Player Objects")]
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;

    [Space(10)]
    [Header("Camera Parameters")]
    public float camMinXPosition;
    public float camMaxXPosition;
    public float camMinYPosition;
    public float camMaxYPosition;

    private void Update()
    {
        Vector3 desiredPosition = new Vector3(Mathf.Clamp(player.transform.position.x, camMinXPosition, camMaxXPosition), Mathf.Clamp(player.transform.position.y, camMinYPosition, camMaxYPosition), cam.transform.position.z);
        cam.transform.position = desiredPosition;

    }
}
