using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private GameObject _getPoint;

    [SerializeField] private float _radius;

    Plane objPlane;

    Vector3 centerPosition;

    private void Start()
    {

    }

    Ray GenerateMouseRay()
    {
        Vector3 mousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane
            );

        Vector3 mousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane
            );
        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
    }



    void Update()
    {

        if (GameController._oyunAktif && !GameController._oyunuBeklet)
        {
            centerPosition = _getPoint.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = GenerateMouseRay();
                RaycastHit hit;

                if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
                {
                    objPlane = new Plane(Vector3.up, transform.position);

                    Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                    float rayDistance;
                    objPlane.Raycast(mRay, out rayDistance);
                }
                else
                {
                        
                }
            }
            else if (Input.GetMouseButton(0))
            {
                Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float rayDistance;
                if (objPlane.Raycast(mRay, out rayDistance))
                {
                    transform.position = new Vector3(mRay.GetPoint(rayDistance).x, transform.position.y, transform.position.z);
                }
                else
                {

                }
            }
            else
            {

            }

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
        }
        else
        {

        }
        
    }
}
