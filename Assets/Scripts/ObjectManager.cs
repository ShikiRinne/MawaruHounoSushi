using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 共通化させてオブジェクトの個数分処理させたくない変数の宣言
/// </summary>
public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource SESource_MakeSushi;
    [SerializeField]
    private AudioClip SEClip_MakeSyari;
    [SerializeField]
    private AudioClip SEClip_MakeGunkan;
    [SerializeField]
    private AudioClip SEClip_RoolNori;

    public bool IsSpace1 { get; set; } = false;
    public bool IsSpace2 { get; set; } = false;
    public bool IsSpace3 { get; set; } = false;

    public enum SushiType
    {
        Sara,
        Syari,
        Nori,
        Maguro,
        Salmon,
        Ebi,
        Uni,
        Ikura,
        None
    }
    public SushiType PassSushiType { get; set; }

    public void PlaySESyari()
    {
        SESource_MakeSushi.PlayOneShot(SEClip_MakeSyari);
    }

    public void PlaySEGunkan()
    {
        SESource_MakeSushi.PlayOneShot(SEClip_MakeGunkan);
    }

    public void PlaySERoolNori()
    {
        SESource_MakeSushi.PlayOneShot(SEClip_RoolNori);
    }
}
