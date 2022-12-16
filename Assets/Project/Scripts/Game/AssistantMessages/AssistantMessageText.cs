using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AssistantMessageText : MonoBehaviour
{
    public string messageText;      //The actual text of the message
    public string id;               //Identifies what the message is about

    public AssistantMessageText(string text, string id)
    {
        messageText = text;
        this.id = id;
    }
}
