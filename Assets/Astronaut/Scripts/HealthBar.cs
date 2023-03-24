using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public Health playerHealth;

    private void Start()
    {
        playerHealth = FindObjectOfType<UIManager>().GetSelectedPlayer().GetComponent<Health>();
        healthBarImage.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }

    private void Update()
    {
        healthBarImage.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }
}
