using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar3D : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float scaleMax = 1;

    [Header("View")]
    [SerializeField] private GameObject fillLine;
    [SerializeField] private GameObject emptyLine;

    private Vector3 fillLineBeginPosition, emptyLineBeginPosition;

    public void SetProgress(float current, float maximum)
    {
        if (current > maximum)
            current = maximum;

        fillLine.transform.localScale = new Vector3(scaleMax * current / maximum, fillLine.transform.localScale.y, fillLine.transform.localScale.z);
        emptyLine.transform.localScale = new Vector3(scaleMax * (maximum - current) / maximum, fillLine.transform.localScale.y, fillLine.transform.localScale.z);

        fillLine.transform.localPosition = new Vector3(scaleMax / 2f * current / maximum - scaleMax / 2f, 0f, 0f);
        emptyLine.transform.localPosition = new Vector3(- scaleMax / 2f * (maximum - current) / maximum + scaleMax / 2f, 0f, 0f);
    }

    public void ShowView()
    {
        fillLine.SetActive(true);
        emptyLine.SetActive(true);
    }

    public void HideView()
    {
        fillLine.SetActive(false);
        emptyLine.SetActive(false);
    }
}
