using System;
using System.Collections;
using System.Collections.Generic;
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
            collision.gameObject.GetComponent<PlayerMover>().CanMove = false;
            collision.gameObject.GetComponent<PlayerMover>().Timer();
            var dir = (collision.gameObject.transform.position - this.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(force_ * dir);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    void timer()
    {

    }
}
