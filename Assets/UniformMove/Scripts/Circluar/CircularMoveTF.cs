using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class CircularMoveTF : Move
{
	[SerializeField][FloatProperty("Radius")][Range(0, 6f)] private float radius = 1f;
	public float Radius
	{
		get => radius;
		set
		{
			radius = value;
			Restart();
		}
	}

	public float angularSpeed = 15;

    public bool byTime;
	public float time;

	private float degree = 0f;

	protected override void Start()
	{
		base.Start();
	}

	public override void ObjectUpdate(float deltaTime)
	{
		Vector3 nextPosition = startPos;
		float deltaAngle;

		if(byTime && time != 0f)
		{
			var angularSpeedByTime = (360f / time);
			deltaAngle = angularSpeedByTime * deltaTime;
		}
		else
		{
			deltaAngle = angularSpeed * deltaTime;
		}
		degree += deltaAngle;
		degree %= 360f;

		nextPosition.x += Radius * MathF.Cos(Mathf.Deg2Rad * degree);
		nextPosition.z += Radius * MathF.Sin(Mathf.Deg2Rad * degree);

		transform.position = nextPosition;
	}

	public override void Restart()
	{
		base.Restart();
		degree = 0;
	}

	private void OnDrawGizmos()
	{
		Handles.DrawWireDisc(startPos, Vector3.down, radius);
	}
}
