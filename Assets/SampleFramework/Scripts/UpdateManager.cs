using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private static UpdateManager instance;
    public static UpdateManager Instance => instance;

	[ToggleButton("Paused")]
	[SerializeField] private bool mPaused = false;
	public bool Paused {
		get => mPaused;
		set
		{
			Debug.Log($"Change :: Current {mPaused}");
			mPaused = value;
		} 
	}

	[Range(0f, 10f)] public float timeScale = 1f;
	public List<UpdateObject> objects = new List<UpdateObject>();

	private void Awake()
	{
		if(Instance)
			Destroy(this.gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}
	void Update()
    {
        if(Paused) return;

		var deltaTime = Time.deltaTime * timeScale;
		foreach(var obj in objects)
		{
			obj.ObjectUpdate(deltaTime);
		}
    }
}
