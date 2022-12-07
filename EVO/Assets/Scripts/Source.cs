using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    private bool _isOccupied;

    public bool IsOccupied
    {
        get => _isOccupied;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Mineshaft mine;
        Debug.Log("Something there");
        if(other.TryGetComponent<Mineshaft>(out mine))
        {
            Debug.Log("Mine found");
            _isOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Mineshaft mine;
        if(other.TryGetComponent<Mineshaft>(out mine))
        {
            _isOccupied = false;
        }
    }
}
