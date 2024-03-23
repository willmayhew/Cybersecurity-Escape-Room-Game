using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : ChallengeObject
{
    private Animator animator;
    
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        if (canOpenChest())
        {
            animator.Play("Open");
        }

    }

    protected override void Update()
    {

        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (canOpenChest())
            {
                animator.Play("Open");
                inventory.AddToInventory(rewardItem);
                trigger.TriggerDialogue(true, gameObject);
            }
            else
            {
                trigger.TriggerDialogue(false, gameObject);
            }
       
        }

    }

    private bool canOpenChest()
    {
        return logic.isChallengeComplete("Altar1") && logic.isChallengeComplete("Altar2");
    }

}
