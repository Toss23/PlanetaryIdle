using UnityEngine;

public class Resource : MonoBehaviour
{
    private const int DEFAULT_MAXIMUM = 10000000;

    [SerializeField] [Min(0)] private int count;

    public string Identifier { get; private set; }
    public Resource Maximum { get; private set; }

    public int Count { get { return count; } }

    /// <summary>
    /// Create a resource with the specified name and container in which the GameObject will be placed
    /// </summary>
    public static Resource Create(string identifier, Transform container)
    {
        GameObject gameObject = new GameObject(identifier);
        gameObject.transform.parent = container;

        Resource resource = gameObject.AddComponent<Resource>();
        resource.Identifier = identifier;

        ResourcesSystem.Register(resource);
        return resource;
    }

    /// <summary>
    /// Create a resource with the specified name and container in which the GameObject will be placed<br/>
    /// Also create a resource that contain maximum and can be changed
    /// </summary>
    public static Resource Create(string identifier, Transform container, string identifierMaximum, int maximum = DEFAULT_MAXIMUM)
    {
        Resource resource = Create(identifier, container);
        resource.Maximum = Create(identifierMaximum, container);
        resource.Maximum.count = maximum;
        return resource;
    }

    private void Awake()
    {
        // Load data
    }

    /// <summary>
    /// If maximum count return true
    /// </summary>
    public bool Add(int value)
    {
        int maximum = Maximum == null ? DEFAULT_MAXIMUM : Maximum.count;
        value = Mathf.Abs(value);

        if (count + value <= maximum)
        {
            count += value;
            return false;
        }
        else
        {
            count = maximum;
            return true;
        }
    }

    /// <summary>
    /// If successful spend return true
    /// </summary>
    public bool Spend(int value)
    {
        value = Mathf.Abs(value);

        if (count - value >= 0)
        {
            count -= value;
            return true;
        }
        else
        {
            return false;
        }
    }
}
