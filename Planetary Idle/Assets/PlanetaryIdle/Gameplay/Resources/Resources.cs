using UnityEngine;

[CreateAssetMenu(fileName = "Resources", menuName = "Gameplay/Create Resources", order = 0)]
public class Resources : ScriptableObject
{
    [SerializeField] private ResourcePrefab[] prefabs;

    public ResourcePrefab[] Prefabs { get { return prefabs; } }
}

[System.Serializable]
public class ResourcePrefab
{
    [SerializeField] private string identifier;
    [SerializeField] private bool haveMaximum;
    [SerializeField] private string identifierMaximum;
    [SerializeField] private int maximum;

    public string Identifier { get { return identifier; } }
    public bool HaveMaximum { get { return haveMaximum; } }
    public string IdentifierMaximum { get { return identifierMaximum; } }
    public int Maximum { get { return maximum; } }
}