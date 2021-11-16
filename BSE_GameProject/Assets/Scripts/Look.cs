using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float m_Spin;
    public float m_Tilt;

    public Vector2 m_TiltExtents = new Vector2(-85.0f, 85.0f);

    public float m_Sensitivity = 2.0f;

    public bool m_CursorLocked = true;

    private void Start()
    {
        
        LockCursor();
        
    }

    private void LockCursor()
    {
        if (m_CursorLocked)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_CursorLocked = !m_CursorLocked;
            LockCursor();
        }

        if (!m_CursorLocked)
        {
            return;
        }

        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        m_Spin += x * m_Sensitivity;
        m_Tilt -= y * m_Sensitivity;

        m_Tilt = Mathf.Clamp(m_Tilt, m_TiltExtents.x, m_TiltExtents.y);

        transform.localEulerAngles = new Vector3(m_Tilt, m_Spin, 0.0f);
    }
}

