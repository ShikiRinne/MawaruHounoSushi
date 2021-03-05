using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

/// <summary>
/// 基本的な寿司の皿の処理
/// </summary>
public class SushiBase : MonoBehaviour
{
    private ObjectManager OBM;
    private PlayUI PUI;

    private Vector3 PosTemp;

    [SerializeField]
    private GameObject space1 = null;
    [SerializeField]
    private GameObject space2 = null;
    [SerializeField]
    private GameObject space3 = null;

    [SerializeField]
    private AudioSource SESource_Sara;
    [SerializeField]
    private AudioClip SEClip_PutLane;
    [SerializeField]
    private AudioClip SEClip_PutSyari;
    [SerializeField]
    private AudioClip SEClip_NetaOnSyari;
    [SerializeField]
    private AudioClip SEClip_NetaOnGunkan;
    [SerializeField]
    private AudioClip SEClip_RoolNori;

    private bool isSyari = false;

    private bool isOnLane = false;
    private bool isFrowSushi = false;

    void Start()
    {
        OBM = GameObject.Find("GameManager").GetComponent<ObjectManager>();
        PUI = GameObject.Find("Canvas").GetComponent<PlayUI>();

        //皿の子オブジェクトを非アクティブ化
        foreach (Transform sushi in gameObject.transform)
        {
            sushi.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 食材を乗せていく
    /// EventTrigger_Drop
    /// </summary>
    public void PutFoods()
    {
        //シャリがなければシャリを乗せる
        if (!isSyari && OBM.PassSushiType == ObjectManager.SushiType.Syari)
        {
            foreach (Transform syari in gameObject.transform)
            {
                if (syari.CompareTag("Syari"))
                {
                    syari.gameObject.SetActive(true);
                    SESource_Sara.PlayOneShot(SEClip_PutSyari);
                }
            }
        }
    }
    
    /// <summary>
    /// 寿司を動かす
    /// </summary>
    public void SushiMove()
    {
        foreach (Transform sushi in gameObject.transform)
        {
            if (GameManager.GM_Instance.CanControl && sushi.gameObject.activeSelf && sushi.CompareTag("Sushi"))
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                gameObject.transform.position = new Vector3(pos.x, pos.y, -3f);

                //移動限界の設定
                if (gameObject.transform.position.y > 1.3f)
                {
                    gameObject.transform.position = new Vector3(pos.x, 1.3f, -3f);
                }
            }
        }
    }

    /// <summary>
    /// 寿司を掴む
    /// 寿司の位置を保存
    /// </summary>
    public void HaveSushi()
    {
        PosTemp = gameObject.transform.position;
    }

    /// <summary>
    /// レーンに置く
    /// </summary>
    public void PutInLane()
    {
        if (isOnLane)
        {
            isSyari = false;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.6f, -1f);
            SESource_Sara.PlayOneShot(SEClip_PutLane);
            CheckFlowSushiType(gameObject);
        }
        else
        {
            gameObject.transform.position = PosTemp;
        }
    }

    /// <summary>
    /// レーンとの接触判定
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOnLane = true;
        if (!isFrowSushi)
        {
            switch (collision.gameObject.name)
            {
                case "lane1":
                    AvailableSpace();
                    gameObject.transform.parent = collision.transform;
                    isFrowSushi = true;
                    break;
                case "lane2":
                    AvailableSpace();
                    gameObject.transform.parent = collision.transform;
                    isFrowSushi = true;
                    break;
                case "lane3":
                    AvailableSpace();
                    gameObject.transform.parent = collision.transform;
                    isFrowSushi = true;
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// 調理スペースを空ける
    /// </summary>
    private void AvailableSpace()
    {
        if (PosTemp.x == space1.transform.position.x && PosTemp.y == space1.transform.position.y)
        {
            OBM.IsSpace1 = false;
        }
        if (PosTemp.x == space2.transform.position.x && PosTemp.y == space2.transform.position.y)
        {
            OBM.IsSpace2 = false;
        }
        if (PosTemp.x == space3.transform.position.x && PosTemp.y == space3.transform.position.y)
        {
            OBM.IsSpace3 = false;
        }
    }

    /// <summary>
    /// 流れていった寿司を削除する
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "DestroyPoint")
        {
            isOnLane = false;
            isFrowSushi = false;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 流した寿司の種類の判定
    /// </summary>
    /// <param name="sushi"></param>
    private void CheckFlowSushiType(GameObject sushi)
    {
        //アクティブ状態のオブジェクトがどの寿司かによって処理
        //オーダー画面の数字を減らしてスコアをカウント
        foreach (Transform sushitype in sushi.transform)
        {
            if (sushitype.gameObject.activeSelf)
            {
                switch (sushitype.gameObject.name)
                {
                    case "sushi_maguro":
                        if (PUI.MaguroCount != 0)
                        {
                            PUI.MaguroCount--;
                            PUI.DeliveredCount++;
                        }
                        break;
                    case "sushi_salmon":
                        if (PUI.SalmonCount != 0)
                        {
                            PUI.SalmonCount--;
                            PUI.DeliveredCount++;
                        }
                        break;
                    case "sushi_ebi":
                        if (PUI.EbiCount != 0)
                        {
                            PUI.EbiCount--;
                            PUI.DeliveredCount++;
                        }
                        break;
                    case "sushi_ikura":
                        if (PUI.IkuraCount != 0)
                        {
                            PUI.IkuraCount--;
                            PUI.DeliveredCount++;
                        }
                        break;
                    case "sushi_uni":
                        if (PUI.UniCount != 0)
                        {
                            PUI.UniCount--;
                            PUI.DeliveredCount++;
                        }
                        break;
                }
            }
        }
    }
}
