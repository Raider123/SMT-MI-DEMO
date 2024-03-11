using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ShapeRecognizer : MonoBehaviour
{
    public GameObject plane;
    public Transform rayOrigin;
    public GameObject aimLine;

    private LineRenderer lineRenderer;

    private GameObject planeInstance;

    private Coroutine drawPointsCoroutine;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (plane == null)
        {
            Debug.LogError("Plane not assigned to ShapeRecognizer!");
        }

        if (rayOrigin == null)
        {
            rayOrigin = transform;
        }

        if (aimLine == null)
        {
            Debug.LogError("Aim line not assigned to ShapeRecognizer!");
        }
    }

    public void ActivateDrawing()
    {
        // instatiate plane, set aimline active, start ray to draw points onto the plane
        planeInstance = Instantiate(plane, plane.transform.position, plane.transform.rotation);
        planeInstance.SetActive(true);
        aimLine.SetActive(true);

        // start ray
        drawPointsCoroutine = StartCoroutine(DrawPoints());
    }

    public void DeactivateDrawing()
    {
        // stop ray
        if (drawPointsCoroutine != null)
        {
            StopCoroutine(drawPointsCoroutine);
            drawPointsCoroutine = null;
        }

        // destroy plane
        if (planeInstance != null)
        {
            Destroy(planeInstance);
            planeInstance = null;
        }

        // set aimline inactive
        aimLine.SetActive(false);

        // get all points from lineRenderer and store
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);
        lineRenderer.positionCount = 0;

        // send points to shape recognizer
        AnalizeShape(points);
    }

    /// <summary>
    /// Coroutine that draws points on the plane as long as it is active.
    /// </summary>
    /// <returns>Yields null after each point is drawn.</returns>
    IEnumerator DrawPoints()
    {
        while (true)
        {
            Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == planeInstance)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                }
            }
            yield return null;
        }
    }

    /// <summary>
    /// Analyzes the shape drawn by the user.
    /// </summary>
    /// <param name="points">The points that make up the shape drawn by the user.</param>
    private void AnalizeShape(Vector3[] points)
    {
        float circleMatch = CalculateCircleMatch(points);
        float squareMatch = CalculateSquareMatch(points);

        Debug.Log("Circle Match: " + circleMatch);
        Debug.Log("Square Match: " + squareMatch);
    }

    private float CalculateCircleMatch(Vector3[] points)
    {
        if (points.Length < 3)
            return 0f;

        Vector3 center = Vector3.zero;
        foreach (Vector3 point in points)
        {
            center += point;
        }
        center /= points.Length;

        float radiusSum = 0f;
        foreach (Vector3 point in points)
        {
            radiusSum += Vector3.Distance(center, point);
        }
        float averageRadius = radiusSum / points.Length;

        float circleMatch = 0f;
        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(center, point);
            float difference = Mathf.Abs(distance - averageRadius);
            circleMatch += 1f - difference / averageRadius;
        }
        circleMatch /= points.Length;

        return circleMatch;
    }

    private float CalculateSquareMatch(Vector3[] points)
    {
        if (points.Length < 4)
            return 0f;

        Vector3 center = Vector3.zero;
        foreach (Vector3 point in points)
        {
            center += point;
        }
        center /= points.Length;

        float distanceSum = 0f;
        foreach (Vector3 point in points)
        {
            distanceSum += Vector3.Distance(center, point);
        }
        float averageDistance = distanceSum / points.Length;

        float squareMatch = 0f;
        foreach (Vector3 point in points)
        {
            float distance = Vector3.Distance(center, point);
            float difference = Mathf.Abs(distance - averageDistance);
            squareMatch += 1f - difference / averageDistance;
        }
        squareMatch /= points.Length;

        return squareMatch;
    }

}
    
