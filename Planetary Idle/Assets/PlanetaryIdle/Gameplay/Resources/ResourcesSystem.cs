using System.Collections.Generic;
using UnityEngine;

public class ResourcesSystem : MonoBehaviour
{
    private static List<Resource> resources;

    [Header("Main")]
    [SerializeField] private Resources resourcePrefabs;

    private void Awake()
    {
        // Static
        resources = new List<Resource>();

        // Resources
        foreach (ResourcePrefab resource in resourcePrefabs.Prefabs)
        {
            if (resource.HaveMaximum)
                Resource.Create(resource.Identifier, transform, resource.IdentifierMaximum);
            else
                Resource.Create(resource.Identifier, transform);
        }
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
        foreach (Resource resource in resources)
        {
            if (resource.Identifier == identifier)
                return resource;
        }

        Debug.LogError("Resource with identifier <" + identifier + "> not found... Returned <Null>");
        return null;
    }
}