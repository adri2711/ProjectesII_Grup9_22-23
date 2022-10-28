using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] List<Key> keys;

    private Queue<KeyCode> keyCodeInput = new Queue<KeyCode>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    string KeyCodesToString(Queue<KeyCode> input)
    {
        string ret = "";
        foreach (KeyCode code in input)
        {
            ret += code.ToString();
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
                //keyCodeInput.Enqueue(e.keyCode);
                
            }
            keys.First().PushCorrectLetter();
        }
        else if (e.type == EventType.KeyUp)
        {
            //Key released
            keys.First().ResetLetter();
        }
        
    }

    //private void ParserLoop(string currText)
    //{
    //    if (keyCodeInput.Any())
    //    {
    //        KeyCode currInput = keyCodeInput.First();
    //        Debug.Log(currInput);
    //    }
    //}
}
