using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthBar hp;

    public float m_Speed = 2.0f;
    public float m_DRadius = 1.0f;

    private void OnTriggerStay(Collider _other)
    {
        if (_other.GetComponent<Motor>())
        {
            transform.position = Vector3.Lerp(transform.position, _other.transform.position, m_Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _other.transform.position) < m_DRadius)
            {
                FindObjectOfType<Motor>().currentHealth += 25;
                FindObjectOfType<HealthBar>().slider.value += 25;
                Destroy(gameObject);
            }
            if (FindObjectOfType<Motor>().currentHealth == 100)
            {
                transform.position = Vector3.down;
            }
        }
    }
    private void Update()
    {

    }
}
