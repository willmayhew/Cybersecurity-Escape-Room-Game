using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperIntroScript : CanvasScript
{
    protected override void Start()
    {
        base.Start();
    }

    public override void closeCanvas()
    {
        base.closeCanvas();

        challengeObject.CompleteChallenge();
        trigger.TriggerDialogue(false, gameObject);

    }
}
