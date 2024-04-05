using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RomFriScamTests
{
    private NPCScript npcScript;
    private MockDialogueManager mockDialogueManager;
    private MockLogicManager mockLogicManager;

    [SetUp]
    public void Setup()
    {
        npcScript = new GameObject().AddComponent<NPCScript>();

        GameObject dialogueManagerObject = new GameObject();
        mockDialogueManager = dialogueManagerObject.AddComponent<MockDialogueManager>();

        GameObject logicManagerObject = new GameObject();
        mockLogicManager = logicManagerObject.AddComponent<MockLogicManager>();

        npcScript.dialogueManager = mockDialogueManager;
        npcScript.logic = mockLogicManager;
    }

    [Test]
    public void Correct()
    {
        // Arrange
        mockDialogueManager.SetVariableState("pass", true);

        // Act
        npcScript.EndOfDialogue();

        // Assert
        Assert.IsTrue(npcScript.challengeComplete);
    }

    [Test]
    public void Incorrect_LifeDecremented()
    {
        // Arrange
        mockDialogueManager.SetVariableState("pass", false);

        // Act
        npcScript.EndOfDialogue();

        // Assert
        Assert.IsTrue(mockLogicManager.LifeDecremented);
    }

    // Mock DialogueManager class to simulate behavior
    private class MockDialogueManager : DialogueManager
    {
        private Dictionary<string, object> variableStates = new Dictionary<string, object>();

        public void SetVariableState(string key, object value)
        {
            variableStates[key] = value;
        }

        public override List<KeyValuePair<string, object>> GetVariableStates()
        {
            List<KeyValuePair<string, object>> states = new List<KeyValuePair<string, object>>();
            foreach (var pair in variableStates)
            {
                states.Add(new KeyValuePair<string, object>(pair.Key, pair.Value));
            }
            return states;
        }
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
