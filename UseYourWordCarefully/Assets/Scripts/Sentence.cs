using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Sentence : MonoBehaviour
{
	public int TotalValue;
	public string Content = "Test___";
	public List<WordSlot> WordList = new List<WordSlot>();
	public List<int> SlotValues = new List<int>();
	public List<string> SlotWords = new List<string>();

	private void Start()
	{
		SetContentViaWordList();
	}

	private void Update()
	{
		ResetSlotValues();
		ResetSlotContents();
		SetContentViaWordList();
		if (Input.GetKeyDown(KeyCode.A))
		{
			this.Test();
		}
	}

	public void Test()
	{
		this.FillSlot(1,"Fuck");
	}

	public void SetContentViaWordList()
	{
		string temp = "";
		foreach (var word in WordList)
		{
			temp += word.SlotWord;
		}
		Content = temp;
	}
	
	public string GetContentViaWordList()
	{
		string temp = "";
		foreach (var word in WordList)
		{
			temp += word.SlotWord;
		}
		return temp;
	}
	
	public void AddWord(string word)
	{
		if (word == "___")
		{
			WordList.Add(new WordSlot(false, word,1));
			SlotValues.Add(1);
		}
		else
		{
			WordList.Add(new WordSlot(true,word,0));
		}
	}

	public void ResetSlotValues()
	{
		for (int i = 0; i < WordList.Count; i++)
		{
			//找到没有fill的
			if (WordList[i].IsFilled == false)
			{
				WordList[i].SlotValue = SlotValues[i];
			}
		}
	}

	public void ResetSlotContents()
	{
		WordList.Clear();
		for (int i = 0; i < SlotWords.Count; i++)
		{
			AddWord(SlotWords[i]);
		}
	}

	public void CalculateTotalValue()
	{
		TotalValue = 0;
		foreach (var word in WordList)
		{
			TotalValue += word.FinalValue;
		}
	}

	public void FillSlot(int value, string word)
	{
		for (int i = 0; i < WordList.Count; i++)
		{
			//找到没有fill的
			if (WordList[i].IsFilled == false)
			{
				WordList[i].FillSlot(value,word);
				WordList[i].IsFilled = true;
				break;
			}
		}
	}
}
