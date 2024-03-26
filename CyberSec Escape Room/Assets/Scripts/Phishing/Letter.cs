using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Letter
{
    [TextArea(3, 10)]
    public string text;
    public bool isLegitimate;

    public Letter(string text, bool isLegitimate)
    {
        this.text = text;
        this.isLegitimate = isLegitimate;
    }

    public string GetText()
    {
        return text;
    }

    public bool IsLegitimate()
    {
        return isLegitimate;
    }

}
