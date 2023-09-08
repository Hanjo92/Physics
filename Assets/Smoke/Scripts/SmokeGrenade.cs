using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class SmokeGrenade : UpdateObject
{
	public enum ExplosionShape
	{
		Sphere,
		Disc
	}
	[SerializeField][Range(1, 1000)] private int entityCount = 1;
	[SerializeField][Range(0.05f, 1f)] private float entitySize = 0.1f;
	[SerializeField] private ExplosionShape explosionShape = ExplosionShape.Sphere;
	[SerializeField][Range(2, 32)] int directionCount = 6;

	public float EntitySize => entitySize;
	[SerializeField][Range(0.1f, 10f)] private float explosionPower = 2f;
	public float ExplosionSpeed => explosionPower;
	[SerializeField] private AnimationCurve explosionPowerCurve = new AnimationCurve();
	[SerializeField][Range(3f, 30f)] private float holdingTime = 10f;
	public float HoldingTime => holdingTime;

	[SerializeField] private float airResistance = -1f;
	public float AirResistance => airResistance;
	[SerializeField][Range(0.1f, 10f)] private float floodDampingTime = 3f;
	public float FloodDampingTime => floodDampingTime;

	[SerializeField]private SmokeEntity entityPrefab;
	public SmokeEntity EntityPrefab => entityPrefab;

	[SerializeField] private SmokeEntity[] smokeEntities = null;

	[SerializeField] private bool cascadeEntity = false;

	[Button("Explosion")] public bool explosion = true;
	public void Explosion()
	{
		if(smokeEntities.Length > 0)
		{
			for(int i = 0; i < smokeEntities.Length; i++)
			{
				smokeEntities[i].DestroySmoke();
			}
		}

		smokeEntities = new SmokeEntity[entityCount];
		for(int i = 0; i < entityCount; i++)
		{
			smokeEntities[i] = SmokeEntity.CreateEntity(this, CalculateFragmentsDirection(i), CalculateFragmentsPower(i), cascadeEntity);
		}
	}

	public override void ObjectUpdate(float deltaTime)
	{
		if(smokeEntities != null && smokeEntities.Length > 0)
		{
			foreach(var entity in smokeEntities)
			{
				entity?.SmokeUpdate(deltaTime);
			}
		}
	}

	private Vector3 CalculateFragmentsDirection(int index)
	{
		var angleSlice = 360f / directionCount;
		if(explosionShape == ExplosionShape.Disc)
		{
			return transform.rotation * Quaternion.Euler(0f, angleSlice * index, 0f) * transform.forward;
		}
		else
		{
			var dir = Vector3.zero;
			var iLerp = Mathf.Lerp(1f, -0.1f, index / (float)(entityCount));
			dir.x = iLerp;
			dir.y = 1 - iLerp;
			return Quaternion.Euler(angleSlice * index, angleSlice * index, angleSlice * index) * transform.rotation * dir.normalized;
		}
	}
	private float CalculateFragmentsPower(int index)
	{
		var ratio = index / (float)entityCount;
		var powerCurve = (explosionPowerCurve.length > 0) ? explosionPowerCurve.Evaluate(ratio) : Mathf.Cos(Mathf.PI * 0.5f * ratio);
		return powerCurve * explosionPower;
	}

#if UNITY_EDITOR
	[Range(0, 3f)] public float gizmoRadius = 1;
	public Color gizmoColor = Color.white;
#endif
	private void OnDrawGizmos()
	{
		if(explosionShape == ExplosionShape.Disc)
		{
			Handles.color = gizmoColor;
			Handles.DrawSolidDisc(transform.position, transform.up, gizmoRadius);
		}
		else if(explosionShape == ExplosionShape.Sphere)
		{
			Gizmos.color = gizmoColor;
			Gizmos.DrawSphere(transform.position, gizmoRadius);
		}

		if(smokeEntities != null && smokeEntities.Length > 0)
		{
			foreach(var entity in smokeEntities)
			{
				Gizmos.DrawCube(entity.Position, Vector3.one * EntitySize);
			}
		}
	}
}
