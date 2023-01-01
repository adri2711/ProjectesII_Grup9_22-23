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
            Debug.Log("login successful");
            SceneManager.LoadScene("Manager");
        }
        else
        {
            Debug.Log("wrong password");
            wrongPasswordPopUp.SetActive(true);
            password.text = "";
        }
    }
}
