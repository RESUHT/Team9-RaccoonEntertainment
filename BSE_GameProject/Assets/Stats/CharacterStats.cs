using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat armour;
    public int maxHealth = 100;
    public int currentHealth { get; private set; }


    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }



    public void TakeDamage(int damage)
    {

        damage -= armour.getValue();
        damage = Mathf.Clamp(damage, 1, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + "takes" + damage + "damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public virtual void Die()
    {
        //die somehow
        //this method is meant to be overwritten
    }
}
