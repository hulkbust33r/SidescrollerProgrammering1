using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    public bool IsPlayer = false;

    public int DamageValue = -1;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(IsPlayer)
        {
            var enemyScript = collision.gameObject.GetComponent<GoombaEnemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(DamageValue);
            }
        }
        else
        {
            var PlayerScript = collision.gameObject.GetComponent<PhysicsCharacterController>();
            if (PlayerScript != null)
            {
                PlayerScript.TakeDamage(DamageValue);
            }
        }
    }
}
