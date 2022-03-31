using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    public GameObject character;

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
            character.GetComponent<PlayerController>().attacked++;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            character.GetComponent<EnemyScript>().attacked++;
        }
    }
}
