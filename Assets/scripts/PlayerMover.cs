using Gamemanager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    float horizontal;
    float vertical;
    public bool CanMove = true;
    [SerializeField] bool isVacuuming_;
    [SerializeField] LayerMask targetLayers_;
    [SerializeField] PlayerIdentity thisPlayerIdentity_;
    [SerializeField] Rigidbody2D rigidbody2D_;
    [SerializeField] float runSpeed_;
    [SerializeField] GameObject playerVacuumObject_;
    [SerializeField] float vacuumAngle_;
    [SerializeField] float vacuumDistance_;
    [SerializeField] float vacuumRadius_;
    [SerializeField] float vacuumPower_;
    [SerializeField] List<GameObject> collectedObjects = new List<GameObject>();
    [SerializeField] List<GameObject> objectInRadius = new List<GameObject>();
    [SerializeField] int[] ingredientArray = new int[3];
    [SerializeField] int nowArrayPlace = -1;
    [SerializeField] private SpriteRenderer PlayerWalktransform;
    [SerializeField] GameObject playerBulletPrefab_;
    [SerializeField] GameObject bulletSpawnPlace_;
    [SerializeField] float bulletShootForce_;
    [SerializeField] bool IsMoveRight = false;


    void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerMovement, cmd => { movementCommand(cmd); });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerVacuumControl, cmd => { getVacuumCommand(cmd); });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerVacuumSwitch, cmd => { vacuumSwitch(cmd); });
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerShootTrigger, cmd => { playerShooterTrigger(cmd); });


    }

    void movementCommand(PlayerMovementCommand cmd)
    {
        if (cmd.PlayerIdentity == thisPlayerIdentity_)
        {
            horizontal = cmd.PlayerMovementVector.x; vertical = cmd.PlayerMovementVector.y;
        }
    }
    private void Update()
    {
        getSphereObject();
        vacuum();
    }
    private void FixedUpdate()
    {
        if (CanMove)
        {
            rigidbody2D_.velocity = new Vector2(horizontal * runSpeed_, vertical * runSpeed_);
        }
        else
        {
            horizontal = 0;
            vertical = 0;
        }
        if (horizontal > 0)
        {
            if (!IsMoveRight)
            {
                PlayerWalktransform.flipX = true;
                IsMoveRight = true;
            }
        }
        if (horizontal < 0)
        {
            if (IsMoveRight)
            {
                PlayerWalktransform.flipX = false;
                IsMoveRight = false;
            }
        }
    }
    void getVacuumCommand(PlayerVacuumControlCommand cmd)
    {
        if (cmd.PlayerIdentity == thisPlayerIdentity_)
        {
            vacuumUpdater(cmd.PlayerVacuumVector);
        }
    }
    void vacuumUpdater(Vector2 joystickInput)
    {
        var angle = Mathf.Atan2(joystickInput.y, joystickInput.x) * Mathf.Rad2Deg;
        // Mathf.Atan2返回的是弧度，乘以Mathf.Rad2Deg将其转换为角度

        // 确保角度在0到360之间
        if (angle < 0)
        {
            angle += 360f;
        }
        vacuumAngle_ = angle;
        var euler = playerVacuumObject_.transform.rotation.eulerAngles;
        var quaternion = Quaternion.Euler(new Vector3(euler.x, euler.y, angle));
        playerVacuumObject_.transform.rotation = quaternion;
    }
    void getSphereObject()
    {
        if (!isVacuuming_) return;
        // 获取圆心位置
        Vector2 centerPosition = transform.position;

        // 检测圆内的碰撞体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPosition, vacuumDistance_, targetLayers_);

        // 清空已收集的对象列表
        collectedObjects.Clear();
        objectInRadius.Clear();
        // 将检测到的对象添加到列表中
        foreach (Collider2D collider in colliders)
        {
            collectedObjects.Add(collider.gameObject);
        }

        // 在这里，collectedObjects 列表包含了所有在圆内的指定Layer的游戏对象
        // 你可以根据需要对这些对象进行进一步处理
        foreach (var item in collectedObjects)
        {
            Debug.Log(getObjectDegree(item) + item.gameObject.name);
            float angleDifference = Mathf.Abs(vacuumAngle_ - getObjectDegree(item));
            if (angleDifference <= vacuumRadius_ && item != this.gameObject)
            {
                objectInRadius.Add(item);
            }
        }
    }
    float getObjectDegree(GameObject objectB)
    {
        // 获取从A到B的向量
        Vector2 direction = objectB.transform.position - this.gameObject.transform.position;

        // 使用Mathf.Atan2计算角度（弧度）
        float angleInRadians = Mathf.Atan2(direction.y, direction.x);

        // 将弧度转换为角度
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // 注意：angleInDegrees 是相对于正X轴的逆时针角度

        if (angleInDegrees < 0)
        {
            angleInDegrees += 360f;
        }
        return angleInDegrees;

    }
    void vacuum()
    {
        var attractorCenter = this.gameObject.transform.position;
        if (!isVacuuming_ || nowArrayPlace >= 2) return;
        // 遍历列表中的所有物体
        foreach (GameObject attractedObject in objectInRadius)
        {
            if (attractedObject == null) return;
            // 计算朝向中心点的方向
            var rb = attractedObject.GetComponent<Rigidbody2D>();
            Vector2 direction = (attractorCenter - attractedObject.transform.position).normalized;
            var percentage = 1 - (attractorCenter - attractedObject.transform.position).magnitude / vacuumDistance_;
            // 使用AddForce方法来施加力，使物体移动向中心
            if (attractedObject.CompareTag("Player"))
            {
                rb.AddForce(direction * vacuumPower_ * 2f);

            }
            else
            {
                if ((attractorCenter - attractedObject.transform.position).magnitude <= 0.65f && nowArrayPlace < 2)
                {
                    var id = attractedObject.GetComponent<IngredientIdentity>().ThisIngredient;
                    nowArrayPlace += 1;
                    ingredientArray[nowArrayPlace] = (int)id;
                    GameManager.Instance.MainGameEvent.Send(new PlayerGetIngredientCommand() { IngredientType = id });                   
                    MainSoundEffectManager.Instance.SpawnSE(2);
                    if (nowArrayPlace == 2)
                    {
                        MainSoundEffectManager.Instance.SpawnSE(3);
                    }
                    Destroy(attractedObject.gameObject);
                    return;
                }
                rb.AddForce(direction * vacuumPower_ * percentage * percentage);
            }
        }
    }

    void vacuumSwitch(PlayerVacuumSwitchCommand cmd)
    {
        if (cmd.PlayerIdentity == thisPlayerIdentity_)
        {
            isVacuuming_ = cmd.Trigger;
            if (isVacuuming_)
            {
                MainSoundEffectManager.Instance.SpawnSE(1);
            }
            
        }
    }

    void playerShooterTrigger(PlayerShootTriggerCommand cmd)
    {
        if (cmd.PlayerIdentity != thisPlayerIdentity_)
        {
            return;
        }
        if (nowArrayPlace == 2)
        {
            nowArrayPlace = -1;
            //播放發射音效
            MainSoundEffectManager.Instance.SpawnSE(4);
            var bulletObject = Instantiate(playerBulletPrefab_, bulletSpawnPlace_.transform.position, Quaternion.identity);
            var dir = (bulletSpawnPlace_.transform.position - this.gameObject.transform.position).normalized;
            bulletObject.GetComponent<Rigidbody2D>().AddForce(dir * bulletShootForce_, ForceMode2D.Impulse);
            var info = bulletObject.GetComponent<BulletInfo>();
            info.fromWhichPlayer_ = thisPlayerIdentity_;
            Array.Sort(ingredientArray);
            for (int i = 0; i < ingredientArray.Length; i++)
            {
                info.Ingredients[i] = ingredientArray[i];
            }
            ingredientArray = new int[3];
        }
    }

    public async void Timer()
    {
        if (!CanMove)
        {
            await Task.Delay(1500);
            CanMove = true;

        }
    }

    public bool IdentityChecker(PlayerIdentity target)
    {
        if (target == thisPlayerIdentity_)
        {
            return true;
        }
        return false;
    }

    public PlayerIdentity GetIdentity()
    {
        return thisPlayerIdentity_;        
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(this.gameObject.transform.position, vacuumDistance_);
        drawCenter();
        drawUpperLine();
        drawLowerLine();
    }

    void drawCenter()
    {
        Vector3 centerPos = this.gameObject.transform.position;
        float x = centerPos.x + Mathf.Cos(Mathf.Deg2Rad * vacuumAngle_) * vacuumDistance_;
        float y = centerPos.y + Mathf.Sin(Mathf.Deg2Rad * vacuumAngle_) * vacuumDistance_;
        Vector3 endPos = new Vector3(x, y, centerPos.z);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(centerPos, endPos);
    }

    void drawUpperLine()
    {
        Vector3 centerPos = this.gameObject.transform.position;
        float x = centerPos.x + Mathf.Cos(Mathf.Deg2Rad * (vacuumAngle_ + vacuumRadius_)) * vacuumDistance_;
        float y = centerPos.y + Mathf.Sin(Mathf.Deg2Rad * (vacuumAngle_ + vacuumRadius_)) * vacuumDistance_;
        Vector3 endPos = new Vector3(x, y, centerPos.z);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(centerPos, endPos);
    }
    void drawLowerLine()
    {
        Vector3 centerPos = this.gameObject.transform.position;
        float x = centerPos.x + Mathf.Cos(Mathf.Deg2Rad * (vacuumAngle_ - vacuumRadius_)) * vacuumDistance_;
        float y = centerPos.y + Mathf.Sin(Mathf.Deg2Rad * (vacuumAngle_ - vacuumRadius_)) * vacuumDistance_;
        Vector3 endPos = new Vector3(x, y, centerPos.z);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(centerPos, endPos);
    }
}
