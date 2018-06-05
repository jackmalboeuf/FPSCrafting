using UnityEngine;
using System.Collections;

public class TeleportTrigger : MonoBehaviour
{
    [SerializeField]
    Transform teleportLocation;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = teleportLocation.position;            
        }
    }
}
