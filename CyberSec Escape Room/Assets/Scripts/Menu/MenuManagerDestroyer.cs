using UnityEngine;

public class MenuManagerDestroyer : MonoBehaviour
{
    private void Start()
    {
        // Destroy all managers
        DestroyManagersWithTag("Logic");
        DestroyManagersWithTag("Inventory");
        DestroyManagersWithTag("Dialogue");
        DestroyManagersWithTag("UI");
    }

    private void DestroyManagersWithTag(string tag)
    {
        Debug.Log(tag);
        GameObject[] managers = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject manager in managers)
        {

            Debug.Log(manager);

            // If the GameObject is inactive, activate it before destroying
            manager.SetActive(true);

            // Now destroy the GameObject
            Destroy(manager);
        }
    }
}
