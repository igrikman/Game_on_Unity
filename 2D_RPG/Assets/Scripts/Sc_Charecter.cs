using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sc_Charecter : Sc_Unit
{
    [SerializeField] private float speed = 3.0F;
    [SerializeField] private float HP = 10;
    [SerializeField] private float jumpForce = 10.0F;
    private bool isGround = false;
    [SerializeField] private Sc_Bullet bullet;
    
    private Rigidbody2D rigibody;
    private Animator animator;
    private SpriteRenderer sprite;



    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

       // bullet = Re;
    }
    private void FixedUpdate()
    {
        CheckGround();
    }
    private void Update()
    {
        if (isGround) State = CharState.Idle;
        if (Input.GetButtonDown("Fire1")) Shoot();
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
        if (!isGround) State = CharState.Jump;
    }

    private void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.0F; position.x += 0.0F; 
        Sc_Bullet newBullet = Instantiate(bullet , position, bullet.transform.rotation);
        newBullet.Derection = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        State = CharState.Fire;
    }
}
public enum CharState
{
    Idle,
    Walk,
    Jump,
    Fire
}