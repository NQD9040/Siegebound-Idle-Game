using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    [Header("Canvas")]
    public Canvas canvas;

    private GameObject upgradePanel;
    private Button toggleButton;

    private bool isUpgradePanelActive;

    [Header("Income Upgrade")]
    public GameObject incomeBonusPanel;

    private Button incomeBonusButton;
    private TextMeshProUGUI incomeBonusStatsText;

    [Header("Currency")]
    public int currentGold = 0;

    [Header("Income Stats")]
    public float incomeMultiplier = 1f;

    [Header("Income Upgrade Cost")]
    public int incomeCost = 10;
    public float incomeCostMultiplier = 2f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Income UI
        incomeBonusButton =
            incomeBonusPanel.transform.GetChild(1).GetComponent<Button>();

        incomeBonusStatsText =
            incomeBonusPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        incomeBonusButton.onClick.AddListener(UpgradeIncome);

        // Canvas
        canvas.gameObject.SetActive(true);

        upgradePanel = canvas.transform.Find("UpgradingBox").gameObject;

        toggleButton =
            canvas.transform.Find("ToggleUpgrades").GetComponent<Button>();

        upgradePanel.SetActive(false);

        toggleButton.onClick.AddListener(ToggleUpgradePanel);

        RefreshIncomeUI();
    }

    public bool IsUpgradePanelActive()
    {
        return isUpgradePanelActive;
    }

    public void ToggleUpgradePanel()
    {
        SetUpgradePanel(!isUpgradePanelActive);
    }

    public void SetUpgradePanel(bool active)
    {
        if (isUpgradePanelActive == active)
            return;

        isUpgradePanelActive = active;

        upgradePanel.SetActive(active);

        Debug.Log($"Upgrade Panel {(active ? "Activated" : "Deactivated")}");
    }

    // =========================
    // GOLD SYSTEM
    // =========================

    public void AddGold(float amount)
    {
        int finalGold = Mathf.RoundToInt(amount * incomeMultiplier);

        currentGold += finalGold;

        if (currentGold < 0)
            currentGold = 0;
    }

    public bool SpendGold(int amount)
    {
        if (currentGold < amount)
            return false;

        currentGold -= amount;

        if (currentGold < 0)
            currentGold = 0;

        return true;
    }

    // =========================
    // INCOME UPGRADE
    // =========================

    void UpgradeIncome()
    {
        if (!SpendGold(incomeCost))
        {
            Debug.Log("Not enough gold.");
            return;
        }

        incomeMultiplier += 1f;

        incomeCost =
            Mathf.RoundToInt(incomeCost * incomeCostMultiplier);

        RefreshIncomeUI();
    }

    void RefreshIncomeUI()
    {
        incomeBonusButton
            .transform
            .GetChild(0)
            .GetComponent<TextMeshProUGUI>().text =
            $"Cost: {incomeCost}";

        incomeBonusStatsText.text =
            $"{incomeMultiplier:F1} -> {(incomeMultiplier + 1f):F1}";
    }
}