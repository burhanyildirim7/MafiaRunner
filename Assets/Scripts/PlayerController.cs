using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private int _iyiToplanabilirDeger;

    [SerializeField] private int _kötüToplanabilirDeger;

    [SerializeField] private int _mafiaDuvarDeger;

    [SerializeField] private int _kilibikDuvarDeger;

    [SerializeField] private List<GameObject> _karakterler = new List<GameObject>();

    [SerializeField] private KarakterPaketiMovement _karakterPaketiMovement;

    [SerializeField] private Text _playerUstuLevelText;

    [SerializeField] private GameObject _karakterPaketi;
    //[SerializeField] Animator karakter1anim;

    [SerializeField] private GameObject _artiBirObje;

    [SerializeField] private Text _artiBirText;

    [SerializeField] private Animator _bossArkasiBirinciKarakter;
    [SerializeField] private Animator _bossArkasiIkinciKarakter;


    private int _playerScore;

    private int _elmasSayisi;

    private Animator _karakterAnimator;

    private int _karakterSeviyesi;

    private GameObject _player;

    private UIController _uiController;

    private int _toplananElmasSayisi;



    void Start()
    {
        LevelStart();

        _uiController = GameObject.Find("UIController").GetComponent<UIController>();

        _playerUstuLevelText.color = Color.red;

        _artiBirObje.SetActive(false);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "İyiToplanabilir")
        {
            int levelsinirsayisi = _playerScore + _iyiToplanabilirDeger;

            _artiBirText.text = "+2";
            _artiBirText.color = Color.green;
            _artiBirObje.SetActive(true);
            Invoke("ArtiBirTextKapat", 0.5f);



            if (levelsinirsayisi >= 99)
            {
                _playerScore = 99;
            }
            else
            {
                _playerScore += _iyiToplanabilirDeger;
            }

            Destroy(other.gameObject);

            KarakterAyarlama();

            Debug.Log("Player Score = " + _playerScore);
        }
        else if (other.tag == "KötüToplanabilir")
        {
            _artiBirText.text = "-2";
            _artiBirText.color = Color.red;
            _artiBirObje.SetActive(true);
            Invoke("ArtiBirTextKapat", 0.5f);

            if (_playerScore > _kötüToplanabilirDeger)
            {
                _playerScore -= _kötüToplanabilirDeger;
                Destroy(other.gameObject);

                KarakterAyarlama();

                Debug.Log("Player Score = " + _playerScore);
            }
            else
            {
                _playerScore = 1;
                Destroy(other.gameObject);

                GameController._oyunuBeklet = true;
                KarakterPaketiMovement._karakteriDurdur = true;

                _karakterAnimator.SetBool("Victory", true);
                _uiController.LevelSonuElmasSayisi(_toplananElmasSayisi * _playerScore);
                Invoke("LoseScreenAc", 2.5f);

                //KarakterAyarlama();

                Debug.Log("Player Score = " + _playerScore);
            }

        }
        else if (other.tag == "MafiaDuvar")
        {
            int levelsinirsayisi = _playerScore + _mafiaDuvarDeger;

            _artiBirText.text = "+12";
            _artiBirText.color = Color.green;
            _artiBirObje.SetActive(true);
            Invoke("ArtiBirTextKapat", 0.5f);

            if (levelsinirsayisi >= 99)
            {
                _playerScore = 99;
            }
            else
            {
                _playerScore += _mafiaDuvarDeger;
            }
            KarakterAyarlama();

            Debug.Log("Player Score = " + _playerScore);

        }
        else if (other.tag == "KılıbıkDuvar")
        {
            _artiBirText.text = "-12";
            _artiBirText.color = Color.red;
            _artiBirObje.SetActive(true);
            Invoke("ArtiBirTextKapat", 0.5f);

            if (_playerScore > _kilibikDuvarDeger)
            {
                _playerScore -= _kilibikDuvarDeger;

                KarakterAyarlama();

                Debug.Log("Player Score = " + _playerScore);
            }
            else
            {
                _playerScore = 1;
                Destroy(other.gameObject);

                GameController._oyunuBeklet = true;
                KarakterPaketiMovement._karakteriDurdur = true;

                _karakterAnimator.SetBool("Victory", true);
                _uiController.LevelSonuElmasSayisi(_toplananElmasSayisi * _playerScore);
                Invoke("LoseScreenAc", 2.5f);

                // KarakterAyarlama();

                Debug.Log("Player Score = " + _playerScore);
            }

        }
        else if (other.tag == "DuvarPaketi")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "Elmas")
        {
            _elmasSayisi += 1;
            _toplananElmasSayisi += 1;
            PlayerPrefs.SetInt("ElmasSayısı", _elmasSayisi);
            Destroy(other.gameObject);

            Debug.Log("Elmas Sayısı = " + _elmasSayisi);
        }
        else if (other.tag == "OyunSonuDuvar")
        {
            GameController._oyunuBeklet = true;

            _karakterAnimator = GameObject.FindWithTag("Karakter").GetComponent<Animator>();
            transform.localPosition = new Vector3(0, 1, 0);

            OyunSonuToplulukControl oyunSonuToplulukControl = GameObject.FindGameObjectWithTag("OyunSonuTopluluk").GetComponent<OyunSonuToplulukControl>();

            if (_karakterSeviyesi == 1)
            {
                oyunSonuToplulukControl.UzulmeAnimasyonuBaslat();
            }
            else
            {
                oyunSonuToplulukControl.SevninmeAnimasyonuBaslat();
            }

            Debug.Log(_karakterAnimator);


        }
        else if (other.tag == "SevinmeNoktasi")
        {

            KarakterPaketiMovement._karakteriDurdur = true;

            if (_karakterSeviyesi == 1)
            {
                _karakterAnimator.SetBool("Idle", false);
                _karakterAnimator.SetBool("Walk", false);
                _karakterAnimator.SetBool("Victory", true);
                _uiController.LevelSonuElmasSayisi(_toplananElmasSayisi * _playerScore);
                Invoke("WinScreenAc", 2.5f);
            }
            else
            {
                //_karakterAnimator.SetBool("Walk", false);
                _karakterAnimator.SetBool("Victory", true);

                if (_karakterSeviyesi == 6)
                {
                    _bossArkasiBirinciKarakter.SetBool("Victory", true);
                    _bossArkasiIkinciKarakter.SetBool("Victory", true);
                }
                else
                {

                }

                _uiController.LevelSonuElmasSayisi(_toplananElmasSayisi * _playerScore);
                Invoke("WinScreenAc", 2.5f);
                Debug.Log("Tamamlandı");
            }
        }
        else
        {

        }
    }

    private void WinScreenAc()
    {
        _uiController.WinScreenPanelOpen();
    }

    private void LoseScreenAc()
    {
        _uiController.LoseScreenPanelOpen();
    }

    public void Karakter1Walk()
    {
        _karakterAnimator = GameObject.FindWithTag("Karakter").GetComponent<Animator>();
        _karakterAnimator.SetBool("Idle", false);
        _karakterAnimator.SetBool("Walk", true);
    }

    private void ArtiBirTextKapat()
    {
        _artiBirObje.SetActive(false);
    }


    private void KarakterAyarlama()
    {
        if (_playerScore < 15)
        {
            _karakterSeviyesi = 1;
            _playerUstuLevelText.color = Color.red;
            _playerUstuLevelText.text = "Lv. " + _playerScore;
            _karakterler[0].SetActive(true);
            _karakterler[1].SetActive(false);
            _karakterler[2].SetActive(false);
            _karakterler[3].SetActive(false);
            _karakterler[4].SetActive(false);
            _karakterler[5].SetActive(false);
            _karakterPaketiMovement.Karakter1Hizi();
            Karakter1Walk();
        }
        else if (_playerScore >= 15 && _playerScore < 30)
        {
            if (_karakterSeviyesi != 2)
            {
                //_karakterPaketiMovement.Bekle();
                _karakterSeviyesi = 2;
            }
            else
            {

            }

            _playerUstuLevelText.color = Color.magenta;
            _playerUstuLevelText.text = "Lv. " + _playerScore;
            _karakterler[0].SetActive(false);
            _karakterler[1].SetActive(true);
            _karakterler[2].SetActive(false);
            _karakterler[3].SetActive(false);
            _karakterler[4].SetActive(false);
            _karakterler[5].SetActive(false);
            _karakterPaketiMovement.Karakter2Hizi();
        }
        else if (_playerScore >= 30 && _playerScore < 50)
        {
            if (_karakterSeviyesi != 3)
            {
                //_karakterPaketiMovement.Bekle();
                _karakterSeviyesi = 3;
            }
            else
            {

            }

            _playerUstuLevelText.color = Color.yellow;
            _playerUstuLevelText.text = "Lv. " + _playerScore;
            _karakterler[0].SetActive(false);
            _karakterler[1].SetActive(false);
            _karakterler[2].SetActive(true);
            _karakterler[3].SetActive(false);
            _karakterler[4].SetActive(false);
            _karakterler[5].SetActive(false);
            _karakterPaketiMovement.Karakter3Hizi();
        }
        else if (_playerScore >= 50 && _playerScore < 70)
        {
            if (_karakterSeviyesi != 4)
            {
                //_karakterPaketiMovement.Bekle();
                _karakterSeviyesi = 4;
            }
            else
            {

            }

            _playerUstuLevelText.color = Color.yellow;
            _playerUstuLevelText.text = "Lv. " + _playerScore;
            _karakterler[0].SetActive(false);
            _karakterler[1].SetActive(false);
            _karakterler[2].SetActive(false);
            _karakterler[3].SetActive(true);
            _karakterler[4].SetActive(false);
            _karakterler[5].SetActive(false);
            _karakterPaketiMovement.Karakter4Hizi();
        }
        else if (_playerScore >= 70 && _playerScore < 85)
        {
            if (_karakterSeviyesi != 5)
            {
                //_karakterPaketiMovement.Bekle();
                _karakterSeviyesi = 5;
            }
            else
            {

            }

            _playerUstuLevelText.color = Color.cyan;
            _playerUstuLevelText.text = "Lv. " + _playerScore;
            _karakterler[0].SetActive(false);
            _karakterler[1].SetActive(false);
            _karakterler[2].SetActive(false);
            _karakterler[3].SetActive(false);
            _karakterler[4].SetActive(true);
            _karakterler[5].SetActive(false);
            _karakterPaketiMovement.Karakter5Hizi();

        }
        else if (_playerScore >= 85)
        {
            if (_karakterSeviyesi != 6)
            {
                //_karakterPaketiMovement.Bekle();
                _karakterSeviyesi = 6;
            }
            else
            {

            }

            _playerUstuLevelText.color = Color.green;
            _playerUstuLevelText.text = "Lv. " + _playerScore;
            _karakterler[0].SetActive(false);
            _karakterler[1].SetActive(false);
            _karakterler[2].SetActive(false);
            _karakterler[3].SetActive(false);
            _karakterler[4].SetActive(false);
            _karakterler[5].SetActive(true);
            _karakterPaketiMovement.Karakter2Hizi();

        }
    }

    public void LevelStart()
    {
        _playerScore = 1;
        _toplananElmasSayisi = 1;
        _elmasSayisi = PlayerPrefs.GetInt("ElmasSayısı");
        _karakterSeviyesi = 1;
        _playerUstuLevelText.color = Color.red;
        _playerUstuLevelText.text = "Lv. " + _playerScore;
        _karakterler[0].SetActive(true);
        _karakterler[1].SetActive(false);
        _karakterler[2].SetActive(false);
        _karakterler[3].SetActive(false);
        _karakterler[4].SetActive(false);
        _karakterler[5].SetActive(false);
        _karakterAnimator = GameObject.FindWithTag("Karakter").GetComponent<Animator>();
        _karakterAnimator.SetBool("Victory", false);
        _karakterAnimator.SetBool("Walk", false);
        _karakterAnimator.SetBool("Idle", true);
        _karakterPaketi.transform.position = new Vector3(0, 0, 0);
        _karakterPaketi.transform.rotation = Quaternion.Euler(0, 0, 0);
        _karakterPaketi.GetComponent<KarakterPaketiMovement>().AciyiNormaleDondur();
        _karakterPaketi.GetComponent<KarakterPaketiMovement>().Karakter1Hizi();
        _player = GameObject.FindWithTag("Player");
        _player.transform.localPosition = new Vector3(0, 1, 0);
    }



}
