using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    public GameObject player;

    public float damage;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject != player)
        {
            Debug.Log("coolideingnggns");
            gameObject.SetActive(false);
        }
        
    }
}
