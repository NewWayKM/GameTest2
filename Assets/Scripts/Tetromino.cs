using UnityEngine;

public class Tetromino : MonoBehaviour
{
    float fall = 0;
    public float fallSpeed = 1;
    public bool isBomb = false;
    public bool isCircle = false;
    public Color circleColor;

    void Update()
    {
        CheckUserInput();
    }

    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            MoveDown();
        }
    }

    void MoveLeft()
    {
        transform.position += new Vector3(-1, 0, 0);

        if (!ValidMove())
        {
            transform.position += new Vector3(1, 0, 0);
        }
    }

    void MoveRight()
    {
        transform.position += new Vector3(1, 0, 0);

        if (!ValidMove())
        {
            transform.position += new Vector3(-1, 0, 0);
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 0, -90);

        if (!ValidMove())
        {
            transform.Rotate(0, 0, 90);
        }
    }

    void MoveDown()
    {
        transform.position += new Vector3(0, -1, 0);

        if (!ValidMove())
        {
            transform.position += new Vector3(0, 1, 0);
            OnStop();
            enabled = false;
        }

        fall = Time.time;
    }

    bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            if (!Grid.insideBorder(v))
                return false;

            if (Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void OnStop()
    {
        if (isBomb)
        {
            foreach (Transform child in transform)
            {
                Grid.clearSameColor(child.position, child.GetComponent<Renderer>().material.color);
            }
        }
        else if (isCircle)
        {
            foreach (Transform child in transform)
            {
                Grid.clearConnected(child.position, circleColor);
            }
        }

        Grid.deleteFullRows();
        FindObjectOfType<GameManager>().SpawnNextTetromino();
    }
}