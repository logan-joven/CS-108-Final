using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerShootScript : MonoBehaviour
{
    Vector3 turretDirection;
    Vector2 pointerPosition;

    public Camera mainCamera;
    public Transform turretParent;

    public float turretRotationSpeed = 150;

    [Space(10)]
    [Header("Bullet Variables")]
    float fireGun;
    bool shotCooldown;
    public float bulletFireRate = 1.5f;
    [SerializeField] GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        pointerPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        fireGun = Input.GetAxisRaw("Fire1");

        // Fire bullet
        if (fireGun != 0 && !shotCooldown)
        {
            StartCoroutine(FireShot1());
        }
    }

    private void FixedUpdate()
    {
        turretDirection = (Vector3)pointerPosition - turretParent.position;

        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;

        var rotationStep = turretRotationSpeed * Time.deltaTime;

        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle), rotationStep);

        //shotDir = new Vector2(pointerPosition.x - turretParent.position.x, pointerPosition.y - turretParent.position.y);
    }

    IEnumerator FireShot1()
    {
        shotCooldown = true;
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(bulletFireRate);

        shotCooldown = false;

    }
}
