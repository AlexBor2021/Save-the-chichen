using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public const string SampleScene = "SampleScene";

    public void OpenPanal(GameObject panal)
    {
        Time.timeScale = 0;
        panal.SetActive(true);
    }

    public void ExitPanal(GameObject panal)
    {
        Time.timeScale = 1;
        panal.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void ExitInMenu()
    {
        SceneManager.LoadScene(SampleScene);
        Time.timeScale = 1;
    }

    public void SetActiveFreezButton(GameObject button)
    {
        button.SetActive(true);
    }
}
