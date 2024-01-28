using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField] GameObject ender_;
    [SerializeField] TextMeshProUGUI score_;
    void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnGameTimeUp, cmd => { ender_.SetActive(true); score_.text = GameManager.Instance.Score.ToString(); });
    }

   
}
