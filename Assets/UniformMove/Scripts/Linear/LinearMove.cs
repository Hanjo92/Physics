using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinearMove : Move
{
	public float speed = 0.5f;
	public Vector3 direction = Vector3.forward;

	public override void ObjectUpdate(float deltaTime)
	{
		if(paused) return;

		var fDistance = speed * deltaTime;
		transform.Translate(direction * fDistance);
		
		// for demo
		if(transform.position.z > 16)
			Restart();
	}


	protected virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, direction);
	}

	public override void Restart()
	{
		base.Restart();
		transform.position = startPos;
	}
}
