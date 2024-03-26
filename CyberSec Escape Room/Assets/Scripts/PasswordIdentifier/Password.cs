using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PasswordStage
{
    public int strongest;
    public string[] passwords;

    public PasswordStage(string[] passwords, int strongest)
    {
        this.passwords = passwords;
        this.strongest = strongest;
    }

}
