using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ParserManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tm;
    private string parserText;

    private Queue<KeyCode> keyCodeInput = new Queue<KeyCode>();

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
                keyCodeInput.Enqueue(e.keyCode);
            }
        }
    }

    private void ParserLoop(string currText)
    {
        if (keyCodeInput.Any())
        {
            KeyCode currInput = keyCodeInput.Dequeue();
            Debug.Log(currInput);
            /*foreach (KeyCode code in keyCodeInput)
            {
                Debug.Log(code);
            }*/
        }
    }
}
