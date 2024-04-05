using NUnit.Framework;
using UnityEngine;

public class InventoryTests
{
    private InventoryManager inventoryManager;
    private GameObject itemPrefab;

    [SetUp]
    public void SetUp()
    {
        inventoryManager = new GameObject().AddComponent<InventoryManager>();
        UIScript.Instance = new GameObject().AddComponent<UIScript>();

        itemPrefab = new GameObject();
        itemPrefab.tag = "DoorKey1";
        itemPrefab.AddComponent<RectTransform>();

    }

    [Test]
    public void AddToInventory()
    {
        inventoryManager.AddToInventory(itemPrefab);

        Assert.IsTrue(inventoryManager.inventoryObjects.Find(x => x.CompareTag("DoorKey1")));
    }

    //[Test]
    //public void UseItem_ItemExists()
    //{
    //    inventoryManager.AddToInventory(itemPrefab);

    //    bool result = inventoryManager.UseItem(itemPrefab);

    //    Assert.IsTrue(result);
    //    //Assert.IsFalse(inventoryManager.inventoryObjects.Contains(itemPrefab));
    //}

    [Test]
    public void UseItem_DoesNotExist()
    {
        GameObject item2 = new GameObject();
        item2.AddComponent<RectTransform>();

        inventoryManager.AddToInventory(itemPrefab);

        bool result = inventoryManager.UseItem(item2);

        Assert.IsFalse(result);
        Assert.IsTrue(inventoryManager.inventoryObjects.Find(x => x.CompareTag("DoorKey1")));
    }
}
