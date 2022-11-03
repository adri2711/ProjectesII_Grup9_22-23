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
    private string levelText = "I love mutant fish!! It makes me hard as a deep sea rock.";
    private string parserText = "I love mutant fish!!";
    private string parserFormat = "<color=green><color=black>";
    private int parserFormatLength;
    private int currLetterIndex = 0;
    private string playerInput;

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
        parserFormatLength = parserFormat.Length;
        parserText = parserFormat + parserText;
        parserTM.text = parserText;
    }

    void Update()
    {
        InputLoop();
        ParserLoop(parserText);
        parserTM.text = parserText;
    }

    private void InputLoop()
    {
        foreach (char c in Input.inputString)
        {
            if (c == levelText[currLetterIndex])
            {
                //Correct Input
                playerInput += c;
                string correctChar = "" + c;
                parserText = parserText.Remove(parserFormatLength + currLetterIndex,1);
                parserText = parserText.Insert(parserFormatLength / 2 + currLetterIndex, correctChar);

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

    private void ParserLoop(string currText)
    {
       
    }

    public bool VerifyKey(KeyCode key)
    {
        string temp = "";
        temp += (parserText[parserFormatLength + currLetterIndex]);
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
        return levelText[pos];
    }
}
