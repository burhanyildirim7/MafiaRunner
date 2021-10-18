using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterPaketiMovement : MonoBehaviour
{
    [SerializeField] private float _speed;

    private float _firstSpeed;

    void Start()
    {
        _firstSpeed = _speed;
    }


    void Update()
    {
        if (GameController._oyunAktif && !GameController._oyunuBeklet)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        }
        else
        {

        }
        

    }

    public void Bekle()
    {
        _speed = 0;
        GameController._oyunuBeklet = true;
        Invoke("DevamEt", 1.5f);
    }

    private void DevamEt()
    {
        GameController._oyunuBeklet = false;
        _speed = _firstSpeed;
    }
}
