using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using System.Linq;

public class ParserManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    private string parserText;

    private Stack<KeyCode> keyCodeInput = new Stack<KeyCode>();

    void Start()
    {
        parserText = "I love mutant fish";
        tm.text = parserText;
    }

    void Update()
    {
        ParserLoop(parserText);
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            if (e.keyCode != KeyCode.None)
            {
                keyCodeInput.Push(e.keyCode);
            }
        }
    }

    private void ParserLoop(string currText)
    {
        if (keyCodeInput.Any())
        {
            KeyCode currInput = keyCodeInput.Pop();
            Debug.Log(currInput);
            /*foreach (KeyCode code in keyCodeInput)
            {
                Debug.Log(code);
            }*/
        }
    }
}
