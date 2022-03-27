using UnityEngine;

[CreateAssetMenu(fileName = "Miner", menuName = "Gameplay/New Miner Configuration", order = 1)]
public class MinerConfiguration : ScriptableObject
{
    [Header("Main")]
    [SerializeField] private int[] prices;
    [SerializeField] private int[] productions;
    [SerializeField] private int productionInterval;

    public int[] Prices { get { return prices; } }
    public int[] Productions { get { return productions; } }
    public int ProdutionInterval { get { return productionInterval; } }
}
