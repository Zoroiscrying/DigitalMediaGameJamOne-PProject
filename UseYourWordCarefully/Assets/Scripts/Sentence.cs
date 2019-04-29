using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Sentence
{
    public int TotalValue = 0;
    public string Content = "Test of Slot: ___ is becomming more and more intensive!";

    //最牛逼的，更新要根据他
    //public List<WordSlot> WordList = new List<WordSlot>();
    public List<int> SlotValues = new List<int>();
    private List<WordSlot> NewWordList = new List<WordSlot>();

    public bool IsAllFilled
    {
        get
        {
            for (int i = 0; i < NewWordList.Count; i++)
            {
                //找到没有fill的
                if (NewWordList[i].IsFilled == false)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public Sentence(string content)
    {
        this.Content = content;
        InitializeSentenceToSlots();
    }

    public void ParseStringToWords(string sentence)
    {
        bool isFindingCharacter = true;
        //分开___和普通词语
        //两种情况：在集word的时候发现_，在集_的时候发现word
        string temp = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            //Debug.Log(sentence[i]);
            //找到了一个字母！
            if (!sentence[i].Equals('_'))
            {
                if (!isFindingCharacter)
                {
                    isFindingCharacter = true;
                    NewWordList.Add(new WordSlot(false, temp, 1));
                    // Debug.Log(temp);
                    temp = "";
                }

                if (isFindingCharacter)
                {
                    temp += sentence[i];
                }
            } //在发现_之前，都是一个词，现在找到了一个'_'！
            else if (sentence[i].Equals('_'))
            {
                //输出现在的temp
                //表示现在不再找character了，在找下划线
                if (isFindingCharacter)
                {
                    isFindingCharacter = false;
                    NewWordList.Add(new WordSlot(true, temp, 0));
                    // Debug.Log(temp);
                    temp = "";
                }

                if (!isFindingCharacter)
                {
                    temp += sentence[i];
                }
            }
        }

        //退出时仍然在找字母
        if (isFindingCharacter)
        {
            NewWordList.Add(new WordSlot(true, temp, 1));
        }
        else
        {
            NewWordList.Add(new WordSlot(false, temp, 0));
        }
    }

//    private void Start()
    //    {
    //        InitializeSentenceToSlots();
    //    }

    private void InitializeSentenceToSlots()
    {
        //SlotValues = new List<int>();
        NewWordList = new List<WordSlot>();
        ParseStringToWords(Content);
        ResetSlotValues(NewWordList);
        SetContentViaWordList(NewWordList);
    }

    /// <summary>
    /// 根据现在的WordList设置输出到TextMeshPro的Content内容
    /// </summary>
    /// <param name="list"></param>
    public void SetContentViaWordList(List<WordSlot> list)
    {
        string temp = "";
        foreach (var word in list)
        {
            temp += word.SlotWord;
        }

        Content = temp;
    }

    public string GetContentViaWordList()
    {
        string temp = "";
        foreach (var word in NewWordList)
        {
            temp += word.SlotWord;
        }

        return temp;
    }

//    public void AddWord(string word, List<WordSlot> list)
//    {
//        if (word == "___")
//        {
//            list.Add(new WordSlot(false, word, 1));
//            SlotValues.Add(1);
//        }
//        else
//        {
//            list.Add(new WordSlot(true, word, 0));
//        }
//    }

    /// <summary>
    /// 设置句子中的SlotValue
    /// </summary>
    /// <param name="list"></param>
    public void ResetSlotValues(List<WordSlot> list)
    {
        int index = 0;
        for (int i = 0; i < list.Count; i++)
        {
            //找到没有fill的
            if (list[i].IsFilled)
            {
                Debug.Log("Nope");
                continue;
            }

            //list[i].SlotValue = SlotValues[index++];
            Debug.Log("Set" + list[i] + "'s value To 1");
            list[i].SlotValue = 1;
        }
    }

    /// <summary>
    /// 计算WordList的总点数，更新到TotalValue中
    /// </summary>
    /// <param name="list"></param>
    public void CalculateTotalValue(List<WordSlot> list)
    {
        TotalValue = 0;
        foreach (var word in list)
        {
            TotalValue += word.FinalValue;
        }
    }

    /// <summary>
    /// 填入一个价值为value的word在现在的wordlist中，更新总点数和Sentence内容
    /// </summary>
    /// <param name="value"></param>
    /// <param name="word"></param>
    public void FillSlot(int value, string word)
    {
        for (int i = 0; i < NewWordList.Count; i++)
        {
            //找到没有fill的
            if (NewWordList[i].IsFilled == false)
            {
                NewWordList[i].FillSlot(value, word);
                // Debug.Log("ADD POINT:" + NewWordList[i].FinalValue);
                NewWordList[i].IsFilled = true;
                CalculateTotalValue(NewWordList);
                SetContentViaWordList(NewWordList);
                break;
            }
        }
    }
}