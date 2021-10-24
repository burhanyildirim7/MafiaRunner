using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController _uiController;

    public static bool _oyunAktif;

    public static bool _oyunuBeklet;

    private PlayerController _playerController;
    
    void Start()
    {
        _oyunAktif = false;
        _oyunuBeklet = false;
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if (!_oyunAktif)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _oyunAktif = true;
                _uiController.TaptoStartPanelClose();
                _playerController.Karakter1Walk();
                KarakterPaketiMovement._karakteriDurdur = false;
            }
            else
            {

            }
        }
        else
        {

        }
    }
}
