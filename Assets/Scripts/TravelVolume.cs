using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelVolume : MonoBehaviour
{
    public Collider2D player;
    // public bool isDeath = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision");
        if (collision == player)
        {
             LevelManager.StageNext();
        }
    }
}
