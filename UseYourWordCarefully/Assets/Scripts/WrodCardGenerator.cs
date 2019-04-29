using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrodCardGenerator : MonoBehaviour
{

	public Transform CardParent;
	public GameObject WordCardPrefab;
	public List<GameObject> WordCards = new List<GameObject>();
	
	#region SingleTon

	public static WrodCardGenerator Instance
	{
		get { return _instance; }
	}

	private static WrodCardGenerator _instance;

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

	struct WordCardBiInfo
	{
		private string word;
		private int value;

		public WordCardBiInfo(string word, int value)
		{
			this.word = word;
			this.value = value;
		}
	}
	//private 
	
	/// <summary>
	/// 根据选择生成一系列玩意
	/// </summary>
	/// <param name="choice"></param>
	public void GenerateWords(Character.Choice choice)
	{
		//以','分隔开
		string[] words = choice.Word.Split(',');
		string[] values = choice.Value.Split(',');
		List<int> intValues = new List<int>();
		try
		{
			foreach (var value in values)
			{
				intValues.Add(int.Parse(value));
			}
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
		Generate(words,intValues);
	}

	private void Generate(string[] words, List<int> intValues)
	{
		List<Vector2> poses = new List<Vector2>();
		poses = CalculateEachPos(words.Length, 600);
		GenerateViaPoses(poses,words,intValues);
	}

	private List<Vector2> CalculateEachPos(int num, int length)
	{
		List<Vector2> poses = new List<Vector2>();
		int eachDis = length / num;
		int initialPosX = -length / 2;
		for (int i = 0; i < num; i++)
		{
			poses.Add(new Vector2(initialPosX + eachDis/2 + i*eachDis,-250));
		}

		return poses;
	}

	private void GenerateViaPoses(List<Vector2> poses, string[]words, List<int> intValues)
	{
		for (int i = 0; i < poses.Count; i++)
		{
			var obj = Instantiate(WordCardPrefab, new Vector3(poses[i].x, poses[i].y, 0), Quaternion.identity);
			//Debug.Log(obj.transform.position);
			obj.transform.SetParent(CardParent,false);		
			var wordcard = obj.GetComponentInChildren<WordCard>();
			wordcard.Initialize(words[i],intValues[i],WordCard.WordType.Dongci);
			WordCards.Add(obj);
		}
	}

	public void DestroyCards()
	{
		foreach (var VARIABLE in WordCards)
		{
			Destroy(VARIABLE);
		}
		WordCards.Clear();
	}

}
