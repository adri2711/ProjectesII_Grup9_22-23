using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginInputField : MonoBehaviour
{
    public TMP_InputField password;
    public string correctPasswordText;

    public GameObject wrongPasswordPopUp;
    public AudioSource wrongSound;
    
    void Start()
    {
        password.contentType = TMP_InputField.ContentType.Password;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToLogin()
    {
        if(password.text == correctPasswordText)
        {
            GameManager.instance.SetGameState("Desktop");
        }
        else
        {
            wrongSound.Play();
            wrongPasswordPopUp.SetActive(true);
            password.text = "";
        }
    }
}
