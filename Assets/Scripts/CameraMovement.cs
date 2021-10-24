using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    private GameObject Player;

    // [SerializeField] private GameObject _dusmanObject;

    private GameObject _karakterPaketi;

    private Quaternion _karakterPaketiRotation;

    Vector3 aradakiFark;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        aradakiFark = transform.position - Player.transform.position;
        _karakterPaketi = GameObject.FindGameObjectWithTag("KarakterPaketi");
        _karakterPaketiRotation = _karakterPaketi.transform.rotation;



    }


    void Update()
    {
        _karakterPaketiRotation = _karakterPaketi.transform.rotation;
        transform.position = Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + aradakiFark.y, Player.transform.position.z + aradakiFark.z), Time.deltaTime * 5f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, _karakterPaketiRotation.y, 0), 1 * Time.deltaTime);

    }

}
