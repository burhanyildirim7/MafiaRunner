using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OyunSonuToplulukControl : MonoBehaviour
{

    [SerializeField] private List<Animator> _toplulukAnimators = new List<Animator>();
    
  

    public void SevninmeAnimasyonuBaslat()
    {
        for (int i = 0; i < _toplulukAnimators.Count; i++)
        {
            _toplulukAnimators[i].SetBool("Victory", true);
        }
    }

    public void UzulmeAnimasyonuBaslat()
    {
        for (int i = 0; i < _toplulukAnimators.Count; i++)
        {
            _toplulukAnimators[i].SetBool("Lose", true);
        }
    }
}
