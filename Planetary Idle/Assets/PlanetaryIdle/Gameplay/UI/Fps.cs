using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Fps : MonoBehaviour
{
    [SerializeField] private int targetFps = 60;

    private Text text;

    private void Awake()
    {
        if (targetFps < 30)
            targetFps = 30;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;
    }

    private void Start()
    {
        text = GetComponent<Text>();
        frames = 0;
        timer = 1f;
    }

    private float timer;
    private int frames;
    private float fps;

    private void Update()
    {
        frames++;

        if (timer < 1f)
        {
            timer += Time.deltaTime;
        }
        else
        {
            fps = frames - 1;
            frames = 0;
            timer -= 1f;
        }

        text.text = "Fps: " + Mathf.Round(fps);
    }
}
