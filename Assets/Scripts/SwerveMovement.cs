using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    private SwerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float _hareketSinirlandirmaSol = -4f;
    [SerializeField] private float _hareketSinirlandirmaSag = 4f;

    [SerializeField] private GameObject _getPoint;

    [SerializeField] private float _radius;

    Vector3 centerPosition;
    //[SerializeField] private float maxSwerveAmount = 1f;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<SwerveInputSystem>();
    }

    private void Update()
    {
        if (GameController._oyunAktif && !GameController._oyunuBeklet)
        {

            centerPosition = _getPoint.transform.position;


            float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
            //swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
            transform.Translate(swerveAmount, 0, 0);


            float distance = Vector3.Distance(transform.position, centerPosition);

            if (distance > _radius)
            {
                Vector3 fromOriginToObject = transform.position - centerPosition;
                fromOriginToObject *= _radius / distance;
                transform.position = centerPosition + fromOriginToObject;
            }
            else
            {

            }

            /*
            if (KarakterPaketiMovement._duzGidiyor == true)
            {
                if (transform.position.x < _hareketSinirlandirmaSol)
                {
                    transform.position = new Vector3(_hareketSinirlandirmaSol, transform.position.y, transform.position.z);
                }
                else if (transform.position.x > _hareketSinirlandirmaSag)
                {
                    transform.position = new Vector3(_hareketSinirlandirmaSag, transform.position.y, transform.position.z);
                }
                else
                {

                }
            }
            else if (KarakterPaketiMovement._sagaDondu == true)
            {
                if (transform.position.z < _hareketSinirlandirmaSol)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, _hareketSinirlandirmaSol);
                }
                else if (transform.position.z > _hareketSinirlandirmaSag)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, _hareketSinirlandirmaSag);
                }
                else
                {

                }
            }
            else if (KarakterPaketiMovement._solaDondu == true)
            {
                if (transform.position.z < _hareketSinirlandirmaSol)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, _hareketSinirlandirmaSol);
                }
                else if (transform.position.z > _hareketSinirlandirmaSag)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, _hareketSinirlandirmaSag);
                }
                else
                {

                }
            }
            else
            {

            }
           */



        }

      
        
       
    }
}
