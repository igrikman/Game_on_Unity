using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sc_Charecter : MonoBehaviour
{
    [SerializeField] private float speed = 3.0F;
    [SerializeField] private float HP = 10;
    [SerializeField] private float jumpForce = 10.0F;
    private bool isGround = false;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private Rigidbody2D rigibody;
    private Animator animator;
    private SpriteRenderer sprite;

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
        if (isGround) State = CharState.Idle;

        if (Input.GetButton("Horizontal")) Run();
        if (isGround && Input.GetButtonDown("Jump")) Jump();
    }
    private void Run()
    {
        Vector3 derection = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + derection, speed * Time.deltaTime);

        sprite.flipX = derection.x < 0.0F;

        if (isGround) State = CharState.Walk;
    }

    private void Jump()
    {
        rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = CharState.Jump;
    }
    private void CheckGround()
    {
        Collider2D[] colider = Physics2D.OverlapCircleAll(transform.position, 1F);
        isGround = colider.Length > 1;
    }
}
public enum CharState
{
    Idle,
    Walk,
    Jump
}