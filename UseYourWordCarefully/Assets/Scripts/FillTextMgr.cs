using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FillTextMgr
{
	public static FillTextMgr Instance
	{
		get{
			if (_instance == null)
			{
				_instance = new FillTextMgr();
				return _instance;
			}
			return _instance;
		}
	}
	private static FillTextMgr _instance;
	
	//public
	public TextMeshProUGUI TMP_Current;
	
	//private
	private string tmp_text;
	private Queue<Vector2> slotPositionsQueue;
	
	private void Awake()
	{
		if (TMP_Current)
		{
			tmp_text = TMP_Current.text;
		}
	}

	// Use this for initialization
	void Start () {
		AnalyzeCurrentTMP();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// analyze '___' and set the positions queue
	/// </summary>
	private void AnalyzeCurrentTMP()
	{
		//we need to find every '_'s first pos and last pos
		bool justFindSlot = false;
		int firstPos = 0;
		int lastPos = 0;
		
		for (int i = 0; i < tmp_text.Length; i++)
		{
			//find the slot's position
			if (tmp_text[i] == '_')
			{
				//if is really a new slot
				if (justFindSlot)
             	{
             		continue;
             	}	
				justFindSlot = true;
			}
			else //means haven't find the '_', the word is another word!
			{	
				//if we are finding the endPoint, which means justFindSlot == true, we've found the endPoint
				if (justFindSlot)
				{
					
				}
				else
				{
					justFindSlot = false;					
				}

			}

		}
	}

	/// <summary>
	/// load string into the slots in the current scene
	/// </summary>
	/// <param name="words"></param>
	public void LoadString(string words)
	{
		Vector2 posVec2 = slotPositionsQueue.Dequeue();
		string s;


	}
	
}
