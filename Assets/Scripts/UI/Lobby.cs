using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lobby : NetworkBehaviour
{
    [SerializeField] private NetworkManagerArena _networkManager;
    [SerializeField] private Color[] _playerColors;
    [SerializeField] private string[] _playerNames;
    [SerializeField] private PlayerInfoView _view;
    [SerializeField] private Transform _viewsContainer;
    [SerializeField] private GameObject _screen;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private float _winnerTextDuration;
    [SerializeField] private int _recolorsToWin;

    private List<Player> _players = new List<Player>();
    private Dictionary<Player, PlayerInfoView> _viewsByPlayer = new Dictionary<Player, PlayerInfoView>();
    private Dictionary<Player, int> _countByPlayer = new Dictionary<Player, int>();

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

    public void StartSession()
    {
        foreach (var player in _players)
            _countByPlayer[player] = 0;

        _spawner.Spawn(_players.ToArray());
    }

    public override void OnStartServer()
    {
        _screen.SetActive(true);
    }

    public override void OnStopServer()
    {
        _players.Clear();
        _viewsByPlayer.Clear();
        _countByPlayer.Clear();
        _winnerText.gameObject.SetActive(false);
    }

    private void OnPlayerConnected(NetworkConnectionToClient connection)
    {
        if (isServer == false)
            return;

        foreach (var owned in connection.owned)
        {
            if (owned.TryGetComponent(out Player player))
            {
                InitPlayer(player);
                InitPlayerInfoView(player, _viewsContainer);
                _countByPlayer[player] = 0;
                player.RecoloredByPlayer += OnPlayerRecolored;
            }
        }
    }

    private void InitPlayer(Player player)
    {
        _players.Add(player);
        player.Init(_playerNames[_players.Count - 1], _playerColors[_players.Count - 1]);
    }

    private void InitPlayerInfoView(Player player, Transform container)
    {
        var view = Instantiate(_view, container);
        _viewsByPlayer[player] = view;
        view.Init(player);
        NetworkServer.Spawn(view.gameObject);
    }

    private void OnPlayerDisconnected(NetworkConnectionToClient connection)
    {
        if (isServer == false)
            return;

        foreach (var owned in connection.owned)
        {
            if (owned.TryGetComponent(out Player player))
            {
                if (_players.Contains(player))
                    _players.Remove(player);

                if (_viewsByPlayer.TryGetValue(player, out PlayerInfoView view))
                {
                    _viewsByPlayer.Remove(player);
                    Destroy(view.gameObject);
                }

                if (_countByPlayer.ContainsKey(player))
                    _countByPlayer.Remove(player);

                player.RecoloredByPlayer -= OnPlayerRecolored;
            }
        }
    }

    private void OnPlayerRecolored(Player recolorer)
    {
        _countByPlayer[recolorer]++;

        if (_countByPlayer[recolorer] >= _recolorsToWin)
            StartCoroutine(ShowWinnerText(recolorer.Name, _winnerTextDuration));
    }

    private IEnumerator ShowWinnerText(string winnerName, float duration, string afterWinnerNameText = " wins!")
    {
        _winnerText.gameObject.SetActive(true);
        _winnerText.text = winnerName + afterWinnerNameText;

        yield return new WaitForSecondsRealtime(duration);

        _winnerText.gameObject.SetActive(false);
        StartSession();
    }
}
