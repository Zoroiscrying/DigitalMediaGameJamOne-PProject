using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordCard : MonoBehaviour
{
	public enum WordType
	{
		Dongci,
		Mingci,
		XingRongci
	}
	
	private TextMeshProUGUI _textMeshPro;
	public int Value = 1;
	public string Word = "Default";
	public WordType Type = WordType.Dongci;
	
	private void Awake()
	{
		_textMeshPro = this.gameObject.GetComponentInChildren<TextMeshProUGUI>();
	}
	// Use this for initialization
	void Start ()
	{
		_textMeshPro.text = this.Word;
	}

	private void OnMouseDown()
	{
		this.OnSelectAndClick();
		//this.DestroySelf();
		WrodCardGenerator.Instance.DestroyCards();
	}

	private void OnMouseOver()
	{
		this.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
	}

	private void OnMouseExit()
	{
		this.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
	}

	public void ResizeSelfAndCollider()
	{
		this.GetComponent<RectTransform>().sizeDelta = new Vector2(_textMeshPro.preferredWidth+30,_textMeshPro.preferredHeight+10);
		this.GetComponent<BoxCollider2D>().size = this.GetComponent<RectTransform>().sizeDelta;
	}
	
	public void OnSelectAndClick()
	{
		FillTextMgr.LoadString(Word,Value);
	}

	public void Initialize(string word, int value, WordType wordType)
	{
		if (this)
		{
			this.Word = word;
			this.Value = value;
			this.Type = wordType;
		}
	}	
	
	public void DestroySelf()
	{
		Destroy(_textMeshPro.gameObject);
		Destroy(this.gameObject);
	}
	
}
