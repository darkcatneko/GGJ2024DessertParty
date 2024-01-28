using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameTimeUpEvent : UnityEvent { }

public class CountdownTimer : MonoBehaviour
{
    public float TotalTime = 60.0f;
    private float CurrentTime;
    public Text TimerText;

    public GameTimeUpEvent OnGameTimeUp= new GameTimeUpEvent();


    //void Start()
    //{
    //    CurrentTime = TotalTime;
    //    Update TimerText();
    //}

    //void Update()
    //{
    //    if (currentTime > 0)
    //    {
    //        currentTime -= Time.deltaTime;
    //        UpdateTimerText();
    //    }
    //    else
    //    {
    //        Debug.Log("倒计时结束！");
    //        OnGameTimeUp.Invoke(); 
    //    }
    //}
}
