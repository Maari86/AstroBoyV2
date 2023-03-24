using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1;
    [SerializeField] private AudioClip alien;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            SoundManager.instance.PlaySound(alien);
         
            // Get the EnemyHealth component from the collided object and take damage
            EnemyHealth currentHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (currentHealth != null)
            {
                currentHealth.TakeDamage(damage);
            }

            // Destroy the bullet game object
            Destroy(gameObject);
        }
        else if (other.CompareTag("Gems"))
        {
            // Find the player object and get its PlayerHealth component
            GameObject Astronut = GameObject.FindGameObjectWithTag("Player");
            Health playerHealth = Astronut.GetComponent<Health>();

            // If the player has a PlayerHealth component, take damage
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Destroy the gems game object
            Destroy(other.gameObject);

            // Destroy the bullet game object
            Destroy(gameObject);
        }
    }
}