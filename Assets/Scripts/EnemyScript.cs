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
    public bool closeProximity;
    public bool weaponOut;

    public int attacked;

    public NavMeshAgent agent;
    public GameObject player;

    public GameObject weapon;
    public GameObject patrolPoint1;
    public GameObject patrolPoint2;
    public GameObject patrolPoint3;
    public GameObject patrolPoint4;


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
        closeProximity = false;
        agent.SetDestination(patrolPoint1.transform.position);
    }

    void Update()
    {
        SetStateColour();

        if (attacked >= 100)
        {   
            eState = enemyState.unconcious;
        }

        switch (eState)
        {
            case enemyState.patrol:
                Patrolling();
                break;

            case enemyState.chase:
                Chasing();
                break;

            case enemyState.search:
                Searching();
                break;

            case enemyState.attack:
                Attacking();
                break;

            case enemyState.retreat:
                Retreating();
                break;

            case enemyState.unconcious:
                Unconcious();
                break;
        }
    }

    private void Patrolling()
    {
        if (seesPlayer)
        {
            eState = enemyState.chase;
            timeBool = true;
            return;
        }

        //abs values

        //Vector3.Distance

        if (transform.position.x == patrolPoint4.transform.position.x && transform.position.z == patrolPoint4.transform.position.z)
        {
            agent.SetDestination(patrolPoint1.transform.position);
        }
        else if (transform.position.x == patrolPoint1.transform.position.x && transform.position.z == patrolPoint1.transform.position.z) 
        {
            agent.SetDestination(patrolPoint2.transform.position);
        }
        else if (transform.position.x == patrolPoint2.transform.position.x && transform.position.z == patrolPoint2.transform.position.z)
        {
            agent.SetDestination(patrolPoint3.transform.position);
        }
        else if (transform.position.x == patrolPoint3.transform.position.x && transform.position.z == patrolPoint3.transform.position.z)
        {
            agent.SetDestination(patrolPoint4.transform.position);
        }



    }

    private void Chasing()
    {
        agent.SetDestination(player.transform.position);

        if (seesPlayer)
        {
            currentTime = 0;
            timeBool = true;
        }

        if (!seesPlayer)
        {
            if (timeBool == true)
            {
                currentTime = Time.time;
                timeBool = false;
            }

            timeElapsed = Time.time - currentTime;

            if (timeElapsed >= 5.0f)
            {
                timeBool = true;
                eState = enemyState.search;
            }
        }

        else if (seesPlayer && closeProximity)
        {
            eState = enemyState.attack;
        }

    }

    private void Searching()
    {
        agent.SetDestination(transform.position);
        transform.Rotate(0, 0.1f, 0);

        if (timeBool)
        {
            timeBool = false;
            currentTime = Time.time;
        }

        timeElapsed = Time.time - currentTime;

        if (seesPlayer)
        {
            timeBool = true;
            eState = enemyState.chase;
            return;
        }

        if (timeElapsed >= 7)
        {
            timeBool = true;
            agent.SetDestination(patrolPoint1.transform.position);
            eState = enemyState.patrol;

        }
    }

    private void Attacking()
    {
        agent.SetDestination(player.transform.position);

        if (timeBool)
        {
            currentTime = Time.time;
            timeBool = false;
        }

        timeElapsed = Time.time - currentTime;

        if (timeElapsed >= 1.0 && !weaponOut && closeProximity)
        {
            weapon.GetComponent<BoxCollider>().enabled = true;
            weapon.GetComponent<MeshRenderer>().enabled = true;
            weaponOut = true;
            currentTime = Time.time;
        }
        else if (timeElapsed >= 1.0 && weaponOut)
        {
            weapon.GetComponent<BoxCollider>().enabled = false;
            weapon.GetComponent<MeshRenderer>().enabled = false;
            weaponOut = false;
            currentTime = Time.time;
        }

        if (timeElapsed >= 7 && !seesPlayer)
        {
            timeBool = true;
            agent.SetDestination(patrolPoint1.transform.position);
            eState = enemyState.search;
        }
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

        if (timeElapsed >= 5.0f && !seesPlayer)
        {
            currentTime = 0;
            timeBool = true;
            agent.SetDestination(patrolPoint1.transform.position);
            eState = enemyState.patrol;
        }
    }

    private void Unconcious()
    {
        agent.SetDestination(transform.position);

        if (timeBool == true)
        {
            currentTime = Time.time;
            timeBool = false;
        }

        timeElapsed = Time.time - currentTime;

        if (timeElapsed >= 10.0f)
        {
            attacked = 0;
            currentTime = 0;
            timeBool = true;
            agent.SetDestination(patrolPoint1.transform.position);
            eState = enemyState.patrol;
        }
    }

    private void SetStateColour()
    {
        if (eState == enemyState.patrol)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
        else if (eState == enemyState.chase)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (eState == enemyState.search)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        else if (eState == enemyState.attack)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (eState == enemyState.retreat)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (eState == enemyState.unconcious)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

}
