using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class Miner : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int level;
    [SerializeField] private Configuration configuration;

    private Resource resource;
    private float productionTimer;

    public string ResourceIdentifier { get { return configuration.OutputResource; } }
    public int Level { get { return level; } set { level = value; } }
    public int MaxLevel { get { return configuration.Levels.Length - 1; } }
    public Configuration Configuration { get { return configuration; } }

    private void Awake()
    {
        level = 1;
        productionTimer = configuration.ProductionInterval;
    }

    private void Start()
    {
        resource = ResourcesSystem.Find(ResourceIdentifier);
    }

    private void Update()
    {
        productionTimer -= Time.deltaTime;

        if (productionTimer <= 0)
        {
            productionTimer = configuration.ProductionInterval;

            // Action on production circle end
            resource.Add(configuration.Levels[level].OutputCount);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MinerView.ShowView(this);
    }
}
