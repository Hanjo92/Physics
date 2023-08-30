using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaMoveByXZDist : ParabolaMove
{
	[SerializeField]
	[FloatProperty("MovingTime")]
	[Range(0.1f, 5f)] protected float movingTime = 1;
	public float MovingTime
	{
		get => movingTime;
		set
		{
			movingTime = value;
			Restart();
		}
	}

	[SerializeField][FloatProperty("Distance")][Range(0, MaxDistance)] private float distance;
	public float Distance
	{
		get => distance;
		set
		{
			distance = value;
			Restart();
		}
	}

	private Vector3 XZDirection => new Vector3(normalizeDir.x, 0, normalizeDir.z);
	private new float StartSpeed => distance / XZDirection.magnitude / MovingTime;

	public override void ObjectUpdate(float deltaTime)
	{
		if(paused)
			return;

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

	protected override Vector3 CalculateDropPos()
	{
		if(Acceleration > 0 || Acceleration == 0)
		{
			return startPos;
		}
		var dropPos = startPos + XZDirection.normalized * distance;
		dropPos.y = DropYPos;
		return dropPos;
	}
}
