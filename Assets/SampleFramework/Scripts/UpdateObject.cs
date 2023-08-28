using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateObject : MonoBehaviour
{
    public bool paused = false;

    void Start()
    {
        if(UpdateManager.Instance == null)
        {
            Debug.LogError("No Update Manager");
            Destroy(gameObject);
            return;
        }

    }

    public virtual void ObjectUpdate(float deltaTime) { }

	[ContextMenu("ContextTest")]
	public void ContextTest() { }
}
