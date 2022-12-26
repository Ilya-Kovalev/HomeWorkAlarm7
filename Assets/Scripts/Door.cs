using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _house;
    [SerializeField] private UnityEvent _entered;
    [SerializeField] private UnityEvent _cameOut;
  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Goblin>(out Goblin goblin)) 
        {
            _house.SetActive(false);
            _entered.Invoke();
        }
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
        _house.SetActive(true);
        _cameOut.Invoke();
    }
}
