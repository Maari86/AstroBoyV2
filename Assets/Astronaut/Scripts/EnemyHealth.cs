using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public bool isDistanceBasedHealth = true;
    [SerializeField] public float startingHealth;
    public float currentHealth;
    private Animator animate;
    private bool dead;
    public float addHealth;
    private bool invulnerable;
  


    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRender;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

  //[Header("Death Sound")]
  //[SerializeField] private AudioClip deathSource;

 // [Header("Hurt Sound")]
   //SerializeField] private AudioClip hurtSource;

    public static event Action OnPlayerDeath;

    private void Awake()
    {
        currentHealth = startingHealth;
        animate = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {      
            StartCoroutine(Invunerability());
         // SoundManager.instance.PlaySound(hurtSource);
        }
        else
        {
            if (!dead)
            { 
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                Destroy(gameObject);
                ScoreCounter.scoreValue += 10;
                OnPlayerDeath?.Invoke();
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        int i = 0;
        while(i < numberOfFlashes)
        {
            spriteRender.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRender.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            i++;
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }

}
