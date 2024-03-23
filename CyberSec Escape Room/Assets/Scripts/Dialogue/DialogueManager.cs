using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SearchService;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;
    public float wordSpeed;

    public PlayerMovement playerMovement;

    private GameObject currentObject;

    private Story currentStory;
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    void Start()
    {
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void StartDialogue(Story story, GameObject currentObject)
    {

        playerMovement.ToggleMovement(false);
        this.currentObject = currentObject;
        dialoguePanel.SetActive(true);

        currentStory = story;

        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {

        if (currentStory.canContinue)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentStory.Continue()));
            DisplayChoices();
            HandleTags(currentStory.currentTags);
        } else
        {
            EndDialogue();
        }
       
    }

    private void HandleTags(List<string> currentTags)
    {

        if (currentTags.Count == 0)
        {
            nameText.text = "...";
        }
        else
        {
            foreach (string tag in currentTags)
            {
                string splitTag = tag.Split(':')[1].Trim();
                nameText.text = splitTag;
            }
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        playerMovement.ToggleMovement(true);
        dialogueText.text = "";
        Debug.Log("End of conversation");

        InteractableObject interactableObject = currentObject.GetComponent<InteractableObject>();
        if (interactableObject != null && !interactableObject.GetChallengeCompletionStatus())
        {
            interactableObject.EndOfDialogue();
        }

    }

    private void DisplayChoices()
    {

        if(currentStory.currentChoices.Count == 0)
        {
            continueButton.gameObject.SetActive(true);
        } else {
            continueButton.gameObject.SetActive(false);
        }

        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("Too many choices for the UI");
        }

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());

    }
    
    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        DisplayNextSentence();
    }

    public List<KeyValuePair<string, object>> GetVariableStates()
    {
        List<KeyValuePair<string, object>> variableStates = new List<KeyValuePair<string, object>>();

        foreach (var variableState in currentStory.variablesState)
        {
            object value = currentStory.variablesState[variableState];
            variableStates.Add(new KeyValuePair<string, object>(variableState, value));
        }

        Debug.Log("Variable states obtained");

        return variableStates;
    }

}
