using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    void OnTriggerEnter(Collider _other)
    {
        Motor motor = _other.GetComponent<Motor>();
        if (motor)
        {
            motor.m_Checkpoint = transform;
        }
    }
}
