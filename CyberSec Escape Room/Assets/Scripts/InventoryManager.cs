using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<GameObject> inventoryObjects = new List<GameObject>();
    private UIScript uiElements;

    private void Awake()
    {

        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad (gameObject);
        
    }

    public void AddToInventory(GameObject item)
    {
        uiElements = UIScript.Instance;
        Transform parent = uiElements.GetInventoryUI();

        Vector3 newPosition;
        GameObject newItem;

        if (inventoryObjects.Count > 0)
        {
            GameObject lastItem = inventoryObjects[inventoryObjects.Count - 1];
            RectTransform lastItemRectTransform = lastItem.GetComponent<RectTransform>();
            RectTransform newItemRectTransform = item.GetComponent<RectTransform>();

            newPosition = lastItemRectTransform.position - new Vector3(2 * lastItemRectTransform.rect.width * 2 + newItemRectTransform.rect.width * 2, 0f, 0f);
            newItem = Instantiate(item, newPosition, Quaternion.identity, parent);
        }
        else{
            newItem = Instantiate(item, parent);
        }

        inventoryObjects.Add(newItem);

        Debug.Log("Key Collected");

    }

    public bool UseItem(GameObject item)
    {
        for (int i = 0; i < inventoryObjects.Count; i++)
        {
            GameObject ownedItem = inventoryObjects[i];
            if (item.tag == ownedItem.tag)
            {

                inventoryObjects.RemoveAt(i);

                RectTransform rtToRemove = ownedItem.GetComponent<RectTransform>();
                Vector2 positionToReplace = rtToRemove.position;

                Destroy(ownedItem);

                for (int j = i; j < inventoryObjects.Count; j++)
                {
                    RectTransform rt = inventoryObjects[j].GetComponent<RectTransform>();
                    Vector2 tempPosition = rt.position;
                    rt.position = positionToReplace;
                    positionToReplace = tempPosition;
                }

                return true;
            }
        }

        return false;
    }

}
