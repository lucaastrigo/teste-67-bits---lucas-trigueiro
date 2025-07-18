using System.Collections;
using UnityEngine;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private int upgradeCost = 5;
    [SerializeField] private float upgradeDelay = 0.5f;

    private int coinsSpent;
    private Coroutine upgradeRoutine;

    private void Start()
    {
        GameManager.Instance.SetUpgradeCost(upgradeCost - coinsSpent);
    }

    private void OnTriggerEnter(Collider other)
    {
        var stack = other.GetComponent<BodyStackManager>();
        if (stack != null)
        {
            upgradeRoutine = StartCoroutine(UpgradeLoop());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BodyStackManager>() != null && upgradeRoutine != null)
        {
            StopCoroutine(upgradeRoutine);
            upgradeRoutine = null;
        }
    }

    private IEnumerator UpgradeLoop()
    {
        while (true)
        {
            if (GameManager.Instance.GetMoney() > 0)
            {
                GameManager.Instance.RemoveMoney(1);
                coinsSpent++;

                if (coinsSpent >= upgradeCost)
                {
                    GameManager.Instance.AddBodyCapacity(1);
                    GameManager.Instance.UpgradeCharColor();
                    coinsSpent = 0;
                }

                GameManager.Instance.SetUpgradeCost(upgradeCost - coinsSpent);
            }

            yield return new WaitForSeconds(upgradeDelay);
        }
    }
}