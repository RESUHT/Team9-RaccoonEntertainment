using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public CharacterController m_Controller;
    public Look m_Look;
    public Camera cam;

    public Transform target;

    Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f);

    public float m_MoveSpeed = 8.0f;
    public float m_Gravity = 1.0f;
    public float m_JumpSpeed = 10.0f;
    public bool m_Sprinting;
    public float m_SprintModifier = 2.0f;
    public float m_GroundedLenience = 0.25f;

    public Interactable focus;

    public Vector3 m_Velocity = Vector3.zero;
    public bool m_Grounded = false;

    public float m_Acceleration = 8.0f;
    public AnimationCurve m_FrictionCurve;
    public float m_AirMomentumMult = 0.0f;

    public float m_GroundedTimer = 0.0f;
    public float m_DistanceTravelled = 0.0f;

    public Transform m_FeetPosition;
    public GameObject[] m_FootstepSounds;
    public float m_FootstepLength = 0.5f;

    public float m_JumpFootstepTimer = 0.0f;
    public float m_JumpFootstepCooldown = 0.5f;

    //Checkpoint
    public Transform m_Checkpoint;

    public bool m_IsRightFoot = true;

    public Transform m_Checkpoint;


    //public GameObject m_FootstepSound;
    private void Start()
    {
 
    }

    void SpawnFootstep()
    {
        m_IsRightFoot = !m_IsRightFoot;
        Vector3 footOffset = new Vector3(m_IsRightFoot ? 1.0f : -1.0f, 0.0f) * 0.25f;
        footOffset = Quaternion.Euler(0.0f, m_Look.m_Spin, 0.0f) * footOffset;
        //Destroy(Instantiate(m_FootstepSounds[Random.Range(0, 3)], m_FeetPosition.position + footOffset, m_FeetPosition.rotation), 1.0f);
    }
    // Update is called once per frame
    void Update()
    {

        float x = 0.0f;
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x += 1.0f;
        }

        float z = 0.0f;
        if (Input.GetKey(KeyCode.S))
        {
            z -= 1.0f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            z += 1.0f;
        }


        if (Input.GetKey(KeyCode.Space) && m_GroundedTimer > 0.0f)
        {
            m_Velocity.y = m_JumpSpeed;
            m_GroundedTimer = 0.0f;
            m_Grounded = false;

        }

        // Checkpoint Code 
        if (transform.position.y < -15.0f)
        {
            transform.position = m_Checkpoint.position;
            m_MoveSpeed = 0;
        }


        Vector3 inputMove = new Vector3(x, 0.0f, z);
        inputMove = Quaternion.Euler(0.0f, m_Look.m_Spin, 0.0f) * inputMove;

        if (inputMove.magnitude > 1.0f)
        {
            inputMove.Normalize();
        }


        float cacheY = m_Velocity.y;
        m_Velocity.y = 0.0f;

        float mag = m_Velocity.magnitude;

        float airMod = m_Grounded ? 1.0f : m_AirMomentumMult;
        m_Velocity += inputMove * m_Acceleration * Time.deltaTime * airMod;
        m_Velocity -= m_Velocity.normalized * m_Acceleration * m_FrictionCurve.Evaluate(mag) * Time.deltaTime * airMod;

        m_Velocity.y = cacheY;
        m_Velocity.y -= m_Gravity * Time.deltaTime;

        Vector3 trueVelocity = m_Velocity;
        trueVelocity.x *= m_MoveSpeed;
        trueVelocity.z *= m_MoveSpeed;

        Vector3 oldPos = transform.position;
        m_Controller.Move(trueVelocity * Time.deltaTime);
        Vector3 actualMove = transform.position - oldPos;

        m_JumpFootstepTimer -= Time.deltaTime;
        m_GroundedTimer -= Time.deltaTime;

        if ((m_Controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            if (!m_Grounded && m_JumpFootstepTimer < 0.0f)
            {
                m_JumpFootstepTimer = m_JumpFootstepCooldown;
                m_DistanceTravelled = 0.0f;
                SpawnFootstep();
            }

            m_Velocity.y = -1.0f;
            m_GroundedTimer = m_GroundedLenience;
            m_Grounded = true;
        }
        else
        {
            m_Grounded = false;
        }

        if (m_Grounded)
        {
            actualMove.y = 0.0f;
            m_DistanceTravelled += actualMove.magnitude;
            if (m_DistanceTravelled > m_FootstepLength)
            {
                m_DistanceTravelled -= m_FootstepLength;
                SpawnFootstep();
            }
        }

        m_Sprinting = Input.GetKey(KeyCode.LeftShift);

        float SprintMod = 12.0f;
        if (m_Sprinting && m_Grounded == true)
        {
            SprintMod = m_SprintModifier;
            if (m_MoveSpeed < 12.0f)
            {
                m_MoveSpeed += SprintMod;
            }
        }
        if (!m_Sprinting)
        {
            SprintMod = 12.0f;
            m_MoveSpeed = 8.0f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (focus = null)
            {
                Ray ray = Camera.main.ViewportPointToRay(rayOrigin);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
            }
            else
            {
                RemoveFocus();
            }
        }


    }


    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
    }


}
