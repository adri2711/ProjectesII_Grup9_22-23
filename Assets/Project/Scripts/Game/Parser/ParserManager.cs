using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using System.IO;

public class ParserManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI parserTM;
    private string textJSON;
    private ParserMultipartText parserText = new ParserMultipartText();
    private int currLetterIndex = 0;
    private int currRenderIndex = 0;
    private int parserLength;

    void Start()
    {
        parserTM = transform.parent.GetComponentInChildren<TextMeshProUGUI>();

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

    private void ParserLoop()
    {
        if (currLetterIndex <= GetTextSize())
        {
            //Update rendered text when all on-screen text has been completed
            if (currLetterIndex >= currRenderIndex && currLetterIndex < GetTextSize())
            {
                parserText.SetRenderedSegment(currLetterIndex, parserLength);
                currRenderIndex += parserText.GetRenderedSegmentLength();
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
                if (c == parserText.GetFullUnformattedText()[currLetterIndex])
                {
                    //Correct Input
                    parserText.AddCorrectLetter();
                    GameEvents.instance.EnterCorrectLetter(currLetterIndex);

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

    public bool VerifyKey(char key)
    {
        if (currLetterIndex < GetTextSize())
        {
            char temp = (parserText.GetFullUnformattedText()[currLetterIndex]);
            if (key >= 'A' && key <= 'Z' || key >= 'a' && key <= 'z')
            {
                return key == temp;
            }
            else if (key == ' ')
            {
                return temp == ' ';
            }
        }
        return false;
    }

    public char GetChar(int pos)
    {
        return parserText.GetFullUnformattedText()[pos];
    }
    public int GetTextSize()
    {
        return parserText.GetFullUnformattedText().Length;
    }
}
