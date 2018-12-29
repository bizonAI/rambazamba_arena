using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXList : MonoBehaviour
{
    // Start is called before the first frame update

    public void Hit()
    {
        FindObjectOfType<AudioManager>().Play("Hit");
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Item")
        {
            FindObjectOfType<AudioManager>().Play("ColectItem");
           
        }
    }
}
