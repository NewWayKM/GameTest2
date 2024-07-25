using UnityEngine;

public class Grid : MonoBehaviour
{
    public static int width = 8;
    public static int height = 12;
    public static Transform[,] grid = new Transform[width, height];

    public static Vector2 roundVec2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }

    public static void deleteRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            Destroy(grid[x, y]?.gameObject);
            grid[x, y] = null;
        }
    }

    public static void decreaseRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void decreaseRowsAbove(int y)
    {
        for (int i = y; i < height; ++i)
        {
            decreaseRow(i);
        }
    }

    public static bool isRowFull(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void deleteFullRows()
    {
        for (int y = 0; y < height; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    public static void clearSameColor(Vector2 pos, Color color)
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                if (grid[x, y] != null && grid[x, y].GetComponent<Renderer>().material.color == color)
                {
                    Destroy(grid[x, y].gameObject);
                    grid[x, y] = null;
                }
            }
        }
    }

    public static void clearConnected(Vector2 pos, Color color)
    {
        bool[,] visited = new bool[width, height];
        ClearConnectedHelper((int)pos.x, (int)pos.y, color, visited);
    }

    private static void ClearConnectedHelper(int x, int y, Color color, bool[,] visited)
    {
        if (x < 0 || x >= width || y < 0 || y >= height || visited[x, y] || grid[x, y] == null)
            return;

        if (grid[x, y].GetComponent<Renderer>().material.color != color)
            return;

        visited[x, y] = true;
        Destroy(grid[x, y].gameObject);
        grid[x, y] = null;

        ClearConnectedHelper(x + 1, y, color, visited);
        ClearConnectedHelper(x - 1, y, color, visited);
        ClearConnectedHelper(x, y + 1, color, visited);
        ClearConnectedHelper(x, y - 1, color, visited);
    }
}