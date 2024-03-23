using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Linq;
using Zxcvbn;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

public class PasswordCanvasIdentifierScript : CanvasScript
{
    [Header("UI Elements")]
    public GameObject introductionObject;
    public GameObject gamePlayObject;
    public TextMeshProUGUI instructions;
    public Button password1Button;
    public Button password2Button;
    public Button password3Button;

    private TextMeshProUGUI password1;
    private TextMeshProUGUI password2;
    private TextMeshProUGUI password3;

    [TextArea(3, 10)]
    public string instructionsText;

    private int currentStage = 0;
    public PasswordStage[] stagePasswords;
    private int selectedButton = -1;

    //Password (Strength: 1)
    //Passw0rd1 (Strength: 1)
    //password1 (Strength: 1)
    //SecurePass! (Strength: 2)
    //Compl3x (Strength: 2)
    //h@ckable!2 (Strength: 2)
    //Tr1ckyP@ss!? (Strength: 0)
    //123Security! (Strength: 0)
    //abcdefg789! (Strength: 0)
    //C0mpl3xPa$$ (Strength: 2)
    //P@ssw0rd!23 (Strength: 2)
    //2ecUr3P@ss (Strength: 2)
    //TrUstN0_0n3 (Strength: 1)
    //$3cUr!tY_P@55WD (Strength: 1)
    //P@$$w0rD_Cr4cK3r (Strength: 1)

    protected override void Start()
    {
        base.Start();
        ShowInstructions();

        password1 = password1Button.GetComponentInChildren<TextMeshProUGUI>();
        password2 = password2Button.GetComponentInChildren<TextMeshProUGUI>();
        password3 = password3Button.GetComponentInChildren<TextMeshProUGUI>();

        password1Button.onClick.AddListener(() => selectedButton = 0);
        password2Button.onClick.AddListener(() => selectedButton = 1);
        password3Button.onClick.AddListener(() => selectedButton = 2);

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
        instructions.text = instructionsText;
    }

    public void StartGame()
    {
        introductionObject.SetActive(false);
        gamePlayObject.SetActive(true);
        DisplayPasswords();
    }

    private void Completed()
    {
        gamePlayObject.SetActive(false);
    }

    public void FinishPress()
    {
        challengeObject.CompleteChallenge();
        closeCanvas();
    }

    private void DisplayPasswords()
    {
        if(stagePasswords.Count() == 0)
        {
            Debug.LogError("Passwords have not been set");
            return;
        }

        password1.text = stagePasswords[currentStage].passwords[0];
        password2.text = stagePasswords[currentStage].passwords[1];
        password3.text = stagePasswords[currentStage].passwords[2];
    }

    public void CheckAnswer()
    {
        if (stagePasswords[currentStage].strongest == selectedButton)
        {
            NextStage();
        } 
        else if(selectedButton != -1)
        {
            logic.decrementLife();
        }

        selectedButton = -1;

    }

    private void NextStage()
    {

        currentStage++;

        if(currentStage >= stagePasswords.Count())
        {
            FinishPress();
            return;

        }

        DisplayPasswords();
    }

}
