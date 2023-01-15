using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapObject : MonoBehaviour
{
    public MinimapObjectType ObjectType;

    private void Start() 
    {
        if (MinimapController.Instance == null) return;
        MinimapController.Instance.MinimapObjectsDictionary[ObjectType].Add(this.gameObject);
        if (MinimapController.Instance.CurrentObjectType != ObjectType) gameObject.SetActive(false);
    }

    private void OnDestroy() {
        MinimapController.Instance?.MinimapObjectsDictionary[ObjectType].Remove(this.gameObject);
    }


	void LateUpdate() 
    {
        if (MinimapController.Instance == null) return;
        var cameraPosition = MinimapController.Instance.MinimapCamera.transform.position;
		transform.position = new Vector3(
            Mathf.Clamp(transform.parent.position.x, cameraPosition.x - MinimapController.Instance.MinimapSize, cameraPosition.x + MinimapController.Instance.MinimapSize),
            Mathf.Clamp(transform.parent.position.y, cameraPosition.y - MinimapController.Instance.MinimapSize, cameraPosition.y + MinimapController.Instance.MinimapSize),
            transform.parent.position.z
        );
	}
}
