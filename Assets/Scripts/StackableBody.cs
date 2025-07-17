using UnityEngine;

[RequireComponent(typeof(EnemyRagdollActivator))]
public class StackableBody : MonoBehaviour
{
    public Transform grabPoint;

    public void FreezeForStack()
    {
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }
}