using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearAccelMove : LinearMove
{
	[SerializeField][FloatProperty("Acceleration")] private float acceleration = 1f;
	public float Acceleration
	{
		get => acceleration;
		set
		{
			acceleration = value;
			Restart();
		}
	}
	protected float moveTime = 0;

	public override void ObjectUpdate(float deltaTime)
	{
		var moveScalar = CalculateMoveScalar();
		var targetPosition = startPos + direction * moveScalar;
		transform.position = targetPosition;
		moveTime += deltaTime;
		if(transform.position.z > MaxDistance)
			Restart();
	}

	protected float CalculateMoveScalar() =>
		(StartSpeed * moveTime) + acceleration * moveTime * moveTime * 0.5f;

	public override void Restart()
	{
		base.Restart();
		moveTime = 0;
	}
}
