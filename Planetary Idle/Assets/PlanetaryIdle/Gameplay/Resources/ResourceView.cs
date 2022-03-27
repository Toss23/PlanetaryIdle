using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ResourceView : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private string resourceName;
    [SerializeField] private bool displayMaximum = false;

    private Text text;
    private Resource resource;

    private void Start()
    {
        text = GetComponent<Text>();
        resource = ResourcesSystem.Find(resourceName);
    }

    private void Update()
    {
        if (resource != null)
        {
            if (displayMaximum)
                text.text = resource.Identifier + ": " + resource.Count + "/" + resource.Maximum.Count;
            else
                text.text = resource.Identifier + ": " + resource.Count;
        }
    }
}
