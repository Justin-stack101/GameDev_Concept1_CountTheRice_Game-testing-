using UnityEngine;

/// <summary>
/// IT2107 Lab 1 - Enemy Health System
/// Attach this to the Enemy GameObject
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 50f;
    private float currentHealth;

    private Renderer enemyRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        enemyRenderer = GetComponentInChildren<Renderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Enemy hit! HP remaining: {currentHealth}");

        // Flash white when hit
        if (enemyRenderer != null)
            StartCoroutine(FlashOnHit());

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Enemy defeated!");
        Destroy(gameObject); // Remove enemy from scene
    }

    System.Collections.IEnumerator FlashOnHit()
    {
        if (enemyRenderer == null) yield break;
        Color original = enemyRenderer.material.color;
        enemyRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        enemyRenderer.material.color = original;
    }
}
