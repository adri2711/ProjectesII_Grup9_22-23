using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Reflection;
using System;

public class ParserManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerTM;
    [SerializeField] private TextMeshProUGUI parserTM;
    private string parserText = "I love mutant fish!!";
    int currLetterIndex = 0;
    private string playerInput;

    void Start()
    {
        parserTM.text = parserText;
    }

    void Update()
    {
        InputLoop();
        ParserLoop(parserText);
        playerTM.text = playerInput;
    }

    private void InputLoop()
    {
        foreach (char c in Input.inputString)
        {
            if (c == parserText[currLetterIndex])
            {
                playerInput += c;
                currLetterIndex++;
            }
            else
            {
                Debug.Log("Wrong!");
            }
        }
    }

    private void ParserLoop(string currText)
    {
       
    }
}
