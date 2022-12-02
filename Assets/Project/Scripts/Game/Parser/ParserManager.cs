using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using System.IO;
using DG.Tweening;

public class ParserManager : MonoBehaviour
{
    private InputChecker inputChecker;
    private ParserEffects parserEffects;
    private TextMeshProUGUI parserTM;
    private string textJSON;
    private ParserMultipartText parserText = new ParserMultipartText();
    private int currLetterIndex = 0;
    private int currRenderIndex = 0;
    private int parserLength;

    void Start()
    {
        parserTM = transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        inputChecker = GetComponent<InputChecker>();
        parserEffects = GetComponent<ParserEffects>();

        string jsonPath = Application.streamingAssetsPath + "/Data/level" + GameManager.instance.GetCurrentLevelNum() + "Text.json";
        textJSON = File.ReadAllText(jsonPath);
        parserText = new ParserMultipartText();
        parserText.Setup(textJSON);

        parserLength = 27;
    }

    void Update()
    {
        ParserLoop();
        if (GameManager.instance.GetCurrentLevel().isParserActive() && !PauseMenu.gameIsPaused)
        {
            InputLoop();
        }
    }

    ///////////////////  Parser functions  ////////////////////////
    private void ParserLoop()
    {
        if (currLetterIndex <= GetTextSize())
        {
            //Update rendered text when all on-screen text has been completed
            if (currLetterIndex >= currRenderIndex && currLetterIndex < GetTextSize())
            {
                parserText.SetRenderedSegment(currLetterIndex, parserLength);
                currRenderIndex += parserText.GetRenderedSegmentLength();
                parserEffects.ChangeRenderSegment();
                if (IntManager.instance != null && currLetterIndex > 0)
                {
                    IntManager.instance.DestroyAllInterruptions();
                }
            }

            parserTM.text = parserText.GetRenderedFormattedText();
        }
    }  
    private void InputLoop()
    {
        foreach (char c in Input.inputString)
        {
            if (currLetterIndex < GetTextSize())
            {
                GameEvents.instance.EnterLetter();
                if (inputChecker.Do(c, parserText.GetFullUnformattedText(), currLetterIndex))
                {
                    //Correct Input
                    parserText.AddCorrectLetter();
                    inputChecker.EnterCorrectLetter();
                    GameEvents.instance.EnterCorrectLetter(currLetterIndex);
                    parserEffects.CorrectLetterEffect(currLetterIndex);

                    currLetterIndex++;

                    if (currLetterIndex == GetTextSize())
                    {
                        //Finish level
                        GameEvents.instance.FinishLevel();
                    }
                }
                else
                {
                    //Wrong Input
                    parserText.WrongLetter();
                    GameEvents.instance.EnterWrongLetter(currLetterIndex);
                }
            }
        }
    }

    ///////////////////  Hooks  ////////////////////////
    public bool VerifyKey(char key)
    {
        return inputChecker.Do(key, parserText.GetFullUnformattedText(), currLetterIndex);
    }
    public void AddFreeKeys(int amount, bool overwrite = false)
    {
        inputChecker.AddFreeKeys(amount, overwrite);
    }

    ///////////////////  Getters  ////////////////////////
    public char GetChar(int pos)
    {
        return parserText.GetFullUnformattedText()[pos];
    }
    public int GetTextSize()
    {
        return parserText.GetFullUnformattedText().Length;
    }
}
