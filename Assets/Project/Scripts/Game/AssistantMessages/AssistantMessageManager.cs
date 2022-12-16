using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class AssistantMessageManager : MonoBehaviour
{
    public GameObject notificationBox;
    public List<AssistantMessageText> messageTexts;
    public int currentMessage;
    private string textJSON;

    public TextMeshProUGUI textContainer;

    public int testCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Read texts from json
        //string jsonPath = Application.streamingAssetsPath + "/Data/level" + GameManager.instance.GetCurrentLevelNum() + "AssistantMessages.json";
        //textJSON = File.ReadAllText(jsonPath);

        currentMessage = 0;
        //These messages would be added from the json
        messageTexts.Add(new AssistantMessageText("Welcome to the game", "welcome"));
        messageTexts.Add(new AssistantMessageText("You will have to type texts", "type"));
        messageTexts.Add(new AssistantMessageText("Careful, the keys can explode", "key explosion"));
    }

    // Update is called once per frame
    void Update()
    {
        textContainer.text = messageTexts[currentMessage].messageText;
        testCounter++;

        //Example of changing a message.
        if (testCounter == 300)
        {
            SetCurrentMessage("type");
            notificationBox.SetActive(true);
        }
    }

    //This will be called by the event
    public void SetCurrentMessage(string messageId)
    {
        //Asssign as current message the one that has the desired id
        for(int i = 0; i < messageTexts.Count; i++)
        {
            if (messageTexts[i].id == messageId)
                currentMessage = i;
        }
    }

    public void IncreaseCurrentMessage()
    {
        if(currentMessage + 1 < messageTexts.Count)
        currentMessage++;
        notificationBox.SetActive(true);
    }
}
