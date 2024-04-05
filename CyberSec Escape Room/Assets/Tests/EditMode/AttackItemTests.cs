using NUnit.Framework;
using UnityEngine;

public class AttackItemTests
{

    private AttackitemScript attackItem;
    private MockLogicManager mockLogicManager;
    private MockBoss mockBoss;

    [SetUp]
    public void Setup()
    {
        attackItem = new GameObject().AddComponent<AttackitemScript>();

        GameObject logicManagerObject = new GameObject();
        mockLogicManager = logicManagerObject.AddComponent<MockLogicManager>();

        GameObject bossObject = new GameObject();
        mockBoss = bossObject.AddComponent<MockBoss>();

        attackItem.logic = mockLogicManager;
        attackItem.boss = mockBoss;
    }

    [Test]
    public void BossDamage()
    {
        attackItem.SetCanvasObject(new GameObject());

        try
        {
            attackItem.ChallengeCompleted(true);
        }
        catch
        {
            // Ignore
        }


        Assert.IsTrue(mockBoss.DamageTaken);

    }

    [Test]
    public void LifeDecremented()
    {
        attackItem.SetCanvasObject(new GameObject());

        try
        {
            attackItem.ChallengeCompleted(false);
        }
        catch
        {
            // Ignore
        }
        

        Assert.IsTrue(mockLogicManager.LifeDecremented);
    }

    private class MockLogicManager : LogicManager
    {
        public bool LifeDecremented { get; private set; }

        public override void decrementLife()
        {
            LifeDecremented = true;
        }
    }

    private class MockBoss : Boss
    {
        public bool DamageTaken { get; private set; }
        public override bool TakeDamage()
        {
            DamageTaken = true;
            return false;
        }

    }
}
