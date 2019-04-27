using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FillTextMgr : MonoBehaviour
{
//	public static FillTextMgr Instance
//	{
//		get{
//			if (_instance == null)
//			{
//				_instance = new FillTextMgr();
//				return _instance;
//			}
//			return _instance;
//		}
//	}
//	private static FillTextMgr _instance;
	

	//public
	public TextMeshProUGUI TMP_Current;
	public List<Sentence> Sentences = new List<Sentence>();
	
	
	//private
	private static Sentence _activeSentence;
	private int _activeSentenceIndex = 0;
	private void Awake()
	{
		
	}

	// Use this for initialization
	void Start ()
	{
		if (Sentences.Count>0) 
		{
			_activeSentence = Sentences[_activeSentenceIndex];
		}
	}

	public void UpdateTextViaCurrentSentence()
	{
		TMP_Current.text = _activeSentence.Content;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateTextViaCurrentSentence();

		if (Input.GetKeyDown(KeyCode.F))
		{
			_activeSentence.FillSlot(1,"FUCK");
		}
		
		if (_activeSentence.IsAllFilled)
		{
			//显示
			if (Input.GetKeyDown(KeyCode.E))
			{
				this.SwitchToNextSentence();
			}
		}
		
	}

	/// <summary>
	/// Put string into the slots in the current scene
	/// </summary>
	/// <param name="word"></param>
	public static void LoadString(string word, int value)
	{
		_activeSentence.FillSlot(value,word);
	}

	public void SwitchToNextSentence()
	{
		if (_activeSentenceIndex < Sentences.Count-1)
		{
			_activeSentence = Sentences[++_activeSentenceIndex];
		}
	}
	
}
