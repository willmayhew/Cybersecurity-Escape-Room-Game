using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManagerDestroyer : MonoBehaviour
{
    private void Start()
    {
        // Find all objects with the tag "Logic" and destroy them
        GameObject[] logicManagers = GameObject.FindGameObjectsWithTag("Logic");
        foreach (GameObject manager in logicManagers)
        {
            Destroy(manager);
        }

        // Find all objects with the tag "Manager" and destroy them
        GameObject[] managers = GameObject.FindGameObjectsWithTag("Inventory");
        foreach (GameObject manager in managers)
        {
            Destroy(manager);
        }

        // Find all objects with the tag "Dialogue" and destroy them
        GameObject[] dialogueManagers = GameObject.FindGameObjectsWithTag("Dialogue");
        foreach (GameObject manager in dialogueManagers)
        {
            Destroy(manager);
        }

        GameObject[] playerUI = GameObject.FindGameObjectsWithTag("UI");
        foreach (GameObject manager in playerUI)
        {
            Destroy(manager);
        }

    }
}