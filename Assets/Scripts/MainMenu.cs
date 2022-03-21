using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private LevelParams DefaultParams = new LevelParams { cannonPeriod = 2f, mazeSize = 10 };
    public Slider CustomMazeSize;
    public Slider CustomFireRate;
    
    public void Play()
    {
        LevelManager.StartGame();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void SetCustomParams()
    {
        GameStats.CurrentParams = new LevelParams { cannonPeriod = 1 / CustomFireRate.value, mazeSize = Mathf.FloorToInt(CustomMazeSize.value) };
        GameStats.UsingCustomParams = true;
        GameStats.ResetHighScore();
    }

    public void SliderReset()
    {
        CustomMazeSize.value = GameStats.CurrentParams.mazeSize;
        CustomFireRate.value = 1 / GameStats.CurrentParams.cannonPeriod;
    }

    public void SlideDefaultSettings()
    {
        CustomMazeSize.value = DefaultParams.mazeSize;
        CustomFireRate.value = 1 / DefaultParams.cannonPeriod;
    }
    private void Start()
    {
        if (GameStats.UsingCustomParams)
        {
            SliderReset();
        } else
        {
            SlideDefaultSettings();
            SetCustomParams();
        }
        

    }
}
