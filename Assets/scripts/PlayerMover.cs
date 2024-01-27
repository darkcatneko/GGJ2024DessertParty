using Gamemanager;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMover : MonoBehaviour
{
    float horizontal;
    float vertical;
    [SerializeField] PlayerIdentity thisPlayerIdentity_;
    [SerializeField] Rigidbody2D rigidbody2D_;
    [SerializeField] float runSpeed_;
    void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerMovement, cmd => { movementCommand(cmd);  });
    }

    void movementCommand(PlayerMovementCommand cmd)
    {
        if (cmd.PlayerIdentity == thisPlayerIdentity_)
        {
            horizontal = cmd.PlayerMovementVector.x; vertical = cmd.PlayerMovementVector.y;
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D_.velocity = new Vector2(horizontal * runSpeed_, vertical * runSpeed_);
    }
}
