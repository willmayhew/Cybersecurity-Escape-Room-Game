using Cinemachine;
using System.Collections;
using UnityEngine;

public class IntroductionScript : InteractableObject
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        //Nothing
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(trigger);
            trigger.TriggerDialogue(false, gameObject);
        }
    }

}
