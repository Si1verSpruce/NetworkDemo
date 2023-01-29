using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _colorView;

    public void Init(Player player)
    {
        _name.text = player.Name;
        _colorView.color = player.ThisColor;
    }
}
