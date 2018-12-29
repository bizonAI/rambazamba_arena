using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBox : MonoBehaviour
{

    public GameObject brokenSupplyBox;

    public int HitsToDestroy = 3;
    public int CurrentHitAmount = 0;

    // Use this for initialization
    private void Update()
    {

        if (CurrentHitAmount >= HitsToDestroy)
        {
            Instantiate(brokenSupplyBox, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            CurrentHitAmount += 1;

        }
    }
}