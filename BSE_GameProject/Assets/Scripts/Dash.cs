using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    Motor moveScript;
    Look lookScript;
    public GameObject DashSfx;
    public float m_DashSpeed;
    public float m_DashTime;
    public bool m_CanDash = false;
    public bool m_Grounded = false;
    public Vector3 m_DashDir = Vector3.zero;
    public Vector3 m_DashVel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<Motor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveScript.m_Grounded == true)
        {
            m_CanDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && m_CanDash == true)
        {
            StartCoroutine(DashAction());
            Destroy(Instantiate(DashSfx, moveScript.m_FeetPosition.position, moveScript.m_FeetPosition.rotation), 1.0f);
        }
    }

    IEnumerator DashAction()
    {
        m_CanDash = false;
        float startTime = Time.time;

        while (Time.time < startTime + m_DashTime)
        {
            m_DashVel = new Vector3(moveScript.m_Velocity.x, 0.0f, moveScript.m_Velocity.z);
            moveScript.m_Controller.Move(m_DashVel * m_DashSpeed * Time.deltaTime);

            yield return null;
        }
    }

}
