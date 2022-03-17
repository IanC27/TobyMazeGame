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
public static class GameStats
{
    public static int Score { get; set; }
    public static int SavedScore { get; set; }
    public static int Level { get; set; }
    public static void SaveScore()
    {
        SavedScore = Score;
    }
    public static void RevertScore()
    {
        Score = SavedScore;
    }
}

public static class LevelManager
{
    public static void ReloadScene()
    {
        GameStats.RevertScore();
        SceneManager.LoadScene(GameStats.Level);
    }
    public static void LoadLevel(int level)
    {
        GameStats.SaveScore();
        SceneManager.LoadScene(level);
    }
    public static void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("GameOver");
    }
}
