using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public LevelParams CustomParams = new LevelParams { cannonPeriod = 1.5f, mazeSize = 10 };
    public Slider CustomMazeSize;
    public Slider CustomFireRate;
    public void Play()
    {
        GameStats.CurrentParams = CustomParams;
        LevelManager.StartGame();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetCustomParams()
    {
        CustomParams = new LevelParams { cannonPeriod = 1 / CustomFireRate.value, mazeSize = Mathf.FloorToInt(CustomMazeSize.value) };
        Debug.Log(CustomParams);
    }
}
