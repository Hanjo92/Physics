using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	[SerializeField][SerializedProperty("TInt")]private int tInt;
	public int TInt
	{
		get => tInt; 
		set
		{
			tInt = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TIntR")][Range(0, 10)] private int tIntR;
	public int TIntR
	{
		get => tIntR;
		set
		{
			tIntR = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TB")] private bool tB;
	public bool TB
	{
		get => tB;
		set
		{
			tB = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TF")] private float tF;
	public float TF
	{
		get => tF;
		set
		{
			tF = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TS")] private string tS;
	public string TS
	{
		get => tS;
		set
		{
			tS = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TColor")] private Color tColor;
	public Color TColor
	{
		get => tColor;
		set
		{
			tColor = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TV2")] private Vector2 tV2;
	public Vector2 TV2
	{
		get => tV2;
		set
		{
			tV2 = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TV3")] private Vector3 tV3;
	public Vector3 TV3
	{
		get => tV3;
		set
		{
			tV3 = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TV4")] private Vector4 tV4;
	public Vector4 TV4
	{
		get => tV4;
		set
		{
			tV4 = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TRect")] private Rect tRect;
	public Rect TRect
	{
		get => tRect;
		set
		{
			tRect = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TV2I")] private Vector2Int tV2I;
	public Vector2Int TV2I
	{
		get => tV2I;
		set
		{
			tV2I = value;
			Debug.Log(".");
		}
	}
	[SerializeField][SerializedProperty("TV3I")] private Vector3Int tV3I;
	public Vector3Int TV3I
	{
		get => tV3I;
		set
		{
			tV3I = value;
			Debug.Log(".");
		}
	}
}
