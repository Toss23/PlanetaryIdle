using UnityEngine;
using UnityEngine.UI;

public class OreView : View
{
    private static OreView Instance;

    [Header("View")]
    [SerializeField] private Text oreNameText;
    [SerializeField] private Text upgradeText;

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
        Instance.upgradeText.text = "Buy (" + selectedOre.MinerPrice + " Gold)";
    }

    protected override void OnClickUpgrade()
    {
        //Resource Gold = ResourcesSystem.Find("Gold");
        //if (Gold.Spend(selectedOre.MinerPrice))
            //selectedOre.BuildMiner();
    }

    protected override void OnClickDestroy() { }
}
