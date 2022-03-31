using System.Collections.Generic;
using UnityEngine;

public class ResourcesSystem : MonoBehaviour
{
    private static List<Resource> resources;

    [Header("Main")]
    [SerializeField] private Resources resourcePrefabs;

    public Resources Resources { get { return resourcePrefabs; } }

    private void Awake()
    {
        // Static
        resources = new List<Resource>();

        // Resources
        foreach (ResourcePrefab resource in resourcePrefabs.Prefabs)
        {
            if (resource.HaveMaximum)
                Resource.Create(resource.Identifier, transform, resource.IdentifierMaximum, resource.Maximum);
            else
                Resource.Create(resource.Identifier, transform);
        }

        // Start
        Find("Gold").Add(10000);
    }

    /// <summary>
    /// Registering a resource so that it can be found
    /// </summary>
    public static void Register(Resource resource)
    {
        resources.Add(resource);
    }

    /// <summary>
    /// Find a resource with the specified name
    /// </summary>
    public static Resource Find(string identifier)
    {
        if (identifier == "Null")
        {
            Debug.LogError("You try find <Null> resource... Please check configurations");
            return null;
        }

        foreach (Resource resource in resources)
        {
            if (resource.Identifier == identifier)
                return resource;
        }

        Debug.LogError("Resource with identifier <" + identifier + "> not found... Returned <Null>");
        return null;
    }
}