using System.Collections.Generic;
using UnityEngine;

public class BodyStackManager : MonoBehaviour
{
    [SerializeField] private Transform stackRoot;
    [SerializeField] private float yOffset = 0.7f;
    [SerializeField] private float followSmooth = 8;
    [SerializeField] private float captureRadius = 1.2f;
    [SerializeField] private LayerMask bodyMask;

    private List<StackedBody> stack = new();

    private void Update()
    {
        TryCaptureBodies();
        UpdateStackPositions();
    }

    private void TryCaptureBodies()
    {
        if (stack.Count >= GameManager.Instance.GetBodies()) return;

        Collider[] hits = Physics.OverlapSphere(stackRoot.position, captureRadius, bodyMask);
        foreach (var h in hits)
        {
            StackableBody body = h.GetComponentInParent<StackableBody>();
            EnemyRagdollActivator ragdoll = h.GetComponentInParent<EnemyRagdollActivator>();
            if (body == null) continue;
            if (stack.Exists(s => s.grabPoint == body.grabPoint)) continue;
            if (ragdoll == null) continue;
            if (!ragdoll.isRagdolled) continue;
            if (!ragdoll.canBeCollected) continue;

            ragdoll.DeactivateRagdoll();
            body.FreezeForStack();

            stack.Add(new StackedBody
            {
                bodyTransform = body.transform,
                grabPoint = body.grabPoint,
                velocity = Vector3.zero
            });
        }
    }

    private void UpdateStackPositions()
    {
        if (stack.Count == 0) return;

        Vector3 targetPos = stackRoot.position;
        Quaternion targetRot = transform.rotation;

        for (int i = 0; i < stack.Count; i++)
        {
            StackedBody body = stack[i];

            if (i > 0) targetPos += Vector3.up * yOffset;

            Vector3 offset = body.bodyTransform.position - body.grabPoint.position;
            Vector3 desiredPos = targetPos + offset;

            body.bodyTransform.position = Vector3.SmoothDamp(
                body.bodyTransform.position,
                desiredPos,
                ref body.velocity,
                0.12f
            );

            body.bodyTransform.rotation = Quaternion.Slerp(
                body.bodyTransform.rotation,
                targetRot,
                Time.deltaTime 
            );

            targetPos = body.bodyTransform.position;
            targetRot = body.bodyTransform.rotation;
        }
    }

    public bool HasBodies()
    {
        return stack.Count > 0;
    }

    public void SellTopBody()
    {
        if (stack.Count == 0) return;

        StackedBody sold = stack[stack.Count - 1];
        stack.RemoveAt(stack.Count - 1);
        GameManager.Instance.AddMoney(1);

        Destroy(sold.bodyTransform.gameObject);
    }
}

[System.Serializable]
public class StackedBody
{
    public Transform bodyTransform;
    public Transform grabPoint;
    public Vector3 velocity;
}