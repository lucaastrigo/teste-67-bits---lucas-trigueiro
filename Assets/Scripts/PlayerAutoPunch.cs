using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAutoPunch : MonoBehaviour
{
    [SerializeField] private float punchRadius = 1.8f;
    [SerializeField] private float punchCooldown = 1;
    [SerializeField] private LayerMask enemyMask;
    
    private float nextPunchTime;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time < nextPunchTime) return;
        
        Collider[] hits = Physics.OverlapSphere(transform.position, punchRadius, enemyMask);
        if (hits.Length > 0)
        {
            EnemyRagdollActivator enemy = hits[0].GetComponentInParent<EnemyRagdollActivator>();
            if (enemy != null && !enemy.isRagdolled)
            {
                anim.SetTrigger("punch");

                nextPunchTime = Time.time + punchCooldown;
                StartCoroutine(ApplyHitAfterDelay(1f, enemy));
            }
        }
    }

    private System.Collections.IEnumerator ApplyHitAfterDelay(float delay, EnemyRagdollActivator enemy)
    {
        yield return new WaitForSeconds(delay);
        enemy.ActivateRagdoll(transform.forward * 5f + Vector3.up * 2f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, punchRadius);
    }
#endif
}
