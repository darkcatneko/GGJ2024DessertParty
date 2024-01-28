using UnityEngine;

public class GameManager : ToSingletonMonoBehavior<GameManager>
{
    public MainGameEventPack MainGameEvent { get; private set; } = new MainGameEventPack();
    [field: SerializeField] public GamepadRegister GamepadRegister { get; set; }

    public int Score = 0;
    protected override void init()
    {
        base.init();
        GameManager.Instance.GamepadRegister = new GamepadRegister();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
