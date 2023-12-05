using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    static public string IP;

    public void OpenKeyboard()
    {
        inputField.ActivateInputField();
    }

    public void LinkToServer()
    {
        IP = inputField.text;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
