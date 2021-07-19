using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frag : Projectile
{

    [SerializeField] GameObject FragPrefab;

    public override void SpecialAbility()
    {

        Vector2 preExplosionVelocity = rb.velocity;
        
        Vector2[] FragmentsMomentum = new Vector2[3];
        FragmentsMomentum[0] = preExplosionVelocity + Vector2.up * preExplosionVelocity.magnitude * 0.3f;
        FragmentsMomentum[1] = preExplosionVelocity * 0.3f;
        FragmentsMomentum[2] = preExplosionVelocity - Vector2.up * preExplosionVelocity.magnitude * 0.3f;

        for (int i = 0; i < 3; i++)
        {
            GameObject fragment = Instantiate(FragPrefab, transform.position, Quaternion.identity);
            fragment.GetComponent<Rigidbody2D>().AddForce(FragmentsMomentum[i], ForceMode2D.Impulse);
        }
        Destroy(gameObject);

    }
}
