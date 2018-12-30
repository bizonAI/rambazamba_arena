using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBox : MonoBehaviour
{

    public GameObject brokenSupplyBox;
    public int hitsToDestroy = 3;
    public int currentHitAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MeleeAttacker"))
        {
            currentHitAmount += 1;

            if (currentHitAmount >= hitsToDestroy)
            {
                Instantiate(brokenSupplyBox, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(gameObject);
            }
        }
    }

 
}