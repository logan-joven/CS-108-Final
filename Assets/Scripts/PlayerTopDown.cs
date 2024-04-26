using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerTopDown : MonoBehaviour
{

    // Movement variables
    public float maxSpeed = 100f;
    public Rigidbody2D rb;
    Vector2 movementInput;

    // Aiming Variables
    public float rotationSpeed = 100f;

    // Bullet Variables
    float fireGun;
    bool shotCooldown;
    public float bulletFireRate = 1.5f;
    [SerializeField] GameObject bulletPrefab; 
    Vector2 shotDir = Vector2.right; // Sets the direction to fire bullet

    // Animator
    public Animator animator;

    // Health Variables
    bool healthCooldown;
    public int health = 3;
    public float healthDamageCooldown = 1.5f;


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        // Fire bullet
        if (fireGun != 0 && !shotCooldown)
        {
            StartCoroutine(FireShot1());
        }

        // Check if player is dead
        if (health <= 0)
        {
            Debug.Log("Player Dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    private void FixedUpdate()
    {
        // movement for wasd arrows and joystick1 - Fire1 is Space & A button
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput.Normalize();
        fireGun = Input.GetAxisRaw("Fire1");


        rb.velocity = (Vector2)transform.up * maxSpeed * movementInput.y * Time.fixedDeltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementInput.x * rotationSpeed * Time.fixedDeltaTime));
        
        if(Vector2.Distance(rb.velocity, transform.position) > 0){
            animator.SetFloat("Speed",1);
        } 
        else{
            animator.SetFloat("Speed",-1);
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

    // Upon Colliding w/ an Melee Enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!healthCooldown && collision.collider.GetComponent<Enemy>())
        {
            StartCoroutine(HealthCoolDown(collision.collider.GetComponent<Enemy>().damage));
        }
    }
}

