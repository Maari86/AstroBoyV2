using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    [SerializeField] private AudioClip die;
    [SerializeField] private AudioClip hurt;
    public float currentHealth { get; private set; }

    private Animator anim;
    private bool dead;
    public float Addhealth;
    private bool invulnerable;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfflashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    // [Header("Death Sound")]
    //  [SerializeField] private AudioClip deathSource;

    //  [Header("Hurt Sound")]
    //  [SerializeField] private AudioClip hurtSource;

    public static event Action OnPlayerDeath;



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();

    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);


        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            StartCoroutine(Invunerability());
            SoundManager.instance.PlaySound(hurt);
        }
        else
        {
            if (!dead)
            {
                // Show the rewarded ad button:
                
                anim.SetTrigger("Die");
                //Deactive all attached Component
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;
                SoundManager.instance.PlaySound(die);

                OnPlayerDeath?.Invoke();
            }

        }
    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);

    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("PlayerIdle");
        StartCoroutine(Invunerability());

        //Active all attached Component
        foreach (Behaviour component in components)
            component.enabled = true;
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfflashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfflashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    
   



}
