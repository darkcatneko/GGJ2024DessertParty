using Gamemanager;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GamepadController : MonoBehaviour
{
    [SerializeField] Vector2 inputValue_;
    [SerializeField] PlayerIdentity playerIdentity_;
    [SerializeField] Vector2 vacuumInputValue_;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        playerIdentity_ = (PlayerIdentity)GameManager.Instance.GamepadRegister.GetEmptyPlayerSpot();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(inputValue_);
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
            SceneManager.LoadScene("MainGameScene");
        }
    }
    void OnPlayerVacuumControl(InputValue value)
    {
        if (value.Get<Vector2>() != Vector2.zero)
        {
            GameManager.Instance.MainGameEvent.Send(new PlayerVacuumControlCommand() { PlayerIdentity = playerIdentity_, PlayerVacuumVector = value.Get<Vector2>() });
        }

    }

    void OnPlayerVacuumSwitch(InputValue value)
    {
        GameManager.Instance.MainGameEvent.Send(new PlayerVacuumSwitchCommand() { PlayerIdentity = playerIdentity_, Trigger = value.isPressed });
    }

    void OnPlayerShootTrigger()
    {
        GameManager.Instance.MainGameEvent.Send(new PlayerShootTriggerCommand());
    }
}
public enum PlayerIdentity
{
    Player1, Player2, Player3, Player4
}
