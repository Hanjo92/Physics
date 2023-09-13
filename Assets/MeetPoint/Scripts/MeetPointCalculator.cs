using System;
using System.Collections.Generic;
using UnityEngine;

public static class MeetPointCalculator
{
	public enum State
	{
		Zero,
		OnePoint,
		TwoPoints
	}

	public static State Calculate(Vector3 point, float range, Vector3 pointOnLine, Vector3 lineDirection, out List<Vector3> meetPoints)
	{
		meetPoints = new List<Vector3>();
		var vectorForTheta = Vector3.Normalize(point - pointOnLine);
		var theta = Mathf.Acos(Vector3.Dot(lineDirection, vectorForTheta));
		var distance = MathF.Sin(theta) * Vector3.Distance(point, pointOnLine);
		var minDistVector = CalculateMinimumDistanceVector(point, pointOnLine, lineDirection);
		var crossPoint = point + minDistVector * distance;
		if(distance < range)
		{
			var l = MathF.Sqrt(range * range - distance * distance);
			meetPoints.Add(crossPoint + lineDirection * l);
			meetPoints.Add(crossPoint + lineDirection * -l);
			return State.TwoPoints;
		}
		else if(distance == range)
		{
			meetPoints.Add(crossPoint);
			return State.OnePoint;
		}

		return State.Zero;
	}

	private static Vector3 CalculateMinimumDistanceVector(Vector3 point, Vector3 lineCrossPoint, Vector3 lineDirection)
	{
		var pointToPoint = Vector3.Normalize(point - lineCrossPoint);
		var normalVector = Vector3.Cross(pointToPoint, lineDirection);
		var rotate = Quaternion.AngleAxis(90f, normalVector);
		return rotate * lineDirection;
	}
}
