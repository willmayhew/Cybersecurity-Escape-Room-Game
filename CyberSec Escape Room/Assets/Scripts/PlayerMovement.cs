using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zxcvbn.Matcher.Matches;
using static UnityEngine.Rendering.DebugUI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private int speed = 5;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    public Vector2[] doorPositions;

    private bool isMovingToTarget = true;
    private bool canMove = true;
    private Collider2D originalCollider;

    private LogicManager logic;

    private void Awake() {

        logic = LogicManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Vector2 doorPosition = doorPositions[logic.getDoorIndex()];
        rb.position = new Vector2(doorPosition.x, doorPosition.y + 0.7f);

        originalCollider = GetComponent<BoxCollider2D>();
        originalCollider.enabled = false;

    }

    private void OnMovement(InputValue value){

        if (!canMove) return;

        movement = value.Get<Vector2>();
        
        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        } 
        else
        {
            animator.SetBool("IsWalking", false);
        }

    }

    private void FixedUpdate()
    {
        if (isMovingToTarget)
        {
            MoveToPosition(doorPositions[logic.getDoorIndex()]);
        }
        else
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }

    }

    public void MoveToPosition(Vector2 targetPosition)
    {

        Vector2 direction = targetPosition - rb.position;
        direction.Normalize();

        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);

        rb.MovePosition(rb.position + 1 * Time.fixedDeltaTime * direction);
        animator.SetBool("IsWalking", true);

        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            isMovingToTarget = false;
            animator.SetBool("IsWalking", false);

            originalCollider.enabled = true;
            canMove = true;
        }
    }

    public void ToggleMovement(bool toggle)
    {
        canMove = toggle;
        movement.x = 0; 
        movement.y = 0;
        animator.SetBool("IsWalking", false);
    }

    public bool CanMove()
    {
        return canMove;
    }

}