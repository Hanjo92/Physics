using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ParabolaMove : LinearAccelMove
{
	public const float DropYPos = 0;
	protected Vector3 normalizeDir => direction.normalized;
	protected float StartYSpeed => (normalizeDir * StartSpeed).y;

	public override void ObjectUpdate(float deltaTime)
	{
		if(paused) return;

		var targetPosition = startPos;
		targetPosition += moveTime * StartSpeed * normalizeDir;
		targetPosition.y += Acceleration * moveTime * moveTime * 0.5f;
		if(targetPosition.y < DropYPos)
		{
			Restart();
			return;
		}
		
		transform.position = targetPosition;

		moveTime += deltaTime;
	}

	protected override void OnDrawGizmos()
	{
		Gizmos.DrawLine(transform.position, transform.position + normalizeDir);
		Handles.DrawWireDisc(CalculateDropPos(), Vector3.up, 0.5f);
	}

	public override void Restart()
	{
		base.Restart();
		moveTime = 0;
	}

	protected virtual Vector3 CalculateDropPos()
	{
		if(Acceleration > 0 || Acceleration == 0)
		{
			return startPos;
		}
		var limitYTime = -(2 * StartYSpeed) / Acceleration;
		var dropPos = startPos + limitYTime * StartSpeed * normalizeDir;
		dropPos.y = DropYPos;
		return dropPos;
	}
}
