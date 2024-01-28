using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class MainSoundEffectManager : MonoBehaviour
{
    public static MainSoundEffectManager Instance;
    [SerializeField] AudioClip[] AudioClips;
    [SerializeField] GameObject sePrefab_;
    private void Awake()
    {
        Instance = this;
    }

    public void SpawnSE(int soundID)
    {
        var obj = Instantiate(sePrefab_);
        sePrefab_.GetComponent<AudioSource>().PlayOneShot(AudioClips[soundID]);
    }
}
