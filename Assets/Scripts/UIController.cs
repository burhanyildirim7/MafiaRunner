using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _tapToStartPanel;
    [SerializeField] private GameObject _gameScreenPanel;
    [SerializeField] private GameObject _winScreenPanel;
    [SerializeField] private GameObject _loseScreenPanel;

    [SerializeField] private Text _levelText;
    [SerializeField] private Text _elmasText;
    [SerializeField] private Text _winElmasText;
    [SerializeField] private Text _loseElmasText;
    [SerializeField] private Text _tapToStartElmasText;

    private int _levelNumber;

    private int _elmasSayisi;

    private PlayerController _playerController;

    private LevelController _levelController;

    private int _levelSonuElmasSayisi;



    void Start()
    {
        _tapToStartPanel.SetActive(true);

        _levelNumber = PlayerPrefs.GetInt("LevelNumber");

        if (_levelNumber == 0)
        {
            _levelNumber = 1;
            PlayerPrefs.SetInt("LevelNumber", _levelNumber);
        }
        else
        {

        }

        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayısı");

        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        _levelController = GameObject.Find("LevelController").GetComponent<LevelController>();

        _tapToStartElmasText.text = _elmasSayisi.ToString();


    }

  
    void Update()
    {
        _levelNumber = PlayerPrefs.GetInt("LevelNumber");

        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayısı");

       
        _levelText.text = "LEVEL " + (_levelNumber);
        
        
        _elmasText.text = _elmasSayisi.ToString();
    }

    public void TaptoStartPanelClose()
    {
        _tapToStartPanel.SetActive(false);
        _gameScreenPanel.SetActive(true);
    }

    public void WinScreenPanelOpen()
    {
        _gameScreenPanel.SetActive(false);
        _winScreenPanel.SetActive(true);
        _winElmasText.text = _levelSonuElmasSayisi.ToString();
    }

    public void LoseScreenPanelOpen()
    {
        _gameScreenPanel.SetActive(false);
        _loseScreenPanel.SetActive(true);
        _loseElmasText.text = _levelSonuElmasSayisi.ToString();
    }

    public void TapToStartPanelOpen()
    {
        GameController._oyunAktif = false;
        GameController._oyunuBeklet = false;
        _winScreenPanel.SetActive(false);
        _loseScreenPanel.SetActive(false);
        _tapToStartPanel.SetActive(true);
        _elmasSayisi = _elmasSayisi + _levelSonuElmasSayisi;
        PlayerPrefs.SetInt("ElmasSayısı", _elmasSayisi);
        _tapToStartElmasText.text = _elmasSayisi.ToString();
        _levelController.LevelDegistir();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _playerController.LevelStart();
    }

    public void LevelSonuElmasSayisi(int deger)
    {
        _levelSonuElmasSayisi = deger;
    }

    public void LevelRestartButton()
    {
        GameController._oyunAktif = false;
        GameController._oyunuBeklet = false;
        _winScreenPanel.SetActive(false);
        _loseScreenPanel.SetActive(false);
        _tapToStartPanel.SetActive(true);
        _elmasSayisi = _elmasSayisi + _levelSonuElmasSayisi;
        PlayerPrefs.SetInt("ElmasSayısı", _elmasSayisi);
        _tapToStartElmasText.text = _elmasSayisi.ToString();
        _levelController.LevelRestart();
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _playerController.LevelStart();
    }

}
