using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CharState
{
    Idle,
    Run,
    Jump,
    Hurt,
    Crouch,
    Attack,
    Death
}

public class Character : Unit
{

    [SerializeField]
    private int lives = 3;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float jumpForce = 10;

    private bool isGrounded = false;

    


    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    Bullet bullet;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    // Update is called once per frame
    private void Update()
    {
        if(isGrounded)State = CharState.Idle;
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
    }

    private void Run() 
    {
        if (isGrounded)State = CharState.Run;
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0;

        
    }

    private void Jump() 
    {
        
        State = CharState.Jump;

        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    

    private void Shoot()
    {   
       
        Vector3 position = transform.position;
        
        State = CharState.Attack;
       
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);
        
      
    }

    public override void ReceiveDamage()
    {
        State = CharState.Hurt;
        lives--;
        if (lives == 0)
        {
            State = CharState.Death;
            Die();
        }
        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);
        
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);
        isGrounded = colliders.Length > 1;
        if (!isGrounded) State = CharState.Jump;
    }
}


