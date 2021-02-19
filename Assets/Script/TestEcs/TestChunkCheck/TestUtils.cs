using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public static class TestUtils
{
    #region MOUSE
    // Get Mouse Position in World with Z = 0f
    public static float3 GetMouseWorldPosition()
    {
        float3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.y = 0f;
        return vec;
    }
    public static float3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static float3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static float3 GetMouseWorldPositionWithZ(float3 screenPosition, Camera worldCamera)
    {
        float3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }


    public static bool Intersect(AABB box, Ray ray)
    {
        double tx1 = (box.Min.x - ray.origin.x) * (1 / ray.direction.x);
        double tx2 = (box.Max.x - ray.origin.x) * (1 / ray.direction.x);

        double tmin = math.min(tx1, tx2);
        double tmax = math.max(tx1, tx2);

        double ty1 = (box.Min.y - ray.origin.y) * (1 / ray.direction.y);
        double ty2 = (box.Max.y - ray.origin.y) * (1 / ray.direction.y);

        tmin = math.max(tmin, math.min(ty1, ty2));
        tmax = math.min(tmax, math.max(ty1, ty2));

        double tz1 = (box.Min.z - ray.origin.z) * (1 / ray.direction.z);
        double tz2 = (box.Max.z - ray.origin.z) * (1 / ray.direction.z);

        tmin = math.max(tmin, math.min(tz1, tz2));
        tmax = math.min(tmax, math.max(tz1, tz2));

        return tmax >= tmin;
    }

    public static bool Intersect(AABB box1, AABB box2)
    {
        return (box1.Min.x <= box2.Max.x && box1.Max.x >= box2.Min.x) &&
               (box1.Min.y <= box2.Max.y && box1.Max.y >= box2.Min.y) &&
               (box1.Min.z <= box2.Max.z && box1.Max.z >= box2.Min.z);
    }
    #endregion MOUSE

    #region Rectangle
    //===================================================================
    //RECTANGLE PART
    //===================================================================
    static Texture2D _whiteTexture;
    public static Texture2D WhiteTexture
    {
        get
        {
            if (_whiteTexture == null)
            {
                _whiteTexture = new Texture2D(1, 1);
                _whiteTexture.SetPixel(0, 0, Color.white);
                _whiteTexture.Apply();
            }

            return _whiteTexture;
        }
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, WhiteTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        TestUtils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        TestUtils.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        TestUtils.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        TestUtils.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }
    #endregion Rectangle
}
