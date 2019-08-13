using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    // Use this for initialization
    void Start () {
        
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                var enemy = enemiesToDamage[i];

                var enemyComponent = enemy.GetComponent<PlatformerPro.Enemy>();
                if (enemyComponent!=null)
                {
                    enemyComponent.Damage(new PlatformerPro.DamageInfo(
                                                                                    1,
                                                                                    PlatformerPro.DamageType.PHYSICAL,
                                                                                    new Vector2(10,10)
                                                                                )
                                                );
                }

                
            }
        }        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
