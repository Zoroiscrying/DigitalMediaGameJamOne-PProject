using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region SingleTon

    public static CharacterManager Instance
    {
        get { return _instance; }
    }

    private static CharacterManager _instance;
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
    
    public List<Character> Characters = new List<Character>();
    private Character _activeCharacter;
    private int _activeCIndex = 0;

    public Character ActiveCharacter
    {
        get { return _activeCharacter; }
    }

    public void SwitchToNextCharacter()
    {
        if (_activeCIndex < Characters.Count)
        {
            //Debug.Log("Next Character!");
            //
            _activeCharacter = Characters[_activeCIndex++];
            Debug.Log("Switch To Character: " + _activeCharacter.gameObject.name);
            _activeCharacter.Initialize();
        }
    }

    public void GenerateWordCard()
    {
        WrodCardGenerator.Instance.GenerateWords(_activeCharacter.GetChoice());
    }
    
    //每个角色对应一段对话，这段对话结束后进行角色的切换和sentence的更新，对应在ChatBobbleController中。
    private void Start()
    {
        SwitchToNextCharacter();
    }
}
