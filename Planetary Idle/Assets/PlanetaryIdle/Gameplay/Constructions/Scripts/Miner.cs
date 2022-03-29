using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class Miner : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string resourceIdentifier;
    [SerializeField] private int level;
    [SerializeField] private Configuration configuration;

    private Resource resource;
    private float productionTimer;

    public Configuration Configuration { get { return configuration; } }

    private void Awake()
    {
        level = 1;
        productionTimer = configuration.ProductionInterval;
    }

    private void Start()
    {
        resource = ResourcesSystem.Find(resourceIdentifier);
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
        // Show UI
    }
}
