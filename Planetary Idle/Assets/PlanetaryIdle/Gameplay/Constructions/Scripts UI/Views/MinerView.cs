using UnityEngine;
using UnityEngine.UI;

public class MinerView : View
{
    private static MinerView Instance;

    [Header("View")]
    [SerializeField] private Text minerNameText;
    [SerializeField] private Text productionText;
    [SerializeField] private Text upgradeText;

    private Miner selectedMiner;

    protected override void Initialize()
    {
        Instance = this;
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
        Instance.upgradeText.text = "Upgrade (" + level.Price + " " + level.PriceResource + ")";
    }

    protected override void OnClickUpgrade()
    {

    }

    protected override void OnClickDestroy()
    {
        
    }
}
