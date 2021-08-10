using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{

    [SerializeField] float Health = 150f;
    [SerializeField] int points = 100;
    [SerializeField] AudioSource deathAudio;
    public delegate void DeathHandler(int points, Pig deadPig);

    public event DeathHandler OnDeath;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rbCollision = collision.gameObject.GetComponent<Rigidbody2D>();
    
        if (rbCollision == null)
        {
            Health -= gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;
            //return;
        }
        else
        {
            //if we are hit by a bird
            if (collision.gameObject.CompareTag("Bird"))
            {
                Die();
            }
            else //we're hit by something else
            {
                //calculate the damage via the hit object velocity
                float damage = rbCollision.velocity.magnitude * 10;
                Health -= damage;
                //don't play sound for small damage
                if (damage >= 10)
                    GetComponent<AudioSource>().Play();

                if (Health <= 0)
                    Die();
            }

        }

    }


    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathAudio.clip, transform.position);
        if (OnDeath != null) 
            OnDeath(points, this);

        Destroy(this.gameObject);
    }
   
}
