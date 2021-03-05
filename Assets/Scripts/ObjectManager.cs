using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 共通化させてオブジェクトの個数分処理させたくない変数の宣言
/// </summary>
public class ObjectManager : MonoBehaviour
{
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

}
