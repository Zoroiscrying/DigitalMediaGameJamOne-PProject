using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using Object = UnityEngine.Object;

public class RevealSentence : MonoBehaviour {
        private TMP_Text m_TextComponent;
        private bool hasTextChanged;
        public int visibleCount = 0;
        public void ResetViewableCount()
        {
            m_TextComponent.maxVisibleCharacters = 0;
        }
        
        void Awake()
        {
            m_TextComponent = gameObject.GetComponent<TMP_Text>();
        }

        void Start()
        {
            StartCoroutine(RevealCharacters(m_TextComponent));
            //StartCoroutine(RevealWords(m_TextComponent));
        }

        private void Update()
        {

        }

        void OnEnable()
        {
            // Subscribe to event fired when text object has been regenerated.
            TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
        }

        void OnDisable()
        {
            TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
        }


        // Event received when the text object has changed.
        void ON_TEXT_CHANGED(Object obj)
        {
            hasTextChanged = true;
        }

        public void AutoReveal()
        {
            StartCoroutine(RevealCharacters(m_TextComponent));
        }
        
        /// <summary>
        /// Method revealing the text one character at a time.
        /// </summary>
        /// <returns></returns>
        IEnumerator RevealCharacters(TMP_Text textComponent)
        {
            visibleCount = 0;
            textComponent.ForceMeshUpdate();

            TMP_TextInfo textInfo = textComponent.textInfo;

            int totalVisibleCharacters = textInfo.characterCount; // Get # of Visible Character in text object
            while (true)
            {
                if (hasTextChanged)
                {
                    totalVisibleCharacters = textInfo.characterCount; // Update visible character count.
                    hasTextChanged = false; 
                }

                if (visibleCount > totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(0.5f);
                   // Debug.Log("FINISHED");
                    ////Show Press E button
                    FillTextMgr.Instance.IsSentenceFinished = true;
                    StopCoroutine(RevealCharacters(m_TextComponent));
                    //visibleCount = 0;
                }

                textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?
                if (textComponent.text.Length >= visibleCount)
                {
                    visibleCount += 1;
                    //Debug.Log(visibleCount);
                    FillTextMgr.Instance.IsSentenceFinished = false;                    
                }
                yield return new WaitForSeconds(0.05f);
            }
        }


        /// <summary>
        /// Method revealing the text one word at a time.
        /// </summary>
        /// <returns></returns>
        IEnumerator RevealWords(TMP_Text textComponent)
        {
            textComponent.ForceMeshUpdate();

            int totalWordCount = textComponent.textInfo.wordCount;
            int totalVisibleCharacters = textComponent.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;
            int currentWord = 0;
            int visibleCount = 0;

            while (true)
            {
                currentWord = counter % (totalWordCount + 1);

                // Get last character index for the current word.
                if (currentWord == 0) // Display no words.
                    visibleCount = 0;
                else if (currentWord < totalWordCount) // Display all other words with the exception of the last one.
                    visibleCount = textComponent.textInfo.wordInfo[currentWord - 1].lastCharacterIndex + 1;
                else if (currentWord == totalWordCount) // Display last word and all remaining characters.
                    visibleCount = totalVisibleCharacters;

                textComponent.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, wait 1.0 second and start over.
                if (visibleCount >= totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                }

                counter += 1;

                yield return new WaitForSeconds(0.1f);
            }
        }
}
