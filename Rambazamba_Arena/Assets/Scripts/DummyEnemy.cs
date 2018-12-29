using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    public GameObject attacker;

    bool wasHit;
    float damage;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    void GetHit()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("MeleeAttacker") && other.gameObject != attacker)
        {
            anim.SetTrigger("gotHit");
            damage = other.GetComponent<Attacker>().damage;
            GetHit();
            Debug.Log("hit");
        }
    }
}
