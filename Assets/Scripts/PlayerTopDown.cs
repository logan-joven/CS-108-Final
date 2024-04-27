using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerTopDown : MonoBehaviour
{

    [Header("Movement Variables")]
    public float maxSpeed = 100f;
    public Rigidbody2D rb;
    Vector2 movementInput;
    public float rotationSpeed = 100f;

<<<<<<< Updated upstream
    // Bullet Variables
    float fireGun;
    bool shotCooldown;
    public float bulletFireRate = 1.5f;
    [SerializeField] GameObject bulletPrefab; 
    Vector2 shotDir = Vector2.right; // Sets the direction to fire bullet

    // Animator
    public Animator animator;

    // Health Variables
=======
    [Space(10)]
    [Header("Health Variables")]
>>>>>>> Stashed changes
    bool healthCooldown;
    public int health = 3;
    public float healthDamageCooldown = 1.5f;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        //if (health <= 0)
        //    health = 3;
        //else
        //    health = StaticData.Health;
    }

    private void Update()
    {
        // Check if player is dead
        if (health <= 0)
        {
            Debug.Log("Player Dead");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // test methods, delete later
        if (Input.GetKeyDown(KeyCode.Z))
            StaticData.test++;
        if (Input.GetKeyDown(KeyCode.X))
            Debug.Log(StaticData.test);
    }



    private void FixedUpdate()
    {
        // movement for wasd arrows and joystick1 - Fire1 is Space & A button
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput.Normalize();

        rb.velocity = (Vector2)transform.up * maxSpeed * movementInput.y * Time.fixedDeltaTime;
        rb.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementInput.x * rotationSpeed * Time.fixedDeltaTime));
<<<<<<< Updated upstream
        
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

=======
>>>>>>> Stashed changes
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

