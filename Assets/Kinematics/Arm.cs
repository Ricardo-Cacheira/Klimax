using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector2 xy(this Vector3 target)
    {
        return new Vector2(target.x, target.y);
    }

    public static Vector2 xyz(this Vector2 target, int z)
    {
        return new Vector3(target.x, target.y,z);
    }
}


public class Arm : MonoBehaviour
{
    public List<Segment> segments = new List<Segment>();
    public Transform anchor;


    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        segments[0].Follow(mousePosition);
        for (int i = 1; i < segments.Count; i++)
        {
            segments[i].Follow(segments[i - 1].startPoint);
        }

        // segments[segments.Count - 1].Follow(anchor.position.xy());
        // for (int i = segments.Count - 2; i >= 0; i--)
        // {
        //     segments[i].Follow(segments[i + 1].startPoint);
        // }
    }
}
