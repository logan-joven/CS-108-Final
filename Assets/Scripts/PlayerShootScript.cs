using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootScript : MonoBehaviour
{
    Vector3 turretDirection;
    Vector2 pointerPosition;


    public Camera mainCamera;
    public Transform turretParent;

    public float turretRotationSpeed = 150;


    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        pointerPosition = mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private void FixedUpdate()
    {
        turretDirection = (Vector3)pointerPosition - turretParent.position;

        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;

        var rotationStep = turretRotationSpeed * Time.deltaTime;

        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);
    }
}
