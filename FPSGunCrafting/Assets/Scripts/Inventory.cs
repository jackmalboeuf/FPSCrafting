using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<string> inventoryItems = new List<string>();

    public void AddItem(PickupObject item)
    {
        inventoryItems.Add(item.inventoryID);
    }
}
