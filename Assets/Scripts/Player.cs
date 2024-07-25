using UnityEngine;

public class Player : MonoBehaviour
{
    public Grid playerGrid;
    public Grid opponentGrid;
    public GameObject[] tetrominos;
    private GameObject currentTetromino;

    void Start()
    {
        SpawnNextTetromino();
    }

    public void SpawnNextTetromino()
    {
        int index = Random.Range(0, tetrominos.Length);
        currentTetromino = Instantiate(tetrominos[index], transform.position, Quaternion.identity);
        currentTetromino.GetComponent<Tetromino>().fallSpeed = 1; // ”становка скорости падени€
    }

    void Update()
    {
        // ƒополнительна€ логика управлени€ игроком, если потребуетс€
    }
}