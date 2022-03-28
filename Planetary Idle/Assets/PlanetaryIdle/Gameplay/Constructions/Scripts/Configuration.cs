using UnityEngine;

[CreateAssetMenu(fileName = "Configuration", menuName = "Gameplay/New Configuration", order = 1)]
public class Configuration : ScriptableObject
{
    [SerializeField] private string[] priceRersource;
    [SerializeField] private int[] prices;

    [SerializeField] private bool haveConsumption;
    [SerializeField] private string[] resourceInput;
    [SerializeField] private int[] consumptions;

    [SerializeField] private int productionInterval;
    [SerializeField] private string[] resourceOutput;
    [SerializeField] private int[] productions;

    public string[] PriceResource { get { return priceRersource; } }
    public int[] Prices { get { return prices; } }

    public bool HaveConsumption { get { return haveConsumption; } }
    public string[] ResourceInput { get { return resourceInput; } }
    public int[] Consumptions { get { return consumptions; } }

    public int ProductionInterval { get { return productionInterval; } }
    public string[] ResourceOutput { get { return resourceOutput; } }
    public int[] Productions { get { return productions; } }
}
