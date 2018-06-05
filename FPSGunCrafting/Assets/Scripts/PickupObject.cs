using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour
{
    public string inventoryID;
    public Inventory inv;

    public void PickedUp()
    {
        inv.AddItem(gameObject.GetComponent<PickupObject>());

        Destroy(gameObject);
    }
}
