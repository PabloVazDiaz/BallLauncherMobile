using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{

    public bool isLaunched;
    public bool isAbilityUsed = false;
    [HideInInspector]
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public bool isSpecialAbilityReady()
    {
        if (!isAbilityUsed)
        {
            isAbilityUsed = true;
            return true;
        }

        return isAbilityUsed;

    }

    public void Launch()
    {
        
        isLaunched = true;
        //do sound
        
    }

    public virtual void Update()
    {
        if (isLaunched && !isAbilityUsed && Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isSpecialAbilityReady())
            {
                SpecialAbility();
            }
        }
        //Stop destroy when not on ground
        if (isLaunched && rb.velocity.magnitude <= 0.05f)
            Destroy(gameObject);
    }

    public virtual void SpecialAbility()
    {
        
    }
}
