using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    public Material deadMaterial;
    public Material aliveMaterial;
    public GameObject prefab;

    public bool[,] grid;
    public GameObject[,] quads;

    public int width = 64;
    public int height = 64;
    public Vector2Int[] start;

    [Range(0, 1)]
    public float timeScale = 1;

    [Range(0,20)]
    public int odds = 0;

    void Start()
    {
        quads = new GameObject[height, width];
        grid = new bool[height, width];
        
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                var obj = Instantiate(prefab, new Vector3(row - width / 2, col - height / 2 , 0), Quaternion.identity);
                obj.GetComponent<MeshRenderer>().material = deadMaterial;
                quads[row, col] = obj;

                if(odds > 0)
                {
                    int r = Random.Range(0, odds);
                    var material = (r == 0) ? aliveMaterial : deadMaterial;
                    obj.GetComponent<MeshRenderer>().material = material;
                    grid[row, col] = (r == 0);
                }
            }
        }

        if (odds == 0) foreach (var cell in start)
        {
            grid[cell.x, cell.y] = true;
        }
    }

    private void Update()
    {
        Time.timeScale = timeScale;
    }

    void FixedUpdate()
    {
        var current = grid.Clone() as bool[,];

        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                var count = NumNeighbours(row, col);
                bool alive = grid[row, col];

                if (alive && count < 2)
                {
                    current[row, col] = false;
                }
                else if (alive && (count == 2 || count == 3))
                {
                    current[row, col] = true;
                }
                else if (alive && count > 3)
                {
                    current[row, col] = false;
                }
                else if (!alive && count == 3)
                {
                    current[row, col] = true;
                }

                quads[row, col].GetComponent<MeshRenderer>().material = (current[row, col]) ? aliveMaterial : deadMaterial;
            }
        }

        grid = current;
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

    static int Mod(int i, int n)
    {
        return (i % n + n) % n;
    }
}