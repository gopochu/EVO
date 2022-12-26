using System;
using UnityEngine;


public class Swamp : MonoBehaviour
{
    [SerializeField] public float speedMultiplier = 0.5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var walking = col.gameObject.GetComponent<IWalking>();
        walking?.ChangeSpeedMultiplier(speedMultiplier);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var walking = other.gameObject.GetComponent<IWalking>();
        walking?.ChangeSpeedMultiplier(1/speedMultiplier);
    }
}