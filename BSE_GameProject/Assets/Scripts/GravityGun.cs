using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun : MonoBehaviour
{
    public Transform m_Camera;
    public Transform m_HeldTarget;
    public Rigidbody m_AttatchedProp;
    public Transform m_CharacterMotor;

    public float m_GravMoveSpeed = 8.0f;
    public float m_GravRotateSpeed = 16.0f;

    public float m_GravLaunchStrength = 1.0f;

    public LayerMask m_PickupLayerMask;
    public bool m_holding = false;

    void Update()
    {
        

        if (Input.GetMouseButtonDown(0) && m_holding == false)
        {
            m_holding = true;

            RaycastHit hit;
            if(Physics.Raycast(m_Camera.position, m_Camera.forward, out hit, 5.0f, m_PickupLayerMask))
            {
                m_AttatchedProp = hit.transform.GetComponent<Rigidbody>();
                m_AttatchedProp.useGravity = false;
                m_AttatchedProp.angularVelocity = Vector3.zero;
                m_AttatchedProp.velocity = Vector3.zero;
                m_AttatchedProp.isKinematic = true;
                m_AttatchedProp.transform.SetParent(m_CharacterMotor);
                
            }
        }

        if(m_AttatchedProp)
        {
            
            m_AttatchedProp.angularVelocity = Vector3.zero;
            m_AttatchedProp.velocity = Vector3.zero;
            m_AttatchedProp.transform.position = Vector3.Lerp(m_AttatchedProp.transform.position, m_HeldTarget.position, m_GravMoveSpeed * Time.deltaTime);
            m_AttatchedProp.transform.rotation = Quaternion.Slerp(m_AttatchedProp.transform.rotation, m_HeldTarget.rotation, m_GravRotateSpeed * Time.deltaTime);

            if(Input.GetMouseButtonUp(0) && m_holding == true)
            {
                m_holding = false;
                m_AttatchedProp.transform.SetParent(null);
                m_AttatchedProp.useGravity = true;
                m_AttatchedProp.isKinematic = false;
                

                m_AttatchedProp.AddForce(m_Camera.forward * m_GravLaunchStrength, ForceMode.Impulse);

                m_AttatchedProp = null;
            }
        }
        else
        {
            m_holding = false;
        }
    }
}
