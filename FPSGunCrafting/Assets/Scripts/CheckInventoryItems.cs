using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckInventoryItems : MonoBehaviour
{
    public Inventory inv;

    [SerializeField]
    List<string> requiredItems = new List<string>();

    int numOfRequiredItems;
    int numOfCorrectItems;

    void Start()
    {
        numOfRequiredItems = requiredItems.Count;
    }

    public void CheckItems()
    {
        numOfCorrectItems = 0;

        foreach (string item in requiredItems)
        {
            for (int i = 0; i < inv.inventoryItems.Count; i++)
            {
                if (inv.inventoryItems[i] == item)
                    numOfCorrectItems++;
            }
        }

        if (numOfCorrectItems == numOfRequiredItems)
        {
            print("correct items");

            //if gameobject has open door component, open door (code here)

            //if gameobject has any other components that can be triggered with correct items, activate them (code here)
        }
        else
        {
            print("you dont have the required items");
        }
    }
}
