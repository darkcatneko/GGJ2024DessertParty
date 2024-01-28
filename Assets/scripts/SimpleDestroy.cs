using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroy : MonoBehaviour
{
    [SerializeField] float destroyTime_;

    private void Start()
    {
        Destroy(this.gameObject,destroyTime_);
    }
}
