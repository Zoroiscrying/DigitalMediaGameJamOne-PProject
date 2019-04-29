using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimManager : MonoBehaviour {
	#region SingleTon

	public static UIAnimManager Instance
	{
		get { return _instance; }
	}

	private static UIAnimManager _instance;
	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}    
	#endregion

	public Animator CharacterAnimatior;


}
