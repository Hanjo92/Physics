using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMoveByTwoPoints : LinearMove
{
	public bool byTime = false;
	public bool ByTime
	{ 
		get => byTime;
		set
		{
			byTime = value;
			Restart();
		}
	}
	public Transform target;
	public Transform target2;
	public float time = 1f;
	private float fTime = 0;

	public override void ObjectUpdate(float deltaTime)
	{
		if(paused)
			return;
		if(target == null || target2 == null) 
			return;

		var destTime = ByTime ? time : Vector3.Distance(target2.position, target.position) / speed;

		fTime += deltaTime;
		transform.position = Vector3.Lerp(target.position, target2.position, fTime / destTime);
		if(fTime > destTime)
			Restart();
	}

	public override void Restart()
	{
		base.Restart();
		fTime = 0f;
		if(target != null)
			transform.position = target.position;
	}

	protected override void OnDrawGizmos()
	{
		if(target == null) 
			return;
		if(target == null || target2 == null)
			return;

		Gizmos.DrawLine(target.position, target2.position);

		Gizmos.DrawSphere(target.position, 0.2f);
		Gizmos.DrawSphere(target2.position, 0.2f);
	}
}
