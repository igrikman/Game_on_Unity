using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sc_Charecter : MonoBehaviour
{
    [SerializeField] private float speed = 3.0F;
    [SerializeField] private float HP = 10;
    [SerializeField] private float jumpForce = 10.0F;

    private Rigidbody2D rigibody;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGround = false;
    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (Input.GetButton("Horizontal")) Run();
        if (isGround && Input.GetButtonDown("Jump")) Jump();
    }
    private void Run()
    {
        Vector3 derection = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + derection, speed * Time.deltaTime);

        sprite.flipX = derection.x < 0.0F;
    }

    private void Jump()
    {
        rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    private void CheckGround()
    {
        Collider2D[] colider = Physics2D.OverlapCircleAll(transform.position, 1F);
        isGround = colider.Length > 1;
    }
}
