using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private string scene;
    public void selectJurassicLevel()
    {
        scene = "StoneAge";
        //SceneManager.LoadScene(scene);
    }
    public void selectMedievalLevel()
    {
        scene = "MedievalAge";
        //SceneManager.LoadScene(2);
    }

    public void selectModernLevel()
    {
        scene = "ModernAge";
        //SceneManager.LoadScene(3);
    }

    public void selectGreenLibraryLevel()
    {
        scene = "GreenLibrary";
        //SceneManager.LoadScene(4);
    }

    public void selectFutureLevel()
    {
        scene = "FutureAge";
        //SceneManager.LoadScene(5);
    }

    public void selectBossLevel()
    {
        scene = "BossLevel";
        //SceneManager.LoadScene(6);
    }
}
