using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearAccelMoveTwoPoint : LinearAccelMove
{
	public Transform target;
	public Transform target2;

	public override void ObjectUpdate(float deltaTime)
	{
		if(paused)
			return;
		if(target == null || target2 == null)
			return;
		var fullDistance = Vector3.Distance(target.position, target2.position);
		var moveScalar = CalculateMoveScalar();
		var ratio = moveScalar / fullDistance;

		transform.position = Vector3.Lerp(target.position, target2.position, ratio);
		moveTime += deltaTime;
		if(ratio >= 1)
			Restart();
	}

	public override void Restart()
	{
		base.Restart();
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
