using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : UpdateObject
{
	public const float MaxDistance = 16;
	[SerializeField] protected Vector3 startPos;
	public Vector3 direction = Vector3.forward;
	protected override void Start()
	{
		base.Start();

		startPos = transform.position;
	}

	public override void Restart()
	{
		base.Restart();
		transform.position = startPos;
	}
}
