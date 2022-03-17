using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 30;
    public Rigidbody2D rb;
    public Animator animator;

    public AudioSource stepSource;
    public AudioClip[] stepSounds;

    private Vector2 movement;
    private float _t;
    private bool tog;


    private void Awake()
    {
        _t = 0f;
        tog = true;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {

        Vector2 totalMove = movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + totalMove);
        if (totalMove != Vector2.zero)
        {
            _t += Time.fixedDeltaTime * moveSpeed;

            if (_t >= 2f)
            {
                _t = _t % 2f;
                // two foot sounds: toggle based
                stepSource.PlayOneShot(stepSounds[tog ? 0 : 1]);
                tog = !tog;

            }
        }
        
        
    }

}
