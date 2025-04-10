using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void selectJurassicLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void selectMedievalLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void selectModernLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void selectGreenLibraryLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void selectFutureLevel()
    {
        SceneManager.LoadScene(5);
    }

    public void selectBossLevel()
    {
        SceneManager.LoadScene(6);
    }
}
