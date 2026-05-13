using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager instance;

    public Canvas canvas;

    private GameObject upgradePanel;
    private Button toggleButton;

    private bool isUpgradePanelActive;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        canvas.gameObject.SetActive(true);

        upgradePanel = canvas.transform.Find("UpgradingBox").gameObject;
        toggleButton = canvas.transform.Find("ToggleUpgrades").GetComponent<Button>();

        upgradePanel.SetActive(false);

        toggleButton.onClick.AddListener(ToggleUpgradePanel);
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
}