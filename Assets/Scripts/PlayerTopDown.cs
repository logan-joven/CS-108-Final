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

    // Animator
    public Animator animator;
    public Animator explode;

    // Health Variables
    [Space(10)]
    [Header("Health Variables")]
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
            explode.SetFloat("Explode", 1);
            StartCoroutine(Death());
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
        
        if(GetComponent<Rigidbody2D>().velocity != Vector2.zero){
           animator.SetFloat("Speed",1);
        } 
        else{
           animator.SetFloat("Speed",-1);
        }
    }

    IEnumerator HealthCoolDown(int enemDamage)
    {
        healthCooldown = true;
        health -= enemDamage;
        yield return new WaitForSeconds(healthDamageCooldown);
        healthCooldown = false;
    }

    IEnumerator Death(){
        yield return new WaitForSeconds(explode.GetCurrentAnimatorStateInfo(0).length);
        Debug.Log("Player Dead");
        SceneManager.LoadScene("Title");
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

