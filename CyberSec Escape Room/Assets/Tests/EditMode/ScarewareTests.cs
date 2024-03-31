using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarewareTests
{

    private ScarewareScript scarewareScript;
    private ScarewareManager scarewareManager;
    private PlayerMovement playerMovement;
    private MockLogicManager mockLogicManager;

    [SetUp]
    public void SetUp()
    {
        scarewareScript = new GameObject().AddComponent<ScarewareScript>();

        GameObject scarewareManagerObject = new GameObject();
        scarewareManager = scarewareManagerObject.AddComponent<ScarewareManager>();

        GameObject playerObject = new GameObject();
        playerMovement = playerObject.AddComponent<PlayerMovement>();

        GameObject logicManagerObject = new GameObject();
        mockLogicManager = logicManagerObject.AddComponent<MockLogicManager>();

        scarewareScript.scarewareManager = scarewareManager;
        scarewareScript.player = playerMovement;
        scarewareManager.logic = mockLogicManager;
    }

    [Test]
    public void CheckAnswer_CorrectAnswer_LifeNotDecremented()
    {
        try
        {
            scarewareScript.ChoiceMade(true);
        }
        catch { }

        Assert.IsFalse(mockLogicManager.LifeDecremented);

    }

    [Test]
    public void CheckAnswer_IncorrectAnswer()
    {
        try
        {
            scarewareScript.ChoiceMade(false);
        }
        catch { }

        Assert.IsTrue(mockLogicManager.LifeDecremented);
    }

    [Test]
    public void CheckAnswer_CorrectAnswer_LifeIncremented()
    {
        try
        {
            scarewareScript.ChoiceMade(true);
        }
        catch { }

        Assert.IsTrue(mockLogicManager.LifeIncremented);
    }

    [Test]
    public void CheckAnswer_IncorrectAnswer_LifeNotIncremented()
    {
        try
        {
            scarewareScript.ChoiceMade(false);
        }
        catch { }

        Assert.IsFalse(mockLogicManager.LifeIncremented);
    }

    private class MockLogicManager : LogicManager
    {
        public bool LifeDecremented { get; private set; }
        public bool LifeIncremented { get; private set; }
        public override void decrementLife()
        {
            LifeDecremented = true;
        }

        public override void AddLife()
        {
            LifeIncremented = true;
        }
    }

}
