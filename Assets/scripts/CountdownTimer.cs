using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Gamemanager;

public class GameTimeUpEvent : UnityEvent { }

public class CountdownTimer : MonoBehaviour
{

    bool ended_ = false;
    public float TotalTime = 60.0f;
    private float CurrentTime;
    public TextMeshProUGUI TimerText;

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
            if (!ended_)
            {
                ended_ = true;
                GameManager.Instance.MainGameEvent.Send(new GameTimeUpCommand());
            }           
        }
    }
    void UpdatetimerText()
    {
        int minutes = Mathf.FloorToInt(CurrentTime / 60);
        int seconds = Mathf.FloorToInt(CurrentTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
