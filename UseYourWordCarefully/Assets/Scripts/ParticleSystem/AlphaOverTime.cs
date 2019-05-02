using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaOverTime : MonoBehaviour
{

	public AnimationCurve AlphaOverTimeCurve;

	private float _passingTime = 0;
	private float _startTime;
	private ParticleSystem.MainModule _particleSystem;
	// Use this for initialization
	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>().main;
	}

	void Start ()
	{
		_startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_passingTime = Time.time - _startTime;
		if (_passingTime > 1.0f)
		{
			_passingTime = 1.0f;
		}		
		float value = AlphaOverTimeCurve.Evaluate(_passingTime);
		Color adjustColor = _particleSystem.startColor.color;
		adjustColor.a = value;
		_particleSystem.startColor = adjustColor;
		//_particleSystem.startColor.color = new Color(1,1,1,value);
		//Debug.Log(_particleSystem.startColor.color);
	}
}
