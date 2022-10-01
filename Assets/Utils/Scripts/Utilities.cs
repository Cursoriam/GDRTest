using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public static class Utilities
{
    public static void SetProperty(ref float variable, float value, string propertyName)
    {
        if (Math.Abs(variable - Constants.FloatBaseValue) > Constants.FloatComparisionPrecision)
        {
            throw new PropertyHasAlreadyBeenInitializedException(propertyName);
        }

        variable = value;
    }
    
    public static float GetProperty(float variable, string propertyName)
    {
        if (Math.Abs(variable - Constants.FloatBaseValue) < Constants.FloatComparisionPrecision)
        {
            throw new PropertyHasNotBeenInitializedException(propertyName);
        }

        return variable;
    }
    
    public static GameObject DrawLine(Vector2 startPoint, Vector2 endPoint)
    {
        //For creating line renderer object
        var line = new GameObject("Line");
        var lineRenderer = line.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        //For drawing line in the world space, provide the x,y,z values
        lineRenderer.SetPosition(0, startPoint); //x,y and z position of the starting point of the line
        lineRenderer.SetPosition(1, endPoint); //x,y and z position of the end point of the line
        return line;
    }
    
    public static Vector2 CalculatePosition(GameObject gameObjectToSpawn)
    {
        var objectSize = Vector3.Scale(gameObjectToSpawn.transform.localScale,
            gameObjectToSpawn.GetComponent<SpriteRenderer>().sprite.bounds.size);
        var position = new Vector2(Random.Range(-Constants.ScreenWidth+objectSize.x,
            Constants.ScreenWidth-objectSize.x), Random.Range(-Constants.ScreenHeight+objectSize.y,
            Constants.ScreenHeight-objectSize.y));
        var radius = gameObjectToSpawn.GetComponent<SpriteRenderer>().bounds.size.x/2;
        ObjectsOnSceneIntersectsPosition(position, radius);
        while (ObjectsOnSceneIntersectsPosition(position, radius))
        {
            position = new Vector2(Random.Range(-Constants.ScreenWidth+objectSize.x,
                Constants.ScreenWidth-objectSize.x), Random.Range(-Constants.ScreenHeight+objectSize.y,
                Constants.ScreenHeight-objectSize.y));
        }
        return position;
    }

    private static bool ObjectsOnSceneIntersectsPosition(Vector2 position, float radius)
    {
        var hitCollider = Physics2D.OverlapCircle(position, radius);
        return hitCollider != null;
    }
    
    public static bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
