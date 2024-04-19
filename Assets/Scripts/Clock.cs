using System;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{

    [SerializeField] TMP_Text _text;
    float _currentTime = 0;


    private void Update()
    {
        _currentTime += Time.deltaTime;

        TimeSpan time = TimeSpan.FromSeconds(_currentTime);

        if(time.Seconds < 10) _text.text = time.Minutes.ToString() + ":0" + time.Seconds.ToString();
        else _text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

    }

}
