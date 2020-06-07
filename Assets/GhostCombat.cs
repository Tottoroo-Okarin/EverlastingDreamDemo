using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCombat : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
            
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerCombat unit = collider.GetComponent<PlayerCombat>();
        if (unit && unit is PlayerCombat)
        {

            unit.TakeDamage(20);

        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
    
}
