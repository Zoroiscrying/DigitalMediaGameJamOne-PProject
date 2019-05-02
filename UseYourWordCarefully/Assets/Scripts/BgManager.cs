using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgManager : MonoBehaviour
{
	#region SingleTon

	public static BgManager Instance
	{
		get { return _instance; }
	}

	private static BgManager _instance;

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

	public Image EnvironmentBg;
	public Image GameBg;
	public Image CharacterBg;
	public List<Sprite> EnvrmtImageList;
	public List<Sprite> GameBgImageLsit;
	public List<Sprite> CharacterImageList;

	private int EnvrmIndex = 0;
	private int GameBgIndex = 0;
	private int CharacterIndex = 0;
	
	public void SwitchToNextEnvironmentBg()
	{
		if (EnvrmIndex < EnvrmtImageList.Count)
		{
			EnvironmentBg.sprite = EnvrmtImageList[EnvrmIndex++];			
		}
	}

	public void SwitchToEnvironmentBgIndex(int index)
	{
		if (index >= 0 && index < EnvrmtImageList.Count)
		{
			EnvironmentBg.sprite = EnvrmtImageList[index];	
		}
	}
	
	public void SwitchToNextGameBg()
	{
		if (GameBgIndex < GameBgImageLsit.Count)
		{
			GameBg.sprite = GameBgImageLsit[GameBgIndex++];			
		}
	}

	public void SwitchToGameBgIndex(int index)
	{
		if (index >= 0 && index < GameBgImageLsit.Count)
		{
			GameBg.sprite = GameBgImageLsit[index];
		}
	}
	
	public void SwitchToNextCharacterBg()
	{
		if (CharacterIndex < CharacterImageList.Count)
		{
			CharacterBg.sprite = CharacterImageList[CharacterIndex++];			
		}
	}

	public void SwitchToCharacterBgIndex(int index)
	{
		if (index >= 0 && index < CharacterImageList.Count)
		{
			CharacterBg.sprite = CharacterImageList[index];
		}
	}

}
