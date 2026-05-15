using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public CharacterData data;
    public GameObject projectilePrefab;

    private Animator animator;

    [Header("Attack Upgrade UI")]
    public GameObject attackUpgradePanel;
    private Button attackUpgradeButton;
    private TextMeshProUGUI attackUpgradeStatusText;

    [Header("Speed Upgrade UI")]
    public GameObject speedUpgradePanel;
    private Button speedUpgradeButton;
    private TextMeshProUGUI speedUpgradeStatusText;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Attack UI
        attackUpgradeButton =
            attackUpgradePanel.transform.GetChild(1).GetComponent<Button>();

        attackUpgradeStatusText =
            attackUpgradePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        attackUpgradeButton.onClick.AddListener(UpgradeAttack);

        // Speed UI
        speedUpgradeButton =
            speedUpgradePanel.transform.GetChild(1).GetComponent<Button>();

        speedUpgradeStatusText =
            speedUpgradePanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        speedUpgradeButton.onClick.AddListener(UpgradeSpeed);

        RefreshUI();
    }

    [System.Obsolete]
    void Update()
    {
        animator.speed = data.attackSpeed;

        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        animator.SetBool("isShooting", enemies.Length > 0);
    }

    [System.Obsolete]
    void Attack()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    void UpgradeAttack()
    {
        int currentCost = Mathf.RoundToInt(data.attackUpgradeCost);

        // Không đủ tiền thì cút
        if (UpgradeManager.instance.currentGold < currentCost)
            return;

        // Trừ tiền
        UpgradeManager.instance.AddGold(-currentCost);

        // Upgrade stat
        data.baseAttack += data.attackUpgradeIncrement;

        // Tăng giá upgrade
        data.attackUpgradeCost *= data.costMultiplier;

        RefreshUI();
    }

    void UpgradeSpeed()
    {
        int currentCost = Mathf.RoundToInt(data.speedUpgradeCost);

        // Không đủ tiền thì cút
        if (UpgradeManager.instance.currentGold < currentCost)
            return;

        // Trừ tiền
        UpgradeManager.instance.AddGold(-currentCost);

        // Upgrade stat
        data.attackSpeed += data.speedUpgradeIncrement;

        // Tăng giá upgrade
        data.speedUpgradeCost *= data.costMultiplier;

        RefreshUI();
    }

    void RefreshUI()
    {
        // Attack
        attackUpgradeStatusText.text =
            $"{data.baseAttack} -> {data.baseAttack + data.attackUpgradeIncrement}";

        attackUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text =
            $"Cost: {Mathf.RoundToInt(data.attackUpgradeCost)}";

        // Speed
        speedUpgradeStatusText.text =
            $"{data.attackSpeed:F1} -> {data.attackSpeed + data.speedUpgradeIncrement:F1}";

        speedUpgradeButton.GetComponentInChildren<TextMeshProUGUI>().text =
            $"Cost: {Mathf.RoundToInt(data.speedUpgradeCost)}";
    }
}