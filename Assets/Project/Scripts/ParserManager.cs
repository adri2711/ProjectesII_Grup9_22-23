using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Reflection;
using System;

public class ParserManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    private string parserText;

    private Queue<KeyCode> keyCodeInput = new Queue<KeyCode>();

    void Start()
    {
    }

    void Update()
    {
        ParserLoop(parserText);
        tm.text = KeyCodesToString(keyCodeInput);
    }

    string KeyCodesToString(Queue<KeyCode> input)
    {
        string ret = "";
        foreach (KeyCode code in input)
        {
            ret += code.ToString();
            Debug.Log(code.ToString());
        }
        return ret;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            //Key pressed
            if (e.keyCode != KeyCode.None)
            {
                keyCodeInput.Enqueue(e.keyCode);
            }
        }
        else if (e.type == EventType.KeyUp)
        {
            //Key released
        }
    }

    private void ParserLoop(string currText)
    {
        if (keyCodeInput.Any())
        {
            KeyCode currInput = keyCodeInput.First();
            Debug.Log(currInput);
            /*foreach (KeyCode code in keyCodeInput)
            {
                Debug.Log(code);
            }*/
        }
    }
}
