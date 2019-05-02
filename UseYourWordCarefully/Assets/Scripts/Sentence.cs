using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Sentence
{
    public int TotalValue = 0;
    public string Content = "Test of Slot: ___ is becomming more and more intensive!";

    
    //最牛逼的，更新要根据他
    //public List<WordSlot> WordList = new List<WordSlot>();
    public List<int> SlotValues = new List<int>();
    public List<WordSlot> NewWordList = new List<WordSlot>();

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
        bool isFindingSpecialWord = false;
        //分开___和普通词语
        //两种情况：在集word的时候发现_，在集_的时候发现word
        string temp = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            if (sentence[i].Equals('_'))
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
            //找到了一个指令标识符
            else if (sentence[i].Equals('<') || sentence[i].Equals('>'))
            {
                if (sentence[i].Equals('<'))
                {
                    isFindingSpecialWord = true;    
                }
                else
                {
//                    switch (temp.ToLower())
//                    {
//                        case "action":
//                            //action
//                            
//                            break;
//                        case "bg":
//                            //
//                            Debug.Log("ChangeEnBg");
//                            BgManager.Instance.SwitchToNextEnvironmentBg();
//                            break;
//                        case "clickgame0":
//                            //click game for starters
//                            ClickGameGenerator.Instance.GenerateClicks(1,false);
//                            break;
//                        case "clickgame1":
//                            //click game difficulty of 3
//                            
//                            break;
//                        case "clickgame2":
//                            //click game difficulty of 2
//                            
//                            break;
//                        case "clickgame3":
//                            //click game with a difficulty of 3
//                            
//                            break;
//                        case "character":
//                            //change character
//                            
//                            break;
//                        default:
//                            break;
//                    }
                    isFindingSpecialWord = false;
                    isFindingCharacter = true;
                    var wordslot = new WordSlot(true, temp, 0);
                    wordslot.SlotWord = "";
                    wordslot.code = temp;
                    NewWordList.Add(wordslot);
                    temp = "";
                }
            } //在发现_之前，都是一个词，现在找到了一个'_'！
            else//在找词
                //Debug.Log(sentence[i]);
                //找到了一个字母！
            {
                //在找他妈的
                if (isFindingSpecialWord)
                {
                    temp += sentence[i];
                    continue;
                }
                //刚才没有在找字符，所以结束查找，是找到了一个___
                if (!isFindingCharacter)
                {
                    isFindingCharacter = true;
                    NewWordList.Add(new WordSlot(false, temp, 1));
                    // Debug.Log(temp);
                    temp = "";
                    continue;
                }
                //在找字符，我给你加上！
                if (isFindingCharacter)
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
               // Debug.Log("Nope");
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