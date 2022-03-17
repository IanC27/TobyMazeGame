using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelVolume : MonoBehaviour
{
    public Collider player;
    public bool isDeath = false;
    public int levelIndex;

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("collision");
        if (collision == player)
        {
            if (isDeath)
            {
                LevelManager.GameOver();
            }
            else
            {
                LevelManager.LoadLevel(levelIndex);
            }
        }
    }
}
