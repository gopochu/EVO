using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.layer);
 
        if(collision.gameObject.CompareTag("player unit"))
            Destroy(this.gameObject);
       
    }
}
