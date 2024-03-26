using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : InteractableObject
{
    public event Action AttackFinished;

    private Animator animator;
    private Transform enemyGFX;

    public AttackItemSpawner spawner;
    public EnemyAI enemyAI;
    public bool IsAttacking { get; private set; }

    public Transform attackPosition1And2;
    public Transform attackPosition3;
    public GameObject projectile1;
    public GameObject projectile2;
    public GameObject projectile3;

    private float health;
    public float maxHealth = 500;
    public FloatingHealthBar healthBar;

    protected override void Start()
    {
        base.Start();

        health = maxHealth;
        animator = GetComponent<Animator>();
        enemyGFX = GetComponent<Transform>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    protected override void Update()
    {
        //Nothing
    }

    public void SetMoving(bool moving)
    {
        animator.SetBool("Moving", moving);
    }

    public void SetSpriteDirection(Vector2 force)
    {
        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void TriggerAttackAnimation()
    {
        if (!IsAttacking)
        {
            int randomAttack = UnityEngine.Random.Range(1, 4);
            string triggerName = "Attack" + randomAttack;
            animator.SetTrigger(triggerName);
            IsAttacking = true;
            animator.SetBool("IsAttacking", IsAttacking);
        }
    }

    public void Attack(int attack)
    {
        GameObject projectile = null;
        Transform attackPosition;
        Quaternion attackRotation;

        switch (attack)
        {
            case 2:
                projectile = projectile3;
                attackPosition = attackPosition3;
                break;
            case 1:
                projectile = projectile2;
                attackPosition = attackPosition1And2;
                break;
            default:
                projectile = projectile1;
                attackPosition = attackPosition1And2;
                break;
        }

        attackRotation = attackPosition.rotation;
        Instantiate(projectile, attackPosition.position, attackRotation);

    }

    public void FinishAttack()
    {
        IsAttacking = false;
        animator.SetBool("IsAttacking", IsAttacking);
        AttackFinished?.Invoke();
    }

    public virtual bool TakeDamage()
    {
        StartCoroutine(StartAnimationWithDelay("Hurt"));

        health -= 100;
        healthBar.UpdateHealthBar(health, maxHealth);
        Debug.Log("Damage Taken, Current Health: " + health);

        if(health <= 0)
        {
            Death();
            return true;
        }

        return false;

    }

    private void Death()
    {
        spawner.ToggleSpawning(false);
        enemyAI.ToggleMovement(false);
        trigger.TriggerDialogue(false, gameObject);
    }

    IEnumerator StartAnimationWithDelay(string animationTrigger)
    {
        yield return new WaitForSeconds(0.25f);
        animator.SetTrigger(animationTrigger);
    }

    private void HideEnemyObject()
    {
        enemyAI.HideObject();
    }

    public override void EndOfDialogue()
    {
        StartCoroutine(StartAnimationWithDelay("Death"));
    }

}
