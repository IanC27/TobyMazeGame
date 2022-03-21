using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageSystem : MonoBehaviour
{
    public Animator animator;
    public AudioSource scream;
    public GameObject gameOverCanvas;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.StartGame();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.ReturnToMain();
        }
    }
    public void GameOver()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
        PlayerMovement plm = GetComponent<PlayerMovement>();
        plm.enabled = false;
        gameOverCanvas.SetActive(true);
    }

    public void CannonballDeath()
    {
        animator.SetBool("Dead", true);
        animator.Play("TobyDeadHole");
        scream.Play();

        // Debug.Log("ouch");
        GameOver();
    }
}
