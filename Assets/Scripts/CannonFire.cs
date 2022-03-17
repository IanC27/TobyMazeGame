using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonFire : MonoBehaviour
{

    public Transform target;
    public Transform firePosition;
    public GameObject projectile;
    public float period;
    public AudioSource fireSound;

    private float time = 0.0f;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 diff = target.position - transform.position;
        transform.right = diff.normalized;


        time += Time.deltaTime;

        if (time >= period)
        {
            time = time - period;
            fireSound.Play();
            Instantiate(projectile, firePosition.position, transform.rotation);
        }
    }
}
