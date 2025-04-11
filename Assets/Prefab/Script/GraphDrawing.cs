using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GraphDrawing : MonoBehaviour
{
    public RectTransform graphContainer;
    public GameObject pointPrefab;
    public Text minValue;
    public Text maxValue;
    public float intervalMin;
    public float intervalMax;
    public float updateInterval;
    public int maxPoints;
    public float trackedVariable;

    private List<GameObject> points = new List<GameObject>();
    private float timer = 0f;
    private float angle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxValue.text = intervalMax.ToString();
        minValue.text = intervalMin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            timer = 0f;
            AddPoint(trackedVariable);
        }

        angle += 0.05f;
        trackedVariable = 50f + 50f * Mathf.Cos(angle % 360);
    }

    private void AddPoint(float value)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float normalizedValue = Mathf.InverseLerp(intervalMin, intervalMax, value);
        float yPosition = normalizedValue * graphHeight;

        float xSpacing = graphContainer.sizeDelta.x / maxPoints;
        float xPosition = points.Count * xSpacing;

        GameObject newPoint = Instantiate(pointPrefab, graphContainer);
        newPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPosition, yPosition);

        points.Add(newPoint);

        if (points.Count > maxPoints)
        {
            Destroy(points[0]);
            points.RemoveAt(0);

            for (int i = 0; i < points.Count; i++)
            {
                points[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(i * xSpacing, points[i].GetComponent<RectTransform>().anchoredPosition.y);
            }
        }
    }
    public void setTrackedVariable(float value)
    {
        trackedVariable = value;
    }
}
