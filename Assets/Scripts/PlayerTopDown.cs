using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerTopDown : MonoBehaviour
{

    // Movement variables
    public float walkSpeed = 4f;
    Vector2 movementInput;

    // Bullet Variables
    float fireGun;
    bool shotCooldown;
    public float bulletFireRate = 1.5f;
    [SerializeField] GameObject bulletPrefab; 
    Vector2 shotDir = Vector2.right; // Sets the direction to fire bullet


    // Health Variables
    bool healthCooldown;
    public int health = 3;
    public float healthDamageCooldown = 1.5f;


    

    // set the direction of Player model
    bool directionLeft { get { 
            
            if(shotDir.x < 0) return true; 
            else return false;
    }}


    private void FixedUpdate()
    {
        // movement for wasd arrows and joystick1 - Fire1 is Space & A button
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        fireGun = Input.GetAxisRaw("Fire1");

        // Fire bullet
        if (fireGun != 0 && !shotCooldown)
        {
            StartCoroutine(FireShot1());
        }


        transform.Translate(movementInput.normalized * walkSpeed * Time.deltaTime);



        // converts movement points to ints so the bullets have correct spawn point
        if (movementInput != Vector2.zero) shotDir = new Vector2(Mathf.CeilToInt(movementInput.x), Mathf.CeilToInt(movementInput.y));
        GetComponent<SpriteRenderer>().flipX = directionLeft;


        if(health <= 0)
        {
            Debug.Log("Player Dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    IEnumerator FireShot1()
    {
        shotCooldown = true;
        GameObject prefab = Instantiate(bulletPrefab);
        prefab.transform.position = transform.position;
        prefab.GetComponent<bullet>().moveDirection = shotDir;
        yield return new WaitForSeconds(bulletFireRate);

        shotCooldown = false;

    }

    IEnumerator HealthCoolDown(int enemDamage)
    {
        healthCooldown = true;
        health -= enemDamage;
        yield return new WaitForSeconds(healthDamageCooldown);
        healthCooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!healthCooldown && collision.collider.GetComponent<Enemy>())
        {
            StartCoroutine(HealthCoolDown(collision.collider.GetComponent<Enemy>().damage));
        }

        if(!healthCooldown && collision.collider.GetComponent<EnemyBullet>())
        {
            StartCoroutine(HealthCoolDown(collision.collider.GetComponent<EnemyBullet>().damage));
        }
    }
}

