using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Policy
{
    public string[] policy;
    public int badPolicy;

    public Policy(string[] policy, int badPolicy)
    {
        this.policy = policy;
        this.badPolicy = badPolicy;
    }
}
