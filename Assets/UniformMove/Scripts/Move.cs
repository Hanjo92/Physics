using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : UpdateObject
{
	[SerializeField] protected Vector3 startPos;
	protected override void Start()
	{
		base.Start();

		startPos = transform.position;
	}
}
