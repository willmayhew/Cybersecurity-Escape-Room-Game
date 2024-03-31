using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PhishingChallengeTests
{

    private PhishingCanvasScript phishingCanvasScript;

    [SetUp]
    public void Setup()
    {
        phishingCanvasScript = new GameObject().AddComponent<PhishingCanvasScript>();
        phishingCanvasScript.letters = new Letter[]
        {
            new Letter("Legitimate letter", true),
            new Letter("Illegitimate letter", false),
            new Letter("Illegitimate letter", false)
        };

        phishingCanvasScript.letterTextUI = new GameObject().AddComponent<TMPro.TextMeshProUGUI>(); // Initialize letterTextUI
        phishingCanvasScript.acceptButton = new GameObject();
        phishingCanvasScript.rejectButton = new GameObject();
    }

    [Test]
    public void AcceptButton_Clicked_WhenLegitimateLetter()
    {
        // Simulate clicking the accept button
        phishingCanvasScript.LegitimateButton();

        // Ensure that the NextLetter method is called
        Assert.AreEqual(1, phishingCanvasScript.currentLetterIndex);
        Assert.IsFalse(phishingCanvasScript.incorrect);
    }

    [Test]
    public void AcceptButton_Clicked_WhenIllegitimateLetter()
    {
        // Set the current letter index to 1 (illegitimate letter)
        phishingCanvasScript.currentLetterIndex = 1;

        // Simulate clicking the accept button
        phishingCanvasScript.LegitimateButton();

        // Ensure that the incorrect flag is set
        Assert.IsTrue(phishingCanvasScript.incorrect);
        // Ensure that the NextLetter method is called
        Assert.AreEqual(2, phishingCanvasScript.currentLetterIndex);
    }

    [Test]
    public void RejectButton_Clicked_WhenLegitimateLetter()
    {
        // Simulate clicking the reject button
        phishingCanvasScript.IllegitimateButton();

        // Ensure that the incorrect flag is set
        Assert.IsTrue(phishingCanvasScript.incorrect);
        // Ensure that the NextLetter method is called
        Assert.AreEqual(1, phishingCanvasScript.currentLetterIndex);
    }

    [Test]
    public void RejectButton_Clicked_WhenIllegitimateLetter()
    {
        // Set the current letter index to 1 (illegitimate letter)
        phishingCanvasScript.currentLetterIndex = 1;

        // Simulate clicking the reject button
        phishingCanvasScript.IllegitimateButton();

        // Ensure that the NextLetter method is called
        Assert.AreEqual(2, phishingCanvasScript.currentLetterIndex);
        Assert.IsFalse(phishingCanvasScript.incorrect);
    }

}
