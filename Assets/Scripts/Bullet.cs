using UnityEngine;

/// <summary>
/// IT2107 Lab 1 - Bullet Script
/// Bullet deals damage to enemies on collision
/// </summary>
public class Bullet : MonoBehaviour
{
    [Header("Damage")]
    public float damage = 25f;

    void OnCollisionEnter(Collision collision)
    {
        // Try to damage enemy
        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Destroy bullet on any collision (wall, floor, enemy, etc.)
        Destroy(gameObject);
    }
}
