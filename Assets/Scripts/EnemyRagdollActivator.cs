using UnityEngine;

public class EnemyRagdollActivator : MonoBehaviour
{
    [HideInInspector] public bool isRagdolled;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Rigidbody[] ragdollRBs;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        SetRagdoll(false);
    }

    public void ActivateRagdoll(Vector3 pushForce)
    {
        if (isRagdolled) return;
        isRagdolled = true;

        anim.enabled = false;
        SetRagdoll(true);

        foreach (var rb in ragdollRBs)
            rb.AddForce(pushForce, ForceMode.Impulse);
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