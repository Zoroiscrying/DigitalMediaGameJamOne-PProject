using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectGenerator : MonoBehaviour
{

	public GameObject SequenceClickEffect;
	public GameObject ClickKeyDeadEffect;
	public GameObject EffectCanvas;
	
	
	private static ParticleEffectGenerator m_instance;
	public static ParticleEffectGenerator Instance {
		get { return m_instance; }
	}

	private void Awake()
	{
		m_instance = this;
	}

	public void OnClickKeyEffect(Vector2 pos)
	{
		var obj = Instantiate(ClickKeyDeadEffect, new Vector3(pos.x, pos.y, -10.0f), Quaternion.identity);
		obj.transform.SetParent(EffectCanvas.transform,false);
		Destroy(obj,1.0f);
	}

	public void OnSequenceClickEffect(Vector2 pos)
	{
		var obj = Instantiate(SequenceClickEffect, new Vector3(pos.x, pos.y, -10.0f), Quaternion.identity);
		obj.transform.SetParent(EffectCanvas.transform,false);
		Destroy(obj,1.0f);
	}
	
	// Use this for initialization

}
