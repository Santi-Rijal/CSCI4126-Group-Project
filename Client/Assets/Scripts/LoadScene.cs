using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Manages the scene loading process and facilitates the connection to a server.
/// </summary>
public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    static public string IP;

    /// <summary>
    /// Activates the input field, prompting the user to enter the server IP.
    /// </summary>
    public void OpenKeyboard()
    {
        inputField.ActivateInputField();
    }

    /// <summary>
    /// Connects to the server using the entered IP and loads the main scene.
    /// </summary>
    public void LinkToServer()
    {
        IP = inputField.text;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
