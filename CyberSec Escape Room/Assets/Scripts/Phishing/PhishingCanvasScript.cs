using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhishingCanvasScript : CanvasScript
{
    public TextMeshProUGUI letterTextUI;
    [TextArea(3, 10)]
    public string instructionsText;

    public GameObject continueButton;
    public GameObject acceptButton;
    public GameObject rejectButton;

    public Letter[] letters;
    public int currentLetterIndex = 0;
    public bool incorrect = false;

    protected override void Start()
    {
        base.Start();
        ShowInstructions();
    }

    public override void closeCanvas()
    {
        base.closeCanvas();

        if (challengeObject.GetChallengeCompletionStatus())
        {
            trigger.TriggerDialogue(true, gameObject);
        }

    }

    private void ShowInstructions()
    {
        letterTextUI.text = instructionsText;
    }

    protected virtual void StartGame()
    {
        DisplayLetter(0);

        continueButton.SetActive(false);
        acceptButton.SetActive(true);
        rejectButton.SetActive(true);
    }

    public void ResetChallenge()
    {
        currentLetterIndex = 0;
        incorrect = false;
        DisplayLetter(0);
    }

    protected void DisplayLetter(int index)
    {
        if(index < letters.Length)
        {
            letterTextUI.text = letters[index].text;
        }
        else
        {
            EndOfLetters();
        }

    }

    protected virtual void EndOfLetters()
    {
        if (incorrect)
        {
            Debug.Log("Incorrect selections");

            logic.decrementLife();

        }
        else
        {
            Debug.Log("Correct selections");
            challengeObject.CompleteChallenge();
        }

        ResetChallenge();
        closeCanvas();

    }

    protected virtual void NextLetter()
    {
        currentLetterIndex++;
        DisplayLetter(currentLetterIndex);
    }

    protected bool isLetterLegitimate()
    {
        return letters[currentLetterIndex].isLegitimate;
    }

    public void LegitimateButton()
    {
        if (!isLetterLegitimate())
        {
            incorrect = true;
        }
        NextLetter();
    }

    public void IllegitimateButton()
    {
        if (isLetterLegitimate())
        {
            incorrect = true;
        }
        NextLetter();
    }

}
