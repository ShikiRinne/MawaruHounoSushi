using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BGMの管理
/// </summary>
public class BGMManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource BGM_OP;
    [SerializeField]
    private AudioSource BGM_Play;

    public enum BGMType
    {
        Opening,
        Playing
    }

    void Start()
    {
        Play(BGMType.Opening);
        Stop(BGMType.Playing);
    }

    public void Play(BGMType type)
    {
        switch (type)
        {
            case BGMType.Opening:
                BGM_OP.Play();
                break;
            case BGMType.Playing:
                BGM_Play.Play();
                break;
            default:
                break;
        }
    }

    public void Stop(BGMType type)
    {
        switch (type)
        {
            case BGMType.Opening:
                BGM_OP.Stop();
                break;
            case BGMType.Playing:
                BGM_Play.Stop();
                break;
            default:
                break;
        }
    }
}
