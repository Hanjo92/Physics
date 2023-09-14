using UnityEngine;

public class MeetPointDemo : MonoBehaviour
{
	public float radius = 5f;
	public Vector3 line = Vector3.forward;
	public Vector3 LineNormal => line.normalized;
	public Vector3 lineAcrossPoint = Vector3.zero;
	public float meetPointSize = 0.1f;

	public Transform point1;
	public Transform point2;

	private void OnDrawGizmos()
	{
		var p = lineAcrossPoint;
		var v = LineNormal;
		var r = radius;
		if(point1 && point2)
		{
			p = point1.position;
			v = (point2.position - point1.position).normalized;
			r = Vector3.Distance(point2.position, point1.position);
		}

		Gizmos.color = Color.white * 0.5f;
		Gizmos.DrawSphere(transform.position, r);
		Gizmos.color = Color.white;
		Gizmos.DrawRay(p, v * 100);
		Gizmos.DrawRay(p, -v * 100);

		var state = MeetPointCalculator.Calculate(transform.position, r, p, v, out var meetPoints);
		switch(state)
		{
			case MeetPointCalculator.State.Zero:
			break;
			case MeetPointCalculator.State.OnePoint:
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(meetPoints[0], meetPointSize);
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine(transform.position, meetPoints[0]);
			}
			break;
			case MeetPointCalculator.State.TwoPoints:
			{
				Gizmos.color = Color.red;
				Gizmos.DrawLine(transform.position, meetPoints[0]);
				Gizmos.DrawLine(transform.position, meetPoints[1]);
				Gizmos.DrawSphere(meetPoints[0], meetPointSize);
				Gizmos.DrawSphere(meetPoints[1], meetPointSize);

				Gizmos.color = Color.yellow;
				var center = Vector3.Lerp(meetPoints[0], meetPoints[1], 0.5f);
				Gizmos.DrawSphere(center, meetPointSize);
				Gizmos.DrawLine(transform.position, center);

				Gizmos.color = Color.green;
				Gizmos.DrawLine(meetPoints[0], meetPoints[1]);
			}
			break;
		}
	}
}
