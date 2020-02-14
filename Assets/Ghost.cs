using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Unit
{

    public enum CharState
    {
        Move,
        Death
    }

    [SerializeField]
    private int lives = 1;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

   

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if (unit && unit is Character)
        {
           
            unit.ReceiveDamage();
           
        }

    }

    public override void ReceiveDamage()
    {
        lives--;
        if (lives == 0) Die();
    }

    // Start is called before the first frame update
    void Start()
    {
        State = CharState.Move;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
