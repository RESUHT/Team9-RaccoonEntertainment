using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePickup : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(0f, 120f, 0f) * Time.deltaTime);
    }
}