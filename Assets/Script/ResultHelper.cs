using UnityEngine;
using UnityEngine.UI;

public class ResultHelper : MonoBehaviour
{
    /// <summary>
    /// Текст статистики
    /// </summary>
    [SerializeField]
    private Text _text;

    /// <summary>
    /// Победы x
    /// </summary>
    public static int WinX = 0; 

    /// <summary>
    /// Победы О
    /// </summary>
    public static int WinO = 0;

    /// <summary>
    /// Ничьи
    /// </summary>
    public static int Draw = 0;
           

    // Update is called once per frame
    void Update ()
    {
        _text.text = string.Format("Win X={0} \nWin O={1} \nDraw={2}", WinX, WinO, Draw);

    }
}
