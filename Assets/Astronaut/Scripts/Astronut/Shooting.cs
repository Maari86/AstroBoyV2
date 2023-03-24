using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Set up the bullet's properties
    public float bulletSpeed = 10f;
    public float bulletWidth = 10f;
    public float bulletHeight = 10f;
    public Animator animator;
    public GameObject bulletPrefab;
    [SerializeField] private AudioClip bulletSound;
    public Joystick joystick;


    // Set up the player's position and speed
    public float playerSpeed = 5f;

    // Set up the fire point
    public Transform firePoint;

    // Store the direction of the arrow key pressed
    private Vector2 arrowKeyDirection;

    public float fireRate = 0.5f; // Time between bullets (in seconds)
    private float timer = 0.0f; // Time since last bullet was fired

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        // Move the player using the arrow keys
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * playerSpeed * Time.deltaTime);
        timer += Time.deltaTime;

        // Only spawn a bullet when the arrow keys and left mouse button are pressed
        /*if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Mouse0) && (horizontalInput != 0 || verticalInput != 0))
    
        {
            if (timer >= fireRate)
            {
                // Spawn a new bullet
                SpawnBullet();

                // Reset the timer
                timer = 0.0f;
            }
        }*/

        // Store the direction of the arrow key pressed
        if (horizontalInput > 0)
        {
            // Right arrow key pressed, set the direction to the right
            arrowKeyDirection = Vector2.right;
        }
        else if (horizontalInput < 0)
        {
            // Left arrow key pressed, set the direction to the left
            arrowKeyDirection = Vector2.left;
        }
        else if (verticalInput > 0)
        {
            // Up arrow key pressed, set the direction upwards
            arrowKeyDirection = Vector2.up;
        }
        else if (verticalInput < 0)
        {
            // Down arrow key pressed, set the direction downwards
            arrowKeyDirection = Vector2.down;
        }
    }

    public void FixedUpdate()
    {
        // Get the bullet's current velocity
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        // If the bullet is moving horizontally (x velocity is non-zero)
        if (velocity.x != 0)
        {
            // Set the bullet's y position to 0
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    public void SpawnBullet()
    {
        SoundManager.instance.PlaySound(bulletSound);
        // Only spawn the bullet if arrow keys and mouse left button are pressed
        if (arrowKeyDirection != Vector2.zero)
        {
            // Create a new bullet object and set its position
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Set the bullet's velocity
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = arrowKeyDirection * bulletSpeed;

            // Destroy the bullet game object after 3 seconds
            Destroy(bullet, 1f);
        }
    }

    public void OnShootButtonClicked()
    {
        if (joystick.Horizontal !=0 || joystick.Vertical != 0)
        {
            if (timer >= fireRate)
            {
                SpawnBullet();


                timer = 0.0f;
            }
        }
        
    }

}
