using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;
    private Transform inventoryUI;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        inventoryUI = transform.Find("Inventory");

    }

    public Transform GetInventoryUI()
    {
        return inventoryUI;
    }

}
