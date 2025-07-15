using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Animator anim;
    private Rigidbody rb;
    private Vector2 moveInput;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 desiredVelocity = new Vector3(moveInput.x, 0f, moveInput.y).normalized * moveSpeed;
        desiredVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = desiredVelocity;

        RotateTowardsMovement();

        anim.SetBool("walk", moveInput.sqrMagnitude > 0.01f);
    }

    private void RotateTowardsMovement()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(flatVel);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.fixedDeltaTime * 10f);
        }
    }
}
