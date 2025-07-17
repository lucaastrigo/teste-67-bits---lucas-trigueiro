using System.Collections;
using UnityEngine;

public class SellArea : MonoBehaviour
{
    [SerializeField] private float sellDelay = 0.5f;

    private Coroutine sellRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BodyStackManager>() != null)
        {
            var stack = other.GetComponent<BodyStackManager>();
            if (stack != null)
            {
                sellRoutine = StartCoroutine(SellLoop(stack));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BodyStackManager>() != null && sellRoutine != null)
        {
            StopCoroutine(sellRoutine);
            sellRoutine = null;
        }
    }

    private IEnumerator SellLoop(BodyStackManager stack)
    {
        while (stack.HasBodies())
        {
            yield return new WaitForSeconds(sellDelay);
            stack.SellTopBody();
        }
    }
}