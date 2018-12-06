using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonHelper : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Text _text;
    /// <summary>
    /// Текст на кнопке О\Х
    /// </summary>
    public string Text
    {
        get { return _text.text; }
        set { _text.text = value; }
    }

    /// <summary>
    /// Событие клика по кпоке
    /// </summary>
    public Action<ButtonHelper> OnActive { get; internal set; }      

    /// <summary>
    /// Клик по копке
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnActive!=null)
            OnActive(this);
    }
}
