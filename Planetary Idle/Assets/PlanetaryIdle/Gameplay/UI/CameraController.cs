using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float radius;
    [SerializeField] private Vector2 angles;

    [Header("Touch")]
    [SerializeField] private float sensetive;

    [Header("Zoom")]
    [SerializeField] private Slider zoomSlider;

    private Vector2 beginAngles;
    private Vector2 touch, beginTouch, deltaTouch;

    private int width, height;

    private void Awake()
    {
        width = Screen.width;
        height = Screen.height;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                beginTouch = Input.mousePosition;
                beginAngles = angles;
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                touch = Input.mousePosition;
                deltaTouch = touch - beginTouch;
                angles = beginAngles + new Vector2(deltaTouch.x / width, -deltaTouch.y / height) * sensetive;
            }

            zoomSlider.value -= Input.GetAxis("Mouse ScrollWheel");
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        beginTouch = touch.position;
                        beginAngles = angles;
                        break;
                    case TouchPhase.Moved:
                        this.touch = touch.position;
                        deltaTouch = this.touch - beginTouch;
                        angles = beginAngles + new Vector2(deltaTouch.x / width, -deltaTouch.y / height) * sensetive;
                        break;
                }
            }
        }

        transform.localEulerAngles = new Vector3(angles.y, angles.x);
        transform.localPosition = transform.localRotation * new Vector3(0f, 0f, -(radius * (1 + zoomSlider.value)));
    }
}
