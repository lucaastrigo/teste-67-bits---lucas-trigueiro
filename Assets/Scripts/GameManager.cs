using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int money = 0;
    private int maxOfBodies = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
    }

    public void AddBodyCapacity(int amount)
    {
        maxOfBodies += amount;
    }

    public int GetMoney()
    {
        return money;
    }

    public int GetBodies()
    {
        return maxOfBodies;
    }
}