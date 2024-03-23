using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{

    public int doorIndex = 0;

    public string sceneToLoad;
    private SpriteRenderer doorOverlay;
    public float fadeDuration = 1.0f; // Adjust the duration of the fade

    public bool isDoorOpen = false;
    public GameObject unlockItem;
    public string keyTag;

    private InventoryManager inventory;
    private DialogueTrigger trigger;
    private LogicManager logic;

    void Start(){

        inventory = InventoryManager.Instance;
        trigger = GetComponent<DialogueTrigger>();
        logic = LogicManager.Instance;

        unlockItem.tag = keyTag;

        doorOverlay = GetComponent<SpriteRenderer>();

        if (LogicManager.Instance.isDoorOpen(gameObject.name))
        {
            doorOverlay.enabled = false;
            isDoorOpen = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player")) {

            Debug.Log("Player Door collision");

            if (isDoorOpen)
            {
                StartNextLevel();
            } 
            else if(inventory.UseItem(unlockItem))
            {
                Debug.Log("Player has the key. Door is removed!");
                StartCoroutine(FadeOutDoor(collision.gameObject.transform));
                LogicManager.Instance.AddOpenDoor(gameObject.name);
            }
            else
            {
                Debug.Log("Player does not have the key. Door remains closed.");
                trigger.TriggerDialogue(false, gameObject);
            }

        }
    }

    IEnumerator FadeOutDoor(Transform playerTransform)
    {
        Color startColor = doorOverlay.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;

        Vector3 originalPosition = playerTransform.position;
        Vector3 targetPosition = originalPosition - new Vector3(0f, 0.5f, 0f);

        while (elapsedTime < fadeDuration)
        {
            doorOverlay.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            playerTransform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        doorOverlay.color = targetColor;
        playerTransform.position = targetPosition;

        isDoorOpen = true;
    }

    void StartNextLevel()
    {

        //Debug.LogError(logic);
        //Debug.LogError(inventory);
        //Debug.LogError(ui);

        //DontDestroyOnLoad(logic.gameObject);
        //DontDestroyOnLoad(inventory.gameObject);
        //DontDestroyOnLoad(ui.gameObject);

        logic.doorEntered(doorIndex);
        SceneManager.LoadScene(sceneToLoad);
    }

}
