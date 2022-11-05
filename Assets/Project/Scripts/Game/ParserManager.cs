using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Reflection;
using System;
using System.Runtime.ConstrainedExecution;

public class ParserManager : MonoBehaviour
{
    public static ParserManager instance { get; private set; }

    [SerializeField] private TextMeshProUGUI parserTM;
    [SerializeField] private TextAsset[] TextJSON;
    private ParserMultipartText parserText = new ParserMultipartText();
    private int currLetterIndex = 0;
    private int currRenderIndex = 0;
    private int parserLength;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        GameEvents.instance.enterCorrectLetter += AddCorrectLetter;
        GameEvents.instance.enterWrongLetter += WrongLetter;
        parserLength = 25;
        parserText.Setup(TextJSON[0]);
    }

    void Update()
    {
        ParserLoop();
        InputLoop();
    }
    private void AddCorrectLetter(int p)
    {
        parserText.AddCorrectLetter();
    }
    private void WrongLetter(int p)
    {
        parserText.WrongLetter();
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
                if (c == parserText.GetFullUnformattedText()[currLetterIndex])
                {
                    //Correct Input
                    GameEvents.instance.EnterCorrectLetter(currLetterIndex);

                    currLetterIndex++;
                    if (currLetterIndex == GetTextSize())
                    {
                        //Finish level
                        Debug.Log("finished!");
                        GameEvents.instance.FinishLevel();
                    }
                }
                else
                {
                    //Wrong Input
                    GameEvents.instance.EnterWrongLetter(currLetterIndex);
                }
            }
        }
    }

    public bool VerifyKey(KeyCode key)
    {
        if (currLetterIndex < GetTextSize())
        {
            string temp = "";
            temp += (parserText.GetFullUnformattedText()[currLetterIndex]);
            if (key >= KeyCode.A && key <= KeyCode.Z)
            {
                return key.ToString() == temp.ToUpper();
            }
            else if (key == KeyCode.Space)
            {
                return temp == " ";
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
