using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public GameObject[] blocks; // ������ ���� ������

    void Start()
    {
        player1.SpawnNextTetromino();
        player2.SpawnNextTetromino();
    }

    public void SpawnNextTetromino()
    {
        player1.SpawnNextTetromino();
        player2.SpawnNextTetromino();
    }

    void Update()
    {
        // ������ ���������� ����� � �������� ������� ������/���������
    }

    public void SendBlocksToOpponent(int count, Grid opponentGrid)
    {
        for (int i = 0; i < count; i++)
        {
            int x = Random.Range(0, Grid.width);
            int y = Grid.height - 1;
            while (Grid.grid[x, y] != null)  // ������������� ������������ ������� ����� ��� ���� Grid
            {
                x = Random.Range(0, Grid.width);
            }
            GameObject block = Instantiate(blocks[Random.Range(0, blocks.Length)], new Vector3(x, y, 0), Quaternion.identity);
            Grid.grid[x, y] = block.transform;  // ������������� ������������ ������� ����� ��� ���� Grid
            block.GetComponent<Tetromino>().fallSpeed = 0.5f; // ��������� �������� �������
        }
    }
}