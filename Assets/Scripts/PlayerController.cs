using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public int attacked;
    private Quaternion initalRot;

    public GameObject weapon;

    private bool timeBool = true;

    private float timeElapsed;
    private float currentTime;

    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        initalRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (attacked >= 100)
        {
            alive = false;
        }

        if (!alive)
        {
            if (timeBool)
            {
                currentTime = Time.time;
                timeBool = false;
            }

            timeElapsed = Time.time - currentTime;

            if (timeElapsed >= 10.0)
            {
                timeBool = true;
                alive = true;
                attacked = 0;
            }
        }

        if (!alive) return;

        transform.rotation = initalRot; //prevents accidental rotation off walls

        timeElapsed = Time.time - currentTime;

        if(timeElapsed >= 1.0)
        {
            weapon.GetComponent<BoxCollider>().enabled = false;
            weapon.GetComponent<MeshRenderer>().enabled = false;
            timeBool = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (timeBool)
            {
                currentTime = Time.time;
                timeBool = false;
            }
            weapon.GetComponent<BoxCollider>().enabled = true;
            weapon.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
