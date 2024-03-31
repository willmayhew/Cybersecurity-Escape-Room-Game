using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicTests
{
    private LogicManager logicManager;
    int initialLives = 5;

    [SetUp]
    public void Setup()
    {
        logicManager = new GameObject().AddComponent<LogicManager>();
        logicManager.deathObject = new GameObject();
        logicManager.redOverlay = new GameObject();

        logicManager.lives = initialLives;

    }

    [Test]
    public void DecrementLife_PlayerNotImmune_LifeDecremented()
    {
        logicManager.ToggleImmunity(false);

        try
        {
            logicManager.decrementLife();
        }
        catch
        {
            // ignore
        }
        
        
        Assert.AreEqual(initialLives - 1, logicManager.GetLives());

    }

    [Test]
    public void DecrementLife_PlayerImmune_LifeNotDecremented()
    {
        logicManager.ToggleImmunity(true);

        logicManager.decrementLife();

        Assert.AreEqual(initialLives, logicManager.GetLives());
    }

    [Test]
    public void IncrementLife_NotMaxLives()
    {
        try
        {
            logicManager.AddLife();
        } catch { }

        Assert.AreEqual(initialLives + 1, logicManager.GetLives());

    }

    [Test]
    public void IncrementLife_MaxLivesReached()
    {
        logicManager.lives = 10;

        try
        {
            logicManager.AddLife();
        }
        catch { }

        Assert.AreEqual(10, logicManager.GetLives());

    }

}
