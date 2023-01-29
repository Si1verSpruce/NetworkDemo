using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : NetworkBehaviour
{
    [SerializeField] private NetworkManagerArena _networkManager;
    [SerializeField] private Color[] _playerColors;
    [SerializeField] private string[] _playerNames;
    [SerializeField] private PlayerInfoView _view;
    [SerializeField] private Transform _viewsContainer;

    private List<Player> _players = new List<Player>();
    private Dictionary<Player, PlayerInfoView> _views = new Dictionary<Player, PlayerInfoView>();

    private void OnEnable()
    {
        _networkManager.PlayerConnected += OnPlayerConnected;
        _networkManager.PlayerDisconnected += OnPlayerDisconnected;
    }

    private void OnDisable()
    {
        _networkManager.PlayerConnected -= OnPlayerConnected;
        _networkManager.PlayerDisconnected -= OnPlayerDisconnected;
    }

    private void OnPlayerConnected(NetworkConnectionToClient connection)
    {
        foreach (var owned in connection.owned)
        {
            if (owned.TryGetComponent(out Player player))
            {
                InitPlayer(player);
                InitPlayerInfoView(player);
            }
        }
    }

    private void InitPlayer(Player player)
    {
        _players.Add(player);
        player.Init(_playerNames[_players.Count - 1], _playerColors[_players.Count - 1]);
    }

    private void InitPlayerInfoView(Player player)
    {
        var view = Instantiate(_view, _viewsContainer);
        _views[player] = view;
        view.Init(player);
    }

    private void OnPlayerDisconnected(NetworkConnectionToClient connection)
    {
        foreach (var owned in connection.owned)
        {
            if (owned.TryGetComponent(out Player player))
            {
                if (_players.Contains(player))
                    _players.Remove(player);

                if (_views.TryGetValue(player, out PlayerInfoView view))
                {
                    _views.Remove(player);
                    Destroy(view.gameObject);
                }
            }
        }
    }
}
