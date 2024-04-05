using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolicyTest
{
    private SecurityPolicyScript securityPolicyScript;

    [SetUp]
    public void SetUp()
    {
        securityPolicyScript = new GameObject().AddComponent<SecurityPolicyScript>();

        securityPolicyScript.policies = new Policy[]
        {
            new Policy(new string[] {"Policy1a", "Policy1b", "Policy1c"}, 0),
            new Policy(new string[] {"Policy2a", "Policy2b", "Policy2c"}, 1),
            new Policy(new string[] {"Policy3a", "Policy3b", "Policy3c"}, 2)
        };

    }

    [Test]
    public void CorrectAnswer()
    {
        securityPolicyScript.selectedButton = 0; // Select the correct answer

        try
        {
            securityPolicyScript.CheckAnswer();
        }
        catch
        {
            // Ignore
        }
        
        // Assert
        Assert.AreEqual(1, securityPolicyScript.currentIndex);
    }

    [Test]
    public void IncorrectAnswer()
    {
        securityPolicyScript.selectedButton = 1; // Select the incorrect answer

        try
        {
            securityPolicyScript.CheckAnswer();
        }
        catch
        {
            // Ignore
        }

        // Assert
        Assert.AreEqual(0, securityPolicyScript.currentIndex);
    }

}
