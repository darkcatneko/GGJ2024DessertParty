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


    void Start()
    {
        CurrentTime = TotalTime;
        //TimerText = this.gameObject.GetComponent<Text>();
        UpdatetimerText();
    }

    void Update()
    {
        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            UpdatetimerText();
        }
        else
        {
            OnGameTimeUp.Invoke();
        }
    }
    void UpdatetimerText()
    {
        int minutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
