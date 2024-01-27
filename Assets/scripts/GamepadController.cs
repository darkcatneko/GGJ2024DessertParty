using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gamemanager;
using System.Security.Principal;
using UnityEngine.SceneManagement;

public class GamepadController : MonoBehaviour
{
    [SerializeField] Vector2 inputValue_;
    [SerializeField] PlayerIdentity playerIdentity_;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerIdentity_ = (PlayerIdentity)GameManager.Instance.GamepadRegister.GetEmptyPlayerSpot();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(inputValue_);
        GameManager.Instance.MainGameEvent.Send(new PlayerMovementCommand() { PlayerIdentity = playerIdentity_, PlayerMovementVector = inputValue_ });
    }

    void OnPlayerMovement(InputValue value)
    {
        inputValue_ = value.Get<Vector2>();      
    }
     
    void OnPlayerReadyGo()
    {
        var allClear = GameManager.Instance.GamepadRegister.CheckAllClear();

        if (allClear)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
public enum PlayerIdentity
{
    Player1, Player2, Player3, Player4
}
