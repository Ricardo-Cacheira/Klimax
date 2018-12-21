using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public Color deadColor = Color.black;
    public Color aliveColor = Color.white;
    public int width = 128;
    public int height = 128;
    public Vector2Int[] start;

    [Range(0, 1)]
    public float timeScale = 1;

    [Range(0, 20)]
    public int odds;

    Texture2D destTex;
    RenderTexture renderTexture;

    private MeshRenderer mesh;
    Color[] pixels;

    public bool[,] grid;

    int Index(int row, int column) => width * row + column;
    int Row(int index) => index / width;
    int Column(int index) => index % width;
    static int Mod(int i, int n) => (i % n + n) % n;

    void Start()
    {
        grid = new bool[height, width];
        pixels = new Color[width * height];
        destTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        mesh = GetComponent<MeshRenderer>();
        mesh.material.mainTexture = destTex;

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                pixels[Index(row, col)] = deadColor;
                grid[row, col] = false;

                if (odds > 0)
                {
                    int r = Random.Range(0, odds);
                    var color = (r == 0) ? aliveColor : deadColor;
                    pixels[Index(row, col)] = color;
                    grid[row, col] = (r == 0);
                }
            }
        }

        if (odds == 0) foreach (var cell in start)
        {
            pixels[Index(cell.y, cell.x)] = aliveColor;
            grid[cell.y, cell.x] = true;
        }

        destTex.SetPixels(pixels);
        destTex.Apply();
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }

    void FixedUpdate()
    {
        var current = new bool[height, width];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                var count = NumNeighbours(row, col);
                var alive = IsAlive(row, col);

                if (alive && count < 2)
                {
                    current[row, col] = false;
                }
                else if (alive && count > 3)
                {
                    current[row, col] = false;
                }
                else if (!alive && count == 3)
                {
                    current[row, col] = true;
                }
                else
                {
                    current[row, col] = alive;
                }
            }
        }

        for (int i = 0; i < current.Length; i++)
        {
            pixels[i] = current[Row(i), Column(i)] ? aliveColor : deadColor;
        }

        grid = current;

        destTex.SetPixels(pixels);
        destTex.Apply();
    }

    private int NumNeighbours(int row, int col)
    {
        var others = new bool[] {
            IsAlive(row - 1, col - 1),
            IsAlive(row - 1, col),
            IsAlive(row - 1, col + 1),
            IsAlive(row, col - 1),
            IsAlive(row, col + 1),
            IsAlive(row + 1, col - 1),
            IsAlive(row + 1, col),
            IsAlive(row + 1, col + 1),
        };

        int count = 0;
        foreach (bool p in others) if (p) count += 1;

        return count;
    }

    bool IsAlive(int row, int col)
    {
        var rowIndex = Mod(row, height);
        var colIndex = Mod(col, width);
        return grid[rowIndex, colIndex];
    }
}