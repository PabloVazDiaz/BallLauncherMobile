using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    [SerializeField] float Health = 70f;
    [SerializeField] AudioSource audio;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if ( rb == null)
            return;
        float damage = rb.velocity.magnitude;
        if (damage >= 10)
            audio.Play();
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject);
    }
}
