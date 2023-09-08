using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class SmokeEntity : MonoBehaviour
{
	private SmokeGrenade smokeGrenade;
	private static int MapLayer => SmokeDefines.MapLayer;
	public Vector3 Position
	{
		get => transform.position;
		private set => transform.position = value;
	}
	private float AirResistance => smokeGrenade?.AirResistance ?? -1f;
	private float holdingTime; // for Fade
	private float explosionSpeed;
	private Vector3 moveDirection;
	private float floodDampingTime =>smokeGrenade?.FloodDampingTime ?? 0;
	private float fTime;
	public float Size => smokeGrenade.EntitySize;

	public static SmokeEntity CreateEntity(SmokeGrenade sg, Vector3 direction, float startSpeed, bool isCascade = false)
	{
		SmokeEntity entity;
		if(sg.EntityPrefab)
		{
			var obj = GameObject.Instantiate(sg.EntityPrefab);
			entity = obj.GetComponent<SmokeEntity>();
			if(entity == null)
				entity = obj.AddComponent<SmokeEntity>();
		}
		else
		{
			var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
			entity = obj.AddComponent<SmokeEntity>();
		}

		entity.smokeGrenade = sg;
		entity.holdingTime = sg.HoldingTime;
		entity.explosionSpeed = startSpeed;

		if(isCascade)
		{
			entity.transform.SetParent(sg.transform);
		}

		entity.Position = sg.transform.position;
		entity.transform.localScale = Vector3.one * entity.Size;

		entity.transform.rotation = isCascade ? Quaternion.identity : sg.transform.rotation;
		entity.moveDirection = direction;

		return entity;
	}

	public void DestroySmoke()
	{
		if(transform != null)
		{
			GameObject.DestroyImmediate(transform.gameObject);
		}
		Destroy(this);
	}

	public void SmokeUpdate(float deltaTime)
	{
		var moveDistance = CalculateDistance(fTime, deltaTime);
		if(moveDistance <= 0)
			return;
		while(moveDistance > 0)
		{
			while(Physics.Raycast(Position, moveDirection, out var info, moveDistance, MapLayer))
			{
				Position = info.point;
				moveDistance -= Mathf.Max(info.distance, 0.01f);
				var Reflect = Vector3.Reflect(moveDirection, info.normal);
				var lerpCenter = Vector3.Lerp(moveDirection, Reflect, 0.5f);
				moveDirection = Vector3.Lerp(Reflect, lerpCenter, SmokeDefines.ReflectDirection);
			}
			Position += moveDirection * moveDistance;
			moveDistance -= moveDistance;
		}
		fTime += deltaTime;
	}

	private float CalculateDistance(float fTime, float deltaTime)
	{
		var startSpeed = explosionSpeed + AirResistance * fTime;

		return (startSpeed + 0.5f * AirResistance * deltaTime) * deltaTime;
	}
}
