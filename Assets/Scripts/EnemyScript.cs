using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    private float timeElapsed;
    private float currentTime;
    private bool timeBool;

    public bool seesPlayer;

    public NavMeshAgent agent;
    public GameObject player;

    private enum enemyState
    {
        patrol,
        chase,
        search,
        attack,
        retreat,
        unconcious
    }

    enemyState eState;

    void Start()
    {
        eState = enemyState.patrol;
        seesPlayer = false;
    }

    void Update()
    {
        switch (eState)
        {
            case enemyState.patrol:
                //code here
                break;

            case enemyState.chase:
                Chasing();
                break;

            case enemyState.search:
                //code here
                break;

            case enemyState.attack:
                //code here
                break;

            case enemyState.retreat:
                //code here
                break;

            case enemyState.unconcious:
                Unconcious();
                break;

        }
    }

    private void Patrolling()
    {
        //no clue?
    }

    private void Chasing()
    {
        agent.SetDestination(player.transform.position);

        if (!seesPlayer)
        {
            if (timeBool == true)
            {
                currentTime = Time.time;
                timeBool = false;
            }

            timeElapsed = Time.time - currentTime;

            if (timeElapsed > 5.0f)
            {
                currentTime = 0;
                timeBool = true;
                eState = enemyState.search;
            }
        }
        else if (seesPlayer)
        {
            if (Mathf.Abs(transform.position.x - player.transform.position.x) <= 2 && Mathf.Abs(transform.position.y - player.transform.position.y) <= 2)
            {
                eState = enemyState.attack;
            }
        }

    }

    private void Searching()
    {
        //to chasing/retreating
    }

    private void Attacking()
    {
        agent.SetDestination(player.transform.position);

        //idk how to transition to retreat/unconcious
    }

    private void Retreating()
    {
        if (seesPlayer)
        {
            currentTime = 0;
            timeBool = true;
            eState = enemyState.chase;

        }

        if (timeBool && !seesPlayer)
        {
            currentTime = Time.time;
            timeBool = false;
        }

        timeElapsed = Time.time - currentTime;
         
        //agent.SetDestination(patrolTarget1)

        if (timeElapsed >= 5.0f && !seesPlayer)
        {
            currentTime = 0;
            timeBool = true;
            eState = enemyState.patrol;
        }
    }

    private void Unconcious()
    {
        if (timeBool == true)
        {
            currentTime = Time.time;
            timeBool = false;
        }

        timeElapsed = Time.time - currentTime;

        if (timeElapsed >= 10.0f)
        {
            currentTime = 0;
            timeBool = true;
            eState = enemyState.patrol;
        }
    }

    private void ColourState()
    {
        //colours enemy based on state
    }

}
