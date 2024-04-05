using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordIdentifierTests
{
    private PasswordCanvasIdentifierScript passwordIdentifier;
    private PasswordStage[] stages;

    private MockLogicManager mockLogicManager;

    [SetUp]
    public void SetUp()
    {
        GameObject gameObject = new GameObject();
        passwordIdentifier = gameObject.AddComponent<PasswordCanvasIdentifierScript>();

        stages = new PasswordStage[2];
        stages[0] = new PasswordStage(new string[] {"password1", "password2", "password3"}, 0);
        stages[1] = new PasswordStage(new string[] { "password1", "password2", "password3" }, 1);
        passwordIdentifier.stagePasswords = stages;


        GameObject logicManagerObject = new GameObject();
        mockLogicManager = logicManagerObject.AddComponent<MockLogicManager>();
        passwordIdentifier.logic = mockLogicManager;

    }

    [Test]
    public void Correct()
    {
        passwordIdentifier.selectedButton = 0;

        try
        {
            passwordIdentifier.CheckAnswer();
        }
        catch
        {
            // Ignore
        }
        

        Assert.AreEqual(1, passwordIdentifier.currentStage);
    }

    [Test]
    public void Incorrect_LifeDecremented()
    {
        passwordIdentifier.stagePasswords = stages;
        passwordIdentifier.selectedButton = 1;

        try
        {
            passwordIdentifier.CheckAnswer();
        }
        catch
        {
            // Ignore
        }

        Assert.IsTrue(mockLogicManager.LifeDecremented);
    }

    [Test]
    public void NoButtonSelected()
    {
        passwordIdentifier.stagePasswords = stages;
        passwordIdentifier.selectedButton = -1;

        try
        {
            passwordIdentifier.CheckAnswer();
        }
        catch
        {
            // Ignore
        }

        Assert.AreEqual(0, passwordIdentifier.currentStage);
        Assert.IsFalse(mockLogicManager.LifeDecremented);
    }

    private class MockLogicManager : LogicManager
    {
        public bool LifeDecremented { get; private set; }

        public override void decrementLife()
        {
            LifeDecremented = true;
        }
    }

}
