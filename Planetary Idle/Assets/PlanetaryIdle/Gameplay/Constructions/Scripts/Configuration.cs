using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Gameplay/New Configuration", order = 1)]
public class Configuration : ScriptableObject
{
    [SerializeField] private ConfigurationLevel[] levels;
    [SerializeField] private int productionInterval = 1;
    [SerializeField] private bool haveInput = true;

    public ConfigurationLevel[] Levels { get { return levels; } }
    public int ProductionInterval { get { return productionInterval; } }
    public bool HaveInput { get { return haveInput; } }
}

[System.Serializable]
public class ConfigurationLevel
{
    // Upgrade price
    [SerializeField] private string priceResource;
    [SerializeField] private int price;

    // Input
    [SerializeField] private string inputResource;
    [SerializeField] private int inputCount;

    // Output
    [SerializeField] private string outputResource;
    [SerializeField] private int outputCount;

    public string PriceResource { get { return priceResource; } }
    public int Price { get { return price; } }

    public string InputResource { get { return inputResource; } }
    public int InputCount { get { return inputCount; } }

    public string OutputResource { get { return outputResource; } }
    public int OutputCount { get { return outputCount; } }
}