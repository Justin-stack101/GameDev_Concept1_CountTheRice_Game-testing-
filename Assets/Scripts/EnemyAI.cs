using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// IT2107 Lab 1 - Enemy Bot AI
/// Enemy chases and attacks the player when in range
/// </summary>
public class EnemyAI : MonoBehaviour
{
    [Header("Detection")]
    public float detectionRange = 15f;
    public float attackRange = 2f;

    [Header("Combat")]
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;

    [Header("References")]
    public Transform player;

    [Header("Movement Fallback")]
    public float fallbackSpeed = 3.5f;

    private NavMeshAgent agent;
    private float lastAttackTime;
    private Renderer enemyRenderer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Auto-find player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        enemyRenderer = GetComponentInChildren<Renderer>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Chase player via NavMesh if available
            if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                // Fallback direct movement towards player
                Vector3 targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, fallbackSpeed * Time.deltaTime);
                transform.LookAt(targetPos);
            }

            // Flash red when chasing
            if (enemyRenderer != null)
                enemyRenderer.material.color = Color.red;

            // Attack when close enough
            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
        }
        else
        {
            // Idle - stop moving
            if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
                agent.ResetPath();

            // Return to default color
            if (enemyRenderer != null)
                enemyRenderer.material.color = new Color(0.8f, 0.4f, 0f); // Orange
        }
    }

    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            playerHealth.TakeDamage(attackDamage);

        Debug.Log("Enemy attacks player!");
    }

    // Draw detection range in Scene view (editor only)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
