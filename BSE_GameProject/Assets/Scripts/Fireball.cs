using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float delay = 3f;
    float countdown;
    bool hasExploded = false;
    public GameObject ExplosionEffect;
    public GameObject ExplosionEffect2;
    public GameObject ExplosionEffect3;
    public GameObject ExplosionEffect4;
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    public int damage = 20;
    public GameObject BoomSfx;




    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Destroy(Instantiate(ExplosionEffect, transform.position, transform.rotation), 2f);
        Destroy(Instantiate(ExplosionEffect2, transform.position, transform.rotation), 2f);
        //Destroy(Instantiate(ExplosionEffect3, transform.position, transform.rotation), 2f);
        Destroy(Instantiate(ExplosionEffect4, transform.position, transform.rotation), 2f);
        Destroy(Instantiate(BoomSfx, transform.position, transform.rotation), 3f);


        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);

                //enemy take damage
            }
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();

        if(collision.gameObject.tag == "Enemy")
        {

            EnemyTarget target = collision.transform.gameObject.GetComponent<EnemyTarget>();
            target.ApplyDamage(damage);

        }
        
    }
}
