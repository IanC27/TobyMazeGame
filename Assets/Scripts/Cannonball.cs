using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("collision!");
        if (collider.gameObject.layer != LayerMask.NameToLayer("ProjectileTransparent"))
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.GetComponent<DamageSystem>().CannonballDeath();
            } 
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
