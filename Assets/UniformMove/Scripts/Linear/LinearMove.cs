using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LinearMove : Move
{
	[SerializeField][FloatProperty("StartSpeed")] private float startSpeed = 0.5f;
	public float StartSpeed
	{
		get => startSpeed;
		set
		{
			startSpeed = value;
			Restart();
		}
	}

	public override void ObjectUpdate(float deltaTime)
	{
		if(paused) return;

		var fDistance = StartSpeed * deltaTime;
		transform.Translate(direction * fDistance);
		
		// for demo
		if(transform.position.z > MaxDistance)
			Restart();
	}


	protected virtual void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawRay(transform.position, direction);
	}
}
