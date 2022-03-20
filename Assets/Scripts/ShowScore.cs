using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public bool highScore;
    // Start is called before the first frame update
    void Start()
    {
        if (highScore)
        {
            GetComponent<Text>().text = GameStats.HighScore.ToString();
        } else
        {
            GetComponent<Text>().text = GameStats.Score.ToString();
        }
        
    }

}