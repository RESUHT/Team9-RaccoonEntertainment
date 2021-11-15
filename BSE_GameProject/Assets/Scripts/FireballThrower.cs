using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballThrower : MonoBehaviour
{
    public float throwForce = 700f;
    public GameObject FireballPrefab;
    public Transform castPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowFireball();
        }
    }

    void ThrowFireball()
    {
        GameObject fireball = Instantiate(FireballPrefab, castPoint.transform.position, castPoint.transform.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        rb.AddForce(castPoint.transform.forward * throwForce);
    }



}
