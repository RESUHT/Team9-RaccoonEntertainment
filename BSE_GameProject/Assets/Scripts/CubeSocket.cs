using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSocket : MonoBehaviour
{
    [SerializeField] private Animator mySocket = null;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerCharacter"))
        {
            mySocket.Play("SocketOn", 0, 0.0f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlayerCharacter"))
        {
            mySocket.Play("SocketOff", 0, 0.0f);
        }
    }
}
