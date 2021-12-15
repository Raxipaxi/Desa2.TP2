using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject main;
    [SerializeField] private string mainRoom1;


    public void ToGame()
    {
        SceneManager.LoadScene(mainRoom1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }



    public void ToMain()
    {
        main.SetActive(true);
    }


}

