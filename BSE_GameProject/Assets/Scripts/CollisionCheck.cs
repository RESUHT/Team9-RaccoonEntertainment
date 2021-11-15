using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    [SerializeField] public GameObject teleportTarget;
    [SerializeField] public Transform teleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("Teleport"))
        {
            Debug.Log("Passed if check");
            teleportTarget.transform.position = teleportDestination.transform.position;
            
        }
    }

}
