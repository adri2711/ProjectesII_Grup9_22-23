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
    private ParserMultipartText parserText = new ParserMultipartText();
    private int currLetterIndex = 0;

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
    }

    void Update()
    {
        InputLoop();
        parserTM.text = parserText.GetFullFormattedText();
    }

    private void InputLoop()
    {
        foreach (char c in Input.inputString)
        {
            if (c == parserText.GetFullUnformattedText()[currLetterIndex])
            {
                //Correct Input
                GameEvents.instance.EnterCorrectLetter(currLetterIndex);

                currLetterIndex++;
            }
            else
            {
                //Wrong Input
                GameEvents.instance.EnterWrongLetter(currLetterIndex);
            }
        }
    }

    public bool VerifyKey(KeyCode key)
    {
        string temp = "";
        temp += (parserText.GetFullUnformattedText()[currLetterIndex]);
        if (key >= KeyCode.A && key <= KeyCode.Z)
        {
            return key.ToString() == temp.ToUpper();
        }
        else if (key == KeyCode.Space) {
            return temp == " ";
        }
        return false;
    }

    public char GetChar(int pos)
    {
        return parserText.GetFullUnformattedText()[pos];
    }
}
