using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronutAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private GameObject rangeObject;
    private Animator anim;
    private AstronutMovement astronutMovement;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private AudioClip collect;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        astronutMovement = GetComponent<AstronutMovement>();
    }

    private void Start()
    {
        // Set the initial position of the range object relative to the player's position
        rangeObject.transform.position = transform.position + new Vector3(2f, 0f, 0f);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.Mouse0) && cooldownTimer > attackCooldown && astronutMovement)
            Attack();

        if (Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse1) && cooldownTimer > attackCooldown && astronutMovement)
            Collect();

        // Update the position of the range object based on the player's facing direction and arrow key presses
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += Vector3.right;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection += Vector3.left;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += Vector3.up;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection += Vector3.down;
        }

        rangeObject.transform.position = transform.position + moveDirection.normalized * 2f;

        cooldownTimer += Time.deltaTime;
    }


    public void Attack()
    {
       
        anim.SetTrigger("attack");
        cooldownTimer = 0;
    }

    public void Collect()
    {  
        anim.SetTrigger("collect");
        cooldownTimer = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(rangeObject.transform.position, 1f, LayerMask.GetMask("Gems"));
        foreach (Collider2D collider in colliders)
        {
            CollectEnergyballs.Collected += 1;
            Destroy(collider.gameObject);
            SoundManager.instance.PlaySound(collect);
        }
    }
}
