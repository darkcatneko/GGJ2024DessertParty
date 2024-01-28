using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;

public class RegisterUI : MonoBehaviour
{
    public static RegisterUI Instance;
    
    [SerializeField] GameObject[] targetPlace_;
    [SerializeField] GameObject[] targetImage_;
    private void Awake()
    {
        Instance = this;
    }
    public void MoveImage(int target)
    {
        targetImage_[target].transform.DOMove(targetPlace_[target].transform.position, 0.35f);
    }
}
