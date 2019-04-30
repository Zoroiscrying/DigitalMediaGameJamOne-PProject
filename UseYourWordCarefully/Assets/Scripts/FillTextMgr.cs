using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

public class FillTextMgr : MonoBehaviour
{
    #region SingleTon

    public static FillTextMgr Instance
    {
        get { return _instance; }
    }

    private static FillTextMgr _instance;

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

    //public
    public TextMeshProUGUI Debug_Text;
    public TextMeshProUGUI TMP_Current;
    public List<Sentence> Sentences = new List<Sentence>();
    public GameObject PressEImage;
    public bool IsSentenceFinished = false;
    public RevealSentence RevealSentence;
    
    //private
    private static Sentence _activeSentence;
    private int _activeSentenceIndex = 0;

    // Use this for initialization
    void Start()
    {
        PressEImage.SetActive(false);
        ResetSentences();
        //_activeSentence.FillSlot(1,"逸飞");
    }

    private void ResetSentences()
    {
        _activeSentenceIndex = 0;
        if (Sentences.Count > 0)
        {
            _activeSentence = Sentences[_activeSentenceIndex];
        }
    }

    public void UpdateTextViaCurrentSentence()
    {
        TMP_Current.text = _activeSentence.Content;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextViaCurrentSentence();

        if (_activeSentence.IsAllFilled && IsSentenceFinished)
        {
            //Debug.Log("Set True Active");
            PressEImage.SetActive(true);
            //显示
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_activeSentenceIndex == Sentences.Count - 1)
                {
                    Debug.Log("END OF THE DIALOG");
                    CharacterManager.Instance.SwitchToNextCharacter();
                    return;
                }
                this.SwitchToNextSentence();
                RevealSentence.AutoReveal();
            }
        }
        else
        {
            PressEImage.SetActive(false);
        }
        
        UpdateDebugText();
    }

    private void UpdateDebugText()
    {
        if (Debug_Text)
        {
            Debug_Text.text = _activeSentence.TotalValue.ToString();
        }
    }

    /// <summary>
    /// Put string into the slots in the current scene
    /// </summary>
    /// <param name="word"></param>
    public static void LoadString(string word, int value)
    {
        _activeSentence.FillSlot(value, word);
    }

    public void SwitchToNextSentence()
    {
        if (_activeSentenceIndex < Sentences.Count - 1)
        {
            _activeSentence = Sentences[++_activeSentenceIndex];
            if (!_activeSentence.IsAllFilled)
            {
                CharacterManager.Instance.GenerateWordCard();
            }
        }
    }

    /// <summary>
    /// 切换对话角色时，要进行对应对话语句的更换
    /// </summary>
    /// <param name="sentences"></param>
    public void SetCurrentSentences(List<string> sentences)
    {
        Debug.Log("My Sentences Has Been Changed.");
        this.Sentences.Clear();
        //List<Sentence> newSentences = new List<Sentence>();
        for (int i = 0; i < sentences.Count; i++)
        {
         //   Debug.Log("Add Sentence: " + sentences[i]);
           this.Sentences.Add(new Sentence(sentences[i]));
        }
        ResetSentences();
        // Sentences = sentences;
    }
}