using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule
{
    public string description;
    public Rule(string description)
    {
        this.description = description;
    }

}

[System.Serializable]
public class RuleSet
{
    public Rule lengthRule;
    public Rule uppercaseRule;
    public Rule lowercaseRule;
    public Rule numberRule;
    public Rule specialCharacterRule;
    public Rule commonPasswordRule;
}
