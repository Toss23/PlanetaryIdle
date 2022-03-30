using UnityEngine;
using UnityEngine.UI;

public class MinerView : View
{
    private static MinerView Instance;

    [Header("View")]
    [SerializeField] private Text minerNameText;
    [SerializeField] private Text productionText;

    private Miner selectedMiner;

    private Color productionTextColor;

    protected override void Initialize()
    {
        Instance = this;
        productionTextColor = Instance.productionText.color;
    }

    public static void ShowView(Miner miner)
    {
        Instance.selectedMiner = miner;
        Instance.ShowView();
    }

    protected override void UpdateView()
    {
        ConfigurationLevel level = selectedMiner.Configuration.Levels[selectedMiner.Level];
        Instance.minerNameText.text = "Name: " + selectedMiner.ResourceIdentifier + " Miner";
        Instance.productionText.text = "Produce: " + level.OutputCount + " " + level.OutputResource + " / Sec";

        if (selectedMiner.Level < selectedMiner.MaxLevel)
        {
            Instance.EnableUpgrade();
            Instance.upgradeText.text = "Upgrade (" + level.Price + " " + level.PriceResource + ")";
        }
        else
        {
            Instance.DisableUpgrade();
            Instance.upgradeText.text = "Max Level";
        }
    }

    protected override void OnUpgradeButtonFocused()
    {
        if (selectedMiner.Level + 1 <= selectedMiner.MaxLevel)
        {
            ConfigurationLevel level = selectedMiner.Configuration.Levels[selectedMiner.Level + 1];
            Instance.productionText.color = hintColor;
            Instance.productionText.text = "Produce: " + level.OutputCount + " " + level.OutputResource + " / Sec";
        }
    }

    protected override void OnUpgradeButtonUnfocused()
    {
        Instance.productionText.color = productionTextColor;
    }

    protected override void OnClickUpgrade()
    {
        ConfigurationLevel level = selectedMiner.Configuration.Levels[selectedMiner.Level];
        if (ResourcesSystem.Find(level.PriceResource).Spend(level.Price))
            selectedMiner.Level++;
    }
}
