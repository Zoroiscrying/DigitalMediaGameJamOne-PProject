using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSlot
{

	public bool IsFilled = false;
	public string SlotWord = "___";
	public int SlotValue = 1;
	public int FinalValue = 0;

	public string code = "";

	public WordSlot(bool isFilled, string word, int value)
	{
		this.IsFilled = isFilled;
		this.SlotWord = word;
		this.SlotValue = value;
	}
	public void FillSlot(int value, string word)
	{
		SlotWord = word;
		FinalValue = value * SlotValue;
	}

}
