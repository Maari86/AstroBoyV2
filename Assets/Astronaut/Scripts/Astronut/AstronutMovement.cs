using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronutMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private Animator anim;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private float attackCooldown;
    public Joystick joystick;
    [SerializeField] private GameObject rangeObject;
    [SerializeField] private AudioClip collect;
   

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = joystick.Vertical;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(1, 1, 1);

        float VerticalInput = joystick.Horizontal;
        body.velocity = new Vector2(VerticalInput * speed, body.velocity.x);

        if (VerticalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (VerticalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        anim.SetBool("Moving", VerticalInput != 0);
        anim.SetBool("Up", horizontalInput > 0);
        anim.SetBool("Down", horizontalInput < 0);

        float xOffset = 0f;
        float yOffset = 0f;

        if (VerticalInput > 0.01f)
            xOffset = 1f;
        else if (VerticalInput < -0.01f)
            xOffset = -1f;

        if (horizontalInput > 0.01f)
            yOffset = 1f;
        else if (horizontalInput < -0.01f)
            yOffset = -1f;

        rangeObject.transform.position = transform.position + new Vector3(xOffset, yOffset, 0f);

        if (Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.DownArrow) && cooldownTimer > attackCooldown)
            Attack();

        if (Input.GetMouseButton(1) && Input.GetKeyDown(KeyCode.DownArrow) && cooldownTimer > attackCooldown)
           Collect();

        cooldownTimer += Time.deltaTime;
    }
    public void Attack()
    {
        float horizontalInput = Input.GetAxis("Vertical");
        float VerticalInput = Input.GetAxis("Horizontal");

        if (horizontalInput == 0 && VerticalInput == 0)
        {
            anim.SetTrigger("attack");
            cooldownTimer = 0;
        }

    }

    public void Collect()
    {
        anim.SetTrigger("collect");
        cooldownTimer = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(rangeObject.transform.position, 1f, LayerMask.GetMask("EnergyBall"));
        foreach (Collider2D collider in colliders)
        {
            CollectEnergyballs.Collected += 1;
            Destroy(collider.gameObject);
            SoundManager.instance.PlaySound(collect);
        }

    }
   
}