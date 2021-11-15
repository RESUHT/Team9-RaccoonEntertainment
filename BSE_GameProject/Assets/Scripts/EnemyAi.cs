using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public int Health;

    //patrolling
    public Vector3 walkPoint;
    bool walkpointset;
    public float walkpointrange;

    //attacking
    public float attackinterval;
    bool haveattacked;

    //states
    public float sightrange, attackrange;
    public bool playerinattackrange, playerinsightrange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;

        agent.GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //check if player is in range
        playerinsightrange = Physics.CheckSphere(transform.position, sightrange, whatIsPlayer);
        playerinattackrange = Physics.CheckSphere(transform.position, attackrange, whatIsPlayer);

        if (!playerinsightrange && !playerinattackrange) Patrolling();
        if (playerinsightrange && !playerinattackrange) Chase();
        if (playerinattackrange && playerinsightrange) Attack();

    }

    private void Patrolling()
    {
        if (!walkpointset) SearchWalkPoint();

        if (walkpointset)
        {
            agent.SetDestination(walkPoint);

            Vector3 distancetowalkpoint = transform.position - walkPoint;

            if (distancetowalkpoint.magnitude < 1)
            {
                walkpointset = false;
            }
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkpointrange, walkpointrange);
        float randomX = Random.Range(-walkpointrange, walkpointrange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkpointset = true;
        }
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        //stop enemy moving
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!haveattacked)
        {
            //attack code here





            haveattacked = true;
            Invoke(nameof(ResetAttack), attackinterval);
        }
    }

    private void ResetAttack()
    {
        haveattacked = false;
    }

    public void EnemyTakeDamage()
    {

    }
}
