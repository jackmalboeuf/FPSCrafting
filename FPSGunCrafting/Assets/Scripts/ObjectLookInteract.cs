using UnityEngine;
using System.Collections;

public class ObjectLookInteract : MonoBehaviour
{
    [SerializeField]
    float interactRange;

    void Update()
    {
        Vector3 rayStart = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit rayHit;
        Ray lookRay = new Ray(rayStart, GetComponent<Camera>().transform.forward);

        if (Physics.Raycast(lookRay, out rayHit, interactRange) && rayHit.collider.tag == "Interactable")
        {
            GameObject interactedObject = rayHit.collider.gameObject;

            if (Input.GetButtonDown("Interact"))
            {
                if (interactedObject.GetComponent<PickupObject>() != null)
                {
                    interactedObject.GetComponent<PickupObject>().PickedUp();
                }
                else if (interactedObject.GetComponent<CheckInventoryItems>() != null)
                {
                    interactedObject.GetComponent<CheckInventoryItems>().CheckItems();
                }
            }
        }
    }
}
