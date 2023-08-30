using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CircularMove : Move
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

	protected override void Start()
	{
		base.Start();
		transform.position += Vector3.right * Radius;
	}

	public override void ObjectUpdate(float deltaTime)
	{
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
		transform.RotateAround(startPos, Vector3.down, deltaAngle);
	}

	private void OnDrawGizmos()
	{
		Handles.DrawWireDisc(startPos, Vector3.down, radius);
	}

	public override void Restart()
	{
		base.Restart();
		transform.position += Vector3.right * Radius;
	}
}
