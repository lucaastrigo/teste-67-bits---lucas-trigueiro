using UnityEngine;
using System.Collections;

public class EnemyRagdollActivator : MonoBehaviour
{
    [HideInInspector] public bool isRagdolled;
    [HideInInspector] public bool canBeCollected;

    [SerializeField] private float collectionDelay = 1.5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Rigidbody[] ragdollRBs;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        SetRagdoll(false);
        canBeCollected = false;
    }

    public void ActivateRagdoll(Vector3 pushForce)
    {
        if (isRagdolled) return;
        isRagdolled = true;
        canBeCollected = false;

        anim.enabled = false;
        SetRagdoll(true);

        foreach (var rb in ragdollRBs)
            rb.AddForce(pushForce, ForceMode.Impulse);

        StartCoroutine(EnableCollectionAfterDelay());
    }

    public void DeactivateRagdoll()
    {
        if (!isRagdolled) return;

        isRagdolled = false;
        canBeCollected = false;

        SetRagdoll(false);

        if (anim != null)
        {
            anim.enabled = true;
            anim.Play("enemy-idle", 0, 0f);
            anim.Update(0f);
            anim.enabled = false;
        }
    }

    private IEnumerator EnableCollectionAfterDelay()
    {
        yield return new WaitForSeconds(collectionDelay);
        canBeCollected = true;
    }

    private void SetRagdoll(bool state)
    {
        foreach (var rb in ragdollRBs)
        {
            rb.isKinematic = !state;
            rb.detectCollisions = state;
        }
    }
}