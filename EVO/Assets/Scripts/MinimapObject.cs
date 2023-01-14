using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapObject : MonoBehaviour
{
    public MinimapObjectType ObjectType;

    private void Start() 
    {
        Debug.Log(MinimapController.Instance.MinimapObjectsDictionary);
        MinimapController.Instance.MinimapObjectsDictionary[ObjectType].Add(this.gameObject);
    }

    private void OnDestroy() {
        MinimapController.Instance.MinimapObjectsDictionary[ObjectType].Remove(this.gameObject);
    }
}
