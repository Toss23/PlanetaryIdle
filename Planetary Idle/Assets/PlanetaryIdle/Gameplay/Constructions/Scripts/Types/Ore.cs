using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider))]
public class Ore : MonoBehaviour, IPointerClickHandler
{
    [Header("Main")]
    [SerializeField] private string displayName;
    [SerializeField] private Miner minerPrefab;

    [Header("View")]
    [SerializeField] private ProgressBar3D progressBar;
    [SerializeField] private GameObject energy;

    private bool minerBuilded = false;

    public string DisplayName { get { return displayName; } }
    public int Production { get { return minerPrefab.Configuration.Levels[1].OutputCount; } }
    public int MinerPrice { get { return minerPrefab.Configuration.Levels[0].Price; } }

    private void Awake()
    {
        progressBar.HideView();
        progressBar.SetProgress(0f, 1f);
        energy.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!minerBuilded)
        {
            View.HideAll();
            OreView.ShowView(this);
        }
    }

    public void BuildMiner()
    {
        // Create Miner with configuration
        GameObject miner = Instantiate(minerPrefab.gameObject, transform.position, transform.rotation, transform);
        miner.name = "Miner (" + displayName + ")";
            
        // Check as builded and hide UI
        minerBuilded = true;
        View.HideAll();
    }
}
