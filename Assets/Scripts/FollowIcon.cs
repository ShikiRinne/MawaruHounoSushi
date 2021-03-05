using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 掴み上げたオブジェクトをアイコンとしてカーソルに追従
/// </summary>
public class FollowIcon : MonoBehaviour
{
    private GameManager GM;
    private ObjectManager OBM;

    [SerializeField]
    private GameObject Icon_Sara;
    [SerializeField]
    private GameObject Icon_Syari;
    [SerializeField]
    private GameObject Icon_Nori;
    [SerializeField]
    private GameObject Icon_Maguro;
    [SerializeField]
    private GameObject Icon_Salmon;
    [SerializeField]
    private GameObject Icon_Ebi;
    [SerializeField]
    private GameObject Icon_Uni;
    [SerializeField]
    private GameObject Icon_Ikura;

    [SerializeField]
    private GameObject Sushi_Sara;

    private GameObject Icon;
    private GameObject CloneIcon;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        OBM = GameObject.Find("GameManager").GetComponent<ObjectManager>();
    }

    /// <summary>
    /// 掴み上げたオブジェクトごとの処理
    /// EventTrigger_PointerDown
    /// </summary>
    public void Pick()
    {
        //掴み上げたオブジェクトによってアイコンを替える
        switch (gameObject.name)
        {
            case "sara_yamadumi":
                Icon = Icon_Sara;
                OBM.PassSushiType = ObjectManager.SushiType.Sara;
                break;
            case "syarimachine":
                Icon = Icon_Syari;
                OBM.PassSushiType = ObjectManager.SushiType.Syari;
                break;
            case "food_nori":
                Icon = Icon_Nori;
                OBM.PassSushiType = ObjectManager.SushiType.Nori;
                break;
            case "food_maguro":
                Icon = Icon_Maguro;
                OBM.PassSushiType = ObjectManager.SushiType.Maguro;
                break;
            case "food_salmon":
                Icon = Icon_Salmon;
                OBM.PassSushiType = ObjectManager.SushiType.Salmon;
                break;
            case "food_ebi":
                Icon = Icon_Ebi;
                OBM.PassSushiType = ObjectManager.SushiType.Ebi;
                break;
            case "food_ikura":
                Icon = Icon_Ikura;
                OBM.PassSushiType = ObjectManager.SushiType.Ikura;
                break;
            case "food_uni":
                Icon = Icon_Uni;
                OBM.PassSushiType = ObjectManager.SushiType.Uni;
                break;
            default:
                break;
        }

        //掴み上げたオブジェクトのアイコンをマウスカーソルの位置に生成
        if (GM.CanControl)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CloneIcon = Instantiate(Icon, new Vector3(pos.x, pos.y, -5f), Quaternion.identity);
        }
    }

    /// <summary>
    /// マウスカーソルにアイコンを追従させて移動
    /// EventTrigger_Drag
    /// </summary>
    public void Move()
    {
        if (GM.CanControl)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CloneIcon.transform.position = new Vector3(pos.x, pos.y, -9f);
        }
    }

    /// <summary>
    /// アイコンの消去
    /// EventTrigger_PointerUp
    /// </summary>
    public void Release()
    {
        Destroy(CloneIcon);
    }
}
