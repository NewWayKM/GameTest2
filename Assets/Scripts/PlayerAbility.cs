using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateAbility();
        }
    }

    void ActivateAbility()
    {
        // Заглушка для способностей персонажей
        // Реализация будет добавлена позже
    }
}