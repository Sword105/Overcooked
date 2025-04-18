using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public PlayerConfigManager setupManager;
    public GameObject firstSelection;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelection);
    }

    public void selectJurassicLevel()
    {
        setupManager.sceneToLoad = "StoneAge";
        //SceneManager.LoadScene(scene);
    }
    public void selectMedievalLevel()
    {
        setupManager.sceneToLoad = "MedievalAge";
        //SceneManager.LoadScene(2);
    }

    public void selectModernLevel()
    {
        setupManager.sceneToLoad = "ModernAge";
        //SceneManager.LoadScene(3);
    }

    public void selectGreenLibraryLevel()
    {
        setupManager.sceneToLoad = "GreenLibrary";
        //SceneManager.LoadScene(4);
    }

    public void selectFutureLevel()
    {
        setupManager.sceneToLoad = "FutureAge";
        //SceneManager.LoadScene(5);
    }

    public void selectBossLevel()
    {
        setupManager.sceneToLoad = "BossLevel";
        //SceneManager.LoadScene(6);
    }
}
