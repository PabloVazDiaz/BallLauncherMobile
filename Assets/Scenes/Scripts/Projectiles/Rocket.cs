using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rocket : Projectile 
{

    
    [SerializeField] float impulseForce = 10f;
    
    

    public override void SpecialAbility()
    {
        rb.AddForce(rb.velocity * impulseForce,ForceMode2D.Impulse);

    }

   
}
