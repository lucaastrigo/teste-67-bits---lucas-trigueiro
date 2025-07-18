using UnityEngine;

[RequireComponent(typeof(EnemyRagdollActivator))]
public class StackableBody : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer enemyRenderer;
    public Transform grabPoint;

    public void FreezeForStack()
    {
        if (enemyRenderer != null) enemyRenderer.material.color = Color.violet;

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }
}