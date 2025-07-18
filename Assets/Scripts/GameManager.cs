using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI coinValue;
    [SerializeField] private TextMeshProUGUI bodiesValue;
    [SerializeField] private TextMeshProUGUI fps;
    [SerializeField] private TextMeshPro upgradeCostValue;
    [SerializeField] private PlayerColor playerColor;

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

    private void Start()
    {
        Application.targetFrameRate = 60;

        if (coinValue != null) coinValue.text = money.ToString();
        if (bodiesValue != null) bodiesValue.text = maxOfBodies.ToString();
    }

    private void Update()
    {
        if (fps == null) return;

        float fpsValue = 1.0f / Time.deltaTime;
        string text = string.Format("{0:0.} FPS", fpsValue);

        fps.text = text;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        if (coinValue != null) coinValue.text = money.ToString();
    }

    public void RemoveMoney(int amount)
    {
        money -= amount;
        if (coinValue != null) coinValue.text = money.ToString();
    }

    public void AddBodyCapacity(int amount)
    {
        maxOfBodies += amount;
        if (bodiesValue != null) bodiesValue.text = maxOfBodies.ToString();
    }

    public void SetUpgradeCost(int amount)
    {
        if (upgradeCostValue != null) upgradeCostValue.text = amount.ToString();
    }

    public void UpgradeCharColor()
    {
        if (playerColor != null) playerColor.LevelUp();
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