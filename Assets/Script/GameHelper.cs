using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelper : MonoBehaviour
{
    /// <summary>
    /// Количкстов столбцов и строк
    /// </summary>
    private const int _count = 3;

    /// <summary>
    /// Диалоговое окно
    /// </summary>
    [SerializeField]
    private GameObject _messageDialog;

    /// <summary>
    /// Сообщение на диалоговом окне
    /// </summary>
    [SerializeField]
    private Text _message;

    /// <summary>
    /// 
    /// </summary>
    [SerializeField]
    private Text _titl;

    /// <summary>
    /// Кто ходит
    /// </summary>
    [SerializeField]
    private Text _titlTicTacToe;
    private Enums.TicTacToe _ticTacToe;
    private List<ButtonHelper> _buttonHelpers;
    private PlayerHelper _playerHelper;


    public Enums.TicTacToe TicTacToe
    {
        get { return _ticTacToe; }
        private set
        {
            _ticTacToe = value;
            _titlTicTacToe.text = "Next - " + _ticTacToe.ToString();
        }
    }
        
    public PlayerHelper PlayerHelper
    {
        get { return _playerHelper; }
        set
        {
            _playerHelper = value;
            _titl.text = "You - " + _playerHelper.TicTacToe.ToString();
        }
    }

    public Stack<Enums.TicTacToe> TicTacToeStack;

    // Use this for initialization
    void Start()
    {
        _messageDialog.SetActive(false);
        TicTacToe = Enums.TicTacToe.X;
        TicTacToeStack = new Stack<Enums.TicTacToe>();
        for (int i = 0; i < 2; i++)
        {
            TicTacToeStack.Push((Enums.TicTacToe)i);
        }

        _buttonHelpers = new List<ButtonHelper>();
        foreach (var button in FindObjectsOfType<ButtonHelper>())
        {
            _buttonHelpers.Add(button);
            button.OnActive += Click;
        }
    }

    public void Click(ButtonHelper button)
    {
        if (PlayerHelper == null || !string.IsNullOrEmpty(button.Text))
            return;

        PlayerHelper.CmdClick(_buttonHelpers.IndexOf(button));
    }

    public void SetTicTacToe(Enums.TicTacToe tic, int n)
    {
        if (tic != TicTacToe)
            return;

        _buttonHelpers[n].Text = tic.ToString();

        if (Check())
        {
            switch (tic)
            {
                case Enums.TicTacToe.O:
                    ResultHelper.WinO++;
                    break;
                case Enums.TicTacToe.X:
                    ResultHelper.WinX++;
                    break;
            }
            PlayerHelper.Win(tic);
            return;
        }

        if (_buttonHelpers.Find(b => string.IsNullOrEmpty(b.Text)) == null)
        {
            ResultHelper.Draw++;
            PlayerHelper.Win(null);
            return;
        }

        switch (TicTacToe)
        {
            case Enums.TicTacToe.O:
                TicTacToe = Enums.TicTacToe.X;
                break;
            case Enums.TicTacToe.X:
                TicTacToe = Enums.TicTacToe.O;
                break;
        }
    }

    /// <summary>
    /// Проверка три вряд
    /// </summary>
    /// <returns></returns>
    private bool Check()
    {
        var ch = true;

        for (int i = 0; i < _count; i++)
        {
            ch = true;
            for (int j = 1; j < _count; j++)
            {

                if (_buttonHelpers[i * _count].Text != _buttonHelpers[i * _count + j].Text ||
                    string.IsNullOrEmpty(_buttonHelpers[i * _count + 0].Text) ||
                    string.IsNullOrEmpty(_buttonHelpers[i * _count + j].Text))
                {
                    ch = false;
                    break;
                }
            }
            if (ch)
            {
                return true;
            }
        }

        for (int i = 0; i < _count; i++)
        {
            ch = true;
            for (int j = 1; j < _count; j++)
            {
                if (_buttonHelpers[i].Text != _buttonHelpers[j * _count + i].Text ||
                    string.IsNullOrEmpty(_buttonHelpers[i].Text) ||
                    string.IsNullOrEmpty(_buttonHelpers[j * _count + i].Text))
                {
                    ch = false;
                    break;
                }
            }

            if (ch)
            {
                return true;
            }
        }
        ch = true;
        for (int i = 1; i < _count; i++)
        {

            if (_buttonHelpers[0].Text != _buttonHelpers[i * _count + i].Text ||
                string.IsNullOrEmpty(_buttonHelpers[0].Text) ||
                string.IsNullOrEmpty(_buttonHelpers[i * _count + i].Text))
            {
                ch = false;
                break;
            }
        }

        if (ch)
        {
            return true;
        }

        ch = true;
        for (int i = _count - 2; i >= 0; i--)
        {
            if (_buttonHelpers[_count - 1].Text != _buttonHelpers[_count * (_count - i - 1) + i].Text ||
                string.IsNullOrEmpty(_buttonHelpers[_count - 1].Text) ||
                string.IsNullOrEmpty(_buttonHelpers[_count * (_count - i - 1) + i].Text))
            {
                ch = false;
                break;
            }
        }

        if (ch)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Показать диалоговое окно
    /// </summary>
    /// <param name="win">состояние</param>
    public void OpenWin(bool? win)
    {
        if (win == null)
        {
            _message.text = "Draw";
        }
        else if ((bool)win)
        {
            _message.text = "Win";
        }
        else
        {
            _message.text = "Lost";
        }

        _messageDialog.SetActive(true);
    }
    
    /// <summary>
    /// Рестрат для всех клиентов
    /// </summary>
    public void Restart()
    {
        PlayerHelper.CmdRes();
    }

    /// <summary>
    /// Ресет поля
    /// </summary>
    public void SetReset()
    {
        foreach (var but in _buttonHelpers)
        {
            but.Text = string.Empty;
        }

        _messageDialog.SetActive(false);

        switch (TicTacToe)
        {
            case Enums.TicTacToe.O:
                TicTacToe = Enums.TicTacToe.X;
                break;
            case Enums.TicTacToe.X:
                TicTacToe = Enums.TicTacToe.O;
                break;
        }

    }

    public void Exit()
    {
        Application.Quit();
    }
}
