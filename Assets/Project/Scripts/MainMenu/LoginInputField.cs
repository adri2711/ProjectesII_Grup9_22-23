using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginInputField : MonoBehaviour
{
    private TMP_InputField password;
    [SerializeField] private string correctPasswordText;
    [SerializeField] private GameObject wrongPasswordPopUp;
    [SerializeField] private AudioSource wrongSound;
    
    void Start()
    {
        password = GetComponent<TMP_InputField>();
        password.contentType = TMP_InputField.ContentType.Password;
    }

    public void TryToLogin()
    {
        if (password.text == correctPasswordText)
        {
            GameManager.instance.SetGameState("Desktop");
        }
        else if (password.text != "")
        {
            wrongSound.Play();
            wrongPasswordPopUp.SetActive(true);
            password.text = "";
        }
    }
}
