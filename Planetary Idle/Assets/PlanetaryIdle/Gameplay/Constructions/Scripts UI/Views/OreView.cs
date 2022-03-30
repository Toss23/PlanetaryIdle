using UnityEngine;
using UnityEngine.UI;

public class OreView : View
{
    private static OreView Instance;

    [Header("View")]
    [SerializeField] private Text oreNameText;
    [SerializeField] private Text productionText;

    private Ore selectedOre;

    protected override void Initialize()
    {
        Instance = this;
    }

    public static void ShowView(Ore ore)
    {
        Instance.selectedOre = ore;
        Instance.ShowView();
    }

    protected override void UpdateView()
    {
        Instance.oreNameText.text = "Name: " + selectedOre.DisplayName;
        Instance.productionText.text = "Produce: " + selectedOre.Production + " " + selectedOre.DisplayName + " / Sec";
        Instance.upgradeText.text = "Build Miner (" + selectedOre.MinerPrice + " Gold)";
    }

    protected override void OnClickUpgrade()
    {
        if (ResourcesSystem.Find("Gold").Spend(selectedOre.MinerPrice))
            selectedOre.BuildMiner();
    }
}
