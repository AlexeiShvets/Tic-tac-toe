using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHelper : NetworkBehaviour
{
    private GameHelper _gameHelper;
    public Enums.TicTacToe TicTacToe;

    // Use this for initialization
    void Start()
    {
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();
        if (_gameHelper.TicTacToeStack.Count == 0)
        {
            return;
        }
        var tic = _gameHelper.TicTacToeStack.Pop();
        TicTacToe = tic;

        if (isLocalPlayer)
        {            
            _gameHelper.PlayerHelper = this;
        }
    }
        
    [Command]
    public void CmdClick(int n)
    {
        RpcClick(n);
    }

    [ClientRpc]
    public void RpcClick(int n)
    {
        _gameHelper.SetTicTacToe(TicTacToe, n);
    }

    public void Win(Enums.TicTacToe? n)
    {
        if (n == null)
        {
            _gameHelper.OpenWin(null);
            return;
        }
        _gameHelper.OpenWin(n == TicTacToe);
    }

    [Command]
    public void CmdRes()
    {
        RpcRes();
    }

    [ClientRpc]
    public void RpcRes()
    {
        _gameHelper.SetReset();
    }
}
