using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public ChallengeObject challengeObject;
    public PlayerMovement playerMovement;
    protected DialogueTrigger trigger;
    public LogicManager logic;

    protected virtual void Start()
    {
        playerMovement.ToggleMovement(false);
        trigger = GetComponent<DialogueTrigger>();
        logic = LogicManager.Instance;
    }

    private void OnEnable()
    {
        playerMovement.ToggleMovement(false);
    }

    public virtual void closeCanvas()
    {
        gameObject.SetActive(false);
        playerMovement.ToggleMovement(true);
    }

}
