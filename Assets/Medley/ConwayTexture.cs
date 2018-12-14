using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ConwayTexture : MonoBehaviour {
    Texture2D destTex;
    Color[] pixels;

    const int width = 64;
    const int height = 64;

	// Use this for initialization
	void Start () {
        pixels = new Color[width * height];
        destTex = new Texture2D(width, height);
        GetComponent<MeshRenderer>().material.mainTexture = destTex;
    }

    private int NumNeighbours(int index)
    {
        var row = Row(index);
        var col = Column(index);

        if (row == 0 || row == height - 1 || col == 0 || col == width - 1)
        {
            return 0;
        }

        var others = new Color[] {
            pixels[index - width - 1],
            pixels[index - width],
            pixels[index - width + 1],
            pixels[index - 1],
            pixels[index + 1],
            pixels[index + width - 1],
            pixels[index + width],
            pixels[index + width + 1],
        };

        int count = 0;
        foreach (var p in others)
        {
            if (p.a == 1) count += 1;
        }

        return count;
    }

    int Index(int row, int column)
    {
        return 64 * row + column;
    }

    int Row(int index)
    {
        return index % 64;
    }

    int Column(int index)
    {
        return index / 64;
    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < pixels.Length; i++)
        {
            var count = NumNeighbours(i);
            bool alive = (pixels[i].a == 1);

            if (alive && count < 2)
            {
                pixels[i].a = 0;
            }
            else if (alive && (count == 2 || count == 3))
            {
                pixels[i].a = 1;
            }
            else if (alive && count > 3)
            {
                pixels[i].a = 0;
            }
            else if (!alive && count == 3)
            {
                pixels[i].a = 1;
            }
        }

        destTex.SetPixels(pixels);
        destTex.Apply();
    }
}
