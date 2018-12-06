using UnityEngine;

public class PanelHelper : MonoBehaviour
{
    /// <summary>
    /// Количество кнопок
    /// </summary>
    private const int Count = 9;

    // Создание кнопок
    void Awake()
    {
        for (int i = 0; i < Count; i++)
        {
            var button = Instantiate(Resources.Load("Button")) as GameObject;
            if (button != null)
                button.transform.SetParent(transform);
        }
    }
}
