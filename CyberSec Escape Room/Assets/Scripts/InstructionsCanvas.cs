using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsCanvas : CanvasScript
{
    public override void closeCanvas()
    {
        playerMovement.ToggleMovement(true);

    }

}
