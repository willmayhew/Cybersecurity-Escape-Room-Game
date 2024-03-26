using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicTests
{
    private LogicManager logicManager;

    [SetUp]
    public void Setup()
    {
        logicManager = new GameObject().AddComponent<LogicManager>();
        logicManager.deathObject = new GameObject();
        logicManager.redOverlay = new GameObject();
    }

    [Test]
    public void DecrementLife_PlayerNotImmune_LifeDecremented()
    {
        int initialLives = 0;
        logicManager.ToggleImmunity(false);

        logicManager.decrementLife();
        
        Assert.AreEqual(initialLives - 1, logicManager.GetLives());

    }

    [Test]
    public void DecrementLife_PlayerImmune_LifeNotDecremented()
    {
        int initialLives = 0;
        logicManager.ToggleImmunity(true);

        logicManager.decrementLife();

        Assert.AreEqual(initialLives, logicManager.GetLives());
    }

}
