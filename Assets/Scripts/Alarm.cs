using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarm;

    private float _startVolume = 0;
    private float _maxVolume = 1;
    private float _duration = 3;
    private Coroutine _currentCorutine = null;

    public void StartAlarm()
    {
        _alarm.Stop();
        _alarm.volume = _startVolume;
        _alarm.PlayOneShot(_alarm.clip);

        if(_currentCorutine != null) 
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = StartCoroutine(DetermineSound(_startVolume, _maxVolume));
        }
        else 
        {
            _currentCorutine = StartCoroutine(DetermineSound(_startVolume, _maxVolume));
        }       
    }

    public void StopAlarm()
    {
        if(_currentCorutine != null) 
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = StartCoroutine(DetermineSound(_alarm.volume, _startVolume));
        }
    }    

    private float FindNumberOfChanges(float Duration) 
    {
        float numberOfChanges = Duration / Time.deltaTime;
        return numberOfChanges;
    }
   
    private IEnumerator DetermineSound(float currentVolume, float target) 
    {
        float numberOfChanges = FindNumberOfChanges(_duration);
        float sizeOfChange = _maxVolume / numberOfChanges;
        float totalChange = 0;

        while (_alarm.volume != target) 
        {
            _alarm.volume = Mathf.MoveTowards(currentVolume, target, totalChange);
            totalChange += sizeOfChange;

            yield return null;
        }
        _alarm.volume = target;

        if(target == _startVolume && _alarm.volume == _startVolume) 
        {
            _alarm.Stop();
        }
    }
}

