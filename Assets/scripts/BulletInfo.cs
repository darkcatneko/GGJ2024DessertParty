using System;
using System.Collections;
using Gamemanager;
using UnityEngine;

public class BulletInfo : MonoBehaviour
{
    public int[] Ingredients;
    public PlayerIdentity fromWhichPlayer_;
    [SerializeField] float force_;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerMover>().IdentityChecker(fromWhichPlayer_))
            {
                return;
            }
            var condition = checkQuest((int)fromWhichPlayer_,(int)collision.gameObject.GetComponent<PlayerMover>().GetIdentity());
            if (condition)
            {
                //告訴系統再生成一個任務
                GameManager.Instance.MainGameEvent.Send(new PlayerFinishQuestCommand() { FinishPlayerIdentity = fromWhichPlayer_ });
                MainSoundEffectManager.Instance.SpawnSE(6);
                GameManager.Instance.Score++;
            }
            else
            {
                collision.gameObject.GetComponent<PlayerMover>().CanMove = false;
                collision.gameObject.GetComponent<PlayerMover>().Timer();
                var dir = (collision.gameObject.transform.position - this.transform.position).normalized;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force_ * dir);
                MainSoundEffectManager.Instance.SpawnSE(5);
            }          
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
    bool checkQuest(int from,int hit)
    {
        var result = true;
        var quest = QuestSystem.instance.PlayerQuests[from];
        if (quest.ToPlayer != (PlayerIdentity)hit)
        {
            return false;
        }
        for (int i = 0; i < quest.Answer.Length; i++)
        {
            if (Ingredients[i] != quest.Answer[i])
            {
                result = false;
            }
        }
        return result;
    }
}

