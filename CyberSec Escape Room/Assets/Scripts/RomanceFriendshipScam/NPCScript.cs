using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : ChallengeObject
{
    public DialogueManager dialogueManager;

    protected override void Start()
    {
        base.Start();
        dialogueManager = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<DialogueManager>();
    }

    protected override void Update()
    {

        base.Update();

    }

    public override void EndOfDialogue()
    {

        List<KeyValuePair<string, object>> variableStates = dialogueManager.GetVariableStates();

        foreach (var variableState in variableStates)
        {
            if (variableState.Key == "pass")
            {
                bool trustValue = (bool) variableState.Value;
                if (trustValue)
                {
                    Debug.Log("Player correctly identifies scam");
                    CompleteChallenge();
                }
                else
                {
                    Debug.Log("Player incorrectly identfies scam");
                    logic.decrementLife();
                }
                break;
            }
        }

    }

}
