using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[Serializable]
	public struct Choice
	{
		public string Word;
		public string Value;

		public Choice(string word, string value)
		{
			this.Word = word;
			this.Value = value;
		}
	}

	public List<string> Dialog = new List<string>();
	public List<string> StringSecrets = new List<string>();
	public List<Choice> Choices = new List<Choice>();
	
	public string TotalSecrets
	{
		get { return _totalSecrets; }
	}
	
	private string _totalSecrets = "";
	private int _stringIndex = 0;
	private int _choiceIndex = 0;
	public void Initialize()
	{
		Debug.Log(this.gameObject.name + " Changed the FillTextMgr's Dialog.");
		FillTextMgr.Instance.SetCurrentSentences(this.Dialog);
	}

	public Choice GetChoice()
	{
		return Choices[_choiceIndex++];
	}
	
	public void DigSecret()
	{
		if (_stringIndex < StringSecrets.Count)
		{
			_totalSecrets += StringSecrets[_stringIndex++] + "\n";
		}
		else
		{
			//大秘密要藏深————一点
			_totalSecrets += "......";
		}
	}
	
	
	
}
