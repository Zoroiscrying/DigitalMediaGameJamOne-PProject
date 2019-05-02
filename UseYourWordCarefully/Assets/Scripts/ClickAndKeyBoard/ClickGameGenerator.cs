using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEditor;
using UnityEngine;

public class ClickGameGenerator : MonoBehaviour
{
	#region SingleTon

	public static ClickGameGenerator Instance
	{
		get { return _instance; }
	}

	private static ClickGameGenerator _instance;

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
	
	public Transform PrefabParent;
	public GameObject ClickAndKeyPrefab;
	public RectTransform BgRectTransform;
	public GameObject GamePlayCanvas;
	private List<ClickAndKeyBoard> ClickAndKeyBoardList = new List<ClickAndKeyBoard>();
	private Vector2 _leftDownPoint = new Vector2();
	private Vector2 _rightUpPoint = new Vector2();
	private bool isFinishedClicking = false;
	private bool _isBegunJustNow = false;
	private int wavesLeft = 0;
	
	private int _notNullClicksInList
	{
		get
		{
			int i = 0;
			foreach (var VARIABLE in ClickAndKeyBoardList)
			{
				if (VARIABLE)
				{
					i++;
				}
			}

			return i;
		}
	}
	// Use this for initialization
	void Start () {
		var position = BgRectTransform.position;
		var sizeDelta = BgRectTransform.sizeDelta;
		//Debug.Log(sizeDelta);
		_leftDownPoint = new Vector2(position.x - sizeDelta.x/2,position.y - sizeDelta.y/2);
		_rightUpPoint = new Vector2(position.x + sizeDelta.x/2,position.y + sizeDelta.y/2);
	}
	
	
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown(KeyCode.G))
		{
//			GamePlayCanvas.SetActive(true);
//			GenerateClicks(12,true);
//			Debug.Log(ClickAndKeyBoardList.Count);
//			ClickAndKeyBoardList.Add(null);
//			Debug.Log(ClickAndKeyBoardList.Count);	
		}

//		if (wavesLeft > 0)
//		{
//			GenerateClicks(Random.Range(4,6),Random.Range(0,1)==0);
//		}

		if (_notNullClicksInList > 0)
		{
			_isBegunJustNow = true;
		}

		if (_isBegunJustNow) 
		{
			if (_notNullClicksInList == 0)
			{
				isFinishedClicking = true;
			}
		}
	}

	public void GenerateWithDifficulty(int difficulty, int waves)
	{
		StartCoroutine(GenerateClickWaves(waves,difficulty));
	}

	IEnumerator GenerateClickWaves(int waveNum, int difficulty)
	{
		GamePlayCanvas.SetActive(true);
		isFinishedClicking = false;
		for (int i = 0; i < waveNum;)
		{
			//产生几波点击事件。。。
			//如何检测这波已经完成？--Update!
			GenerateClicks(Random.Range(4+difficulty,6+difficulty),Random.Range(0,1)==0);
			yield return new WaitUntil(() => isFinishedClicking );
			if (isFinishedClicking)
			{
				i++;
				Debug.Log(i);
			}
		}
		GamePlayCanvas.SetActive(false);
	}

	/// <summary>
	/// 随机生成一系列点，根据isVertical判断整体走向是否竖直
	/// </summary>
	/// <param name="num"></param>
	public void GenerateClicks(int num, bool isVertical, int hintTimes = 1)
	{
		isFinishedClicking = false;
		if (isVertical)
		{
			var averageDis = Mathf.Abs(_rightUpPoint.y - _leftDownPoint.y - 100.0f)/num;
			var pos = new Vector2();
			Debug.Log(averageDis);
			for (int i = 0; i < num; i++)
			{
				pos.y = Random.Range(_leftDownPoint.y + i*averageDis, _leftDownPoint.y + (i+1)*averageDis);
				pos.x = Random.Range(_leftDownPoint.x, _rightUpPoint.x);
				GenerateOneClick(pos,new Vector2(1,1));
			}
		}
		else
		{
			var averageDis = Mathf.Abs(_rightUpPoint.x - _leftDownPoint.x - 50.0f)/(num+1);
			var pos = new Vector2();
			for (int i = 0; i < num; i++)
			{
				pos.x = Random.Range(_leftDownPoint.x+i*averageDis,_leftDownPoint.x + (i+1)*averageDis);
				pos.y = Random.Range(_leftDownPoint.y, _rightUpPoint.y);
				GenerateOneClick(pos,new Vector2(1,1));
			}
		}
		StartCoroutine(WaveEffect(0.5f, 2));
	}

	public void GenerateOneClick(Vector2 pos, Vector2 scale)
	{
		var obj = Instantiate(ClickAndKeyPrefab, new Vector3(pos.x, pos.y, ClickAndKeyPrefab.transform.position.z), Quaternion.identity);
		obj.transform.SetParent(PrefabParent,false);
		obj.transform.localScale = scale;
		var click = obj.GetComponentInChildren<ClickAndKeyBoard>();
		click.Initialize();
		ClickAndKeyBoardList.Add(click);
	}

	/// <summary>
	/// 按顺序显示效果
	/// </summary>
	IEnumerator WaveEffect(float everyClickTime, int howManyTimes = 999)
	{
		int times = 0;
		yield return new WaitForSeconds(1.0f);
		for (int i = 0; i < howManyTimes; i++)
		{
			if (!isFinishedClicking && times < howManyTimes)
			{
				foreach (var VARIABLE in ClickAndKeyBoardList)
				{
					if (VARIABLE) 
					{
						VARIABLE.HeartBeatEffect();
						yield return new WaitForSeconds(everyClickTime);						
					}
				}
				times++;
			}
			else
			{
				yield break;				
			}			
		}
	}

	/// <summary>
	/// 每隔time秒激活一次SequenceWave
	/// </summary>
	/// <param name="time"></param>
	/// <returns></returns>
	IEnumerator GenerateWaveEveryMuchTime(float time)
	{
		if (!isFinishedClicking)
		{
			StartCoroutine(WaveEffect(0.2f));
			yield return new WaitForSeconds(time);			
		}
		else
		{
			yield break;
		}

	}

	public void OnClickWaveComplete()
	{
		isFinishedClicking = true;
	}
	
	
}
