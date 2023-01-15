using System;
using UnityEngine;


public class Swamp : MonoBehaviour
{
    [SerializeField] public float speedMultiplier = 0.5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        var walking = col.gameObject.GetComponent<IWalking>();
        walking?.ChangeSpeedMultiplier(speedMultiplier);
        Debug.Log(walking?.SpeedMultiplier);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other);
        var walking = other.gameObject.GetComponent<IWalking>();
        walking?.ChangeSpeedMultiplier(1/speedMultiplier);
        Debug.Log(walking?.SpeedMultiplier);
    }
}