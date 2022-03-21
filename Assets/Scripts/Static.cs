using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utilities
{
    /// <summary>
    /// Extension method to check if a layer is in a layermask
    /// </summary>
    /// <param name="mask"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    /// by Mikael-H from https://answers.unity.com/questions/50279/check-if-layer-is-in-layermask.html
    public static bool Contains(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}
// idea from https://gamedev.stackexchange.com/questions/110958/what-is-the-proper-way-to-handle-data-between-scenes
public struct LevelParams
{
    public int mazeSize;
    public float cannonPeriod;
}

public static class GameStats
{
    public static int Score { get; set; }
    public static int HighScore { get; set; }
    public static int Level { get; set; }

    public const int MazeScene = 1;
    public const int MainMenu = 0;
    public static void SaveScore()
    {
        if (Score > HighScore) HighScore = Score;
    }
    public static void Reset()
    {
        Score = 0;
        Level = 0;
    }
    public static void ResetHighScore()
    {
        HighScore = 0;
    }

    public static bool UsingCustomParams = false;
    public static LevelParams CurrentParams { get; set; }

}

public static class LevelManager
{
    public static void StartGame()
    {
        GameStats.Reset();
        SceneManager.LoadScene(GameStats.MazeScene);
    }
    public static void StageNext()
    {
        GameStats.Score += 1;
        GameStats.SaveScore();
        GameStats.Level += 1;
        SceneManager.LoadScene(GameStats.MazeScene);
    }
    public static void ReturnToMain()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(GameStats.MainMenu);
    }
}
