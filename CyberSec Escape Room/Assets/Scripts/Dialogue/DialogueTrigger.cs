using UnityEngine;
using Ink.Runtime; // Make sure to include the Ink.Runtime namespace

public class DialogueTrigger : MonoBehaviour
{
    public TextAsset preInkJSON;
    public TextAsset postInkJSON;

    private Story currentStory;

    public void TriggerDialogue(bool challengeCompleted, GameObject currentObject)
    {
        TextAsset inkJSON = challengeCompleted ? postInkJSON : preInkJSON;

        currentStory = new Story(inkJSON.text);

        FindObjectOfType<DialogueManager>().StartDialogue(currentStory, currentObject);
    }

    public void TriggerDialogue(bool challengeCompleted, GameObject currentObject, TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);

        FindObjectOfType<DialogueManager>().StartDialogue(currentStory, currentObject);
    }

}
