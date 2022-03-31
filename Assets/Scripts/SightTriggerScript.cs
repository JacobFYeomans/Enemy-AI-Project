using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightTriggerScript : MonoBehaviour
{
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyScript>().seesPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.GetComponent<EnemyScript>().seesPlayer = false;
        }
    }
}
