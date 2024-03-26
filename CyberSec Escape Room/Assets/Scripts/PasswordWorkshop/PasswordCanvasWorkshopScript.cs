using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Linq;
using Zxcvbn;

public class PasswordCanvasWorkshopScript : CanvasScript
{
    [Header("UI Elements")]
    public TMP_InputField inputField;
    public TextMeshProUGUI ruleSetText;
    public GameObject introductionObject;
    public GameObject gamePlayObject;
    public GameObject statsObject;
    public GameObject finalObject;
    public TextMeshProUGUI instructions;
    public TextMeshProUGUI statsSideText;
    public TextMeshProUGUI statsFinalText;

    [TextArea(3, 10)]
    public string instructionsText;

    private int currentStage = -1; // Keeps track of the current stage
    private List<Rule> allRules = new List<Rule>(); // List of all available rules
    private List<Rule> rules = new List<Rule>(); // List of rules for the current stage

    private Rule lengthRule = new Rule("Length: Must have at least 8 characters");
    private Rule uppercaseRule = new Rule("Uppercase: Must contain at least one uppercase letter");
    private Rule lowercaseRule = new Rule("Lowercase: Must contain at least one lowercase letter");
    private Rule numberRule = new Rule("Number: Must contain at least one number");
    private Rule specialCharacterRule = new Rule("Special Character: Must contain at least one special character");
    private Rule commonPasswordRule = new Rule("Common Password: Must not be commonly used or easily guessable");
    private Rule scoreRule = new Rule("Password Score: Password score must be of complexity level 4");

    private string userInput;

    protected override void Start()
    {
        base.Start();

        allRules.Add(commonPasswordRule);
        allRules.Add(lengthRule);
        allRules.Add(uppercaseRule);
        allRules.Add(lowercaseRule);
        allRules.Add(numberRule);
        allRules.Add(specialCharacterRule);
        allRules.Add(scoreRule);
       
        ShowInstructions();
    }    
    public override void closeCanvas()
    {
        base.closeCanvas();
        trigger.TriggerDialogue(false, gameObject);
    }

    private void ShowInstructions()
    {
        introductionObject.SetActive(true);
        gamePlayObject.SetActive(false);
        statsObject.SetActive(false);
        finalObject.SetActive(false);
        instructions.text = instructionsText;
    }

    public void StartGame()
    {
        introductionObject.SetActive(false);
        gamePlayObject.SetActive(true);
        statsObject.SetActive(true);
        DisplayRuleSet();
    }

    private void Completed()
    {
        DisplayPasswordStats(statsFinalText);

        statsFinalText.text = "Your final password inputted was: " + userInput + "\n \n" + statsFinalText.text;

        gamePlayObject.SetActive(false);
        statsObject.SetActive(false);
        finalObject.SetActive(true);
    }

    public void FinishPress()
    {
        challengeObject.CompleteChallenge();
        closeCanvas();
    }

    // Method to display the rule set
    private void DisplayRuleSet()
    {
        if (rules.Count == 0)
        {
            ruleSetText.text = "No password requirements necessary";
        }
        else
        {
            ruleSetText.text = "Rules:\n";
            foreach (Rule rule in rules)
            {
                ruleSetText.text += "- " + rule.description + "\n";
            }
        }
    }

    public void ValidateUserInput()
    {
        userInput = inputField.text;
        Debug.Log("User Input: " + userInput);

        DisplayPasswordStats(statsSideText);

        if (rules.Count == 0)
        {
            Debug.Log("No password requirements necessary. Proceeding to the next stage...");
            ProceedToNextStage();
            return;
        }

        foreach (Rule rule in rules)
        {
            if (!CheckRuleCompliance(rule, userInput))
            {
                Debug.Log("Password does not comply with the rule: " + rule.description);
                return;
            }
        }

        Debug.Log("Password complies with all rules. Proceeding to the next stage...");
        ProceedToNextStage();
    }

    private void DisplayPasswordStats(TextMeshProUGUI stats)
    {

        var result = Zxcvbn.Core.EvaluatePassword(userInput);

        string statsText = $"Password Stats:\n" +
                           $"- Crack Time: {result.CrackTimeDisplay.OfflineFastHashing1e10PerSecond}\n" +
                           $"- Complexity Score: {result.Score}\n";

        stats.text = statsText;
    }

    private void ProceedToNextStage()
    {
        currentStage++;

        if (currentStage < allRules.Count)
        {
            rules.Add(allRules[currentStage]);

            DisplayRuleSet();
        }
        else
        {
            Debug.Log("All stages completed.");
            Completed();
        }
    }

    public bool CheckRuleCompliance(Rule rule, string password)
    {
        switch (rule.description.Split(":")[0].Trim())
        {
            case "Length":
                return password.Length >= 8;

            case "Uppercase":
                return password.Any(char.IsUpper);

            case "Lowercase":
                return password.Any(char.IsLower);

            case "Number":
                return password.Any(char.IsDigit);

            case "Special Character":
                return password.Any(c => !char.IsLetterOrDigit(c));

            case "Common Password":
                return !IsCommonPassword(password);

            case "Password Score":
                return IsComplexEnough(password, 4);

            default:
                return true;
        }
    }

    private bool IsCommonPassword(string password)
    {
        var feedback = Zxcvbn.Core.EvaluatePassword(password).Feedback.Warning;
        return feedback != "";
    }

    private bool IsComplexEnough(string password, int count)
    {
        var feedback = Zxcvbn.Core.EvaluatePassword(password).Score;
        return feedback == count;
    }

}
