using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Chess Board");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
