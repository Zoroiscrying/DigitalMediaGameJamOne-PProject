using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RevealThoughtSystem : MonoBehaviour
{
	public CharacterManager CharacterManager;
	private static int _points;

	public static void AddPoints(int points)
	{
		_points += points;
	}

	public static int Point
	{
		get { return _points; }
	}

	
	
	//别人的秘密！！你要揭示它~~~~
	public void RevealPower()
	{
		CharacterManager.ActiveCharacter.DigSecret();
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
		{
			RevealPower();			
		}
	}
}
