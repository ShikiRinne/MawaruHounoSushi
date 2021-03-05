using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームプレイ中のUIの処理
/// </summary>
public class PlayUI : MonoBehaviour
{
    private BGMManager BGMM;

    [SerializeField]
    private GameObject InfomationTexts;
    [SerializeField]
    private GameObject RedyText;
    [SerializeField]
    private GameObject GoText;
    [SerializeField]
    private GameObject EndText;
    [SerializeField]
    private GameObject Result;
    [SerializeField]
    private GameObject ToTitleText;

    //寿司の種類
    [SerializeField]
    private Text Text_ordermaguro;
    [SerializeField]
    private Text Text_ordersalmon;
    [SerializeField]
    private Text Text_orderebi;
    [SerializeField]
    private Text Text_orderikura;
    [SerializeField]
    private Text Text_orderuni;
    [SerializeField]
    //各寿司の個数
    private Text Text_magurocount;
    [SerializeField]
    private Text Text_salmoncount;
    [SerializeField]
    private Text Text_ebicount;
    [SerializeField]
    private Text Text_ikuracount;
    [SerializeField]
    private Text Text_unicount;
    //届けた皿数
    [SerializeField]
    private Text Text_deliveredcount;

    [SerializeField]
    private AudioSource SESource_Start;
    [SerializeField]
    private AudioSource SESource_End;
    [SerializeField]
    private AudioClip SEClip_Start;
    [SerializeField]
    private AudioClip SEClip_End;

    //一度のオーダー数
    private int OrderCount = 9;

    //各寿司のオーダー数
    public int MaguroCount { get; set; } = 0;
    public int SalmonCount { get; set; } = 0;
    public int EbiCount { get; set; } = 0;
    public int IkuraCount { get; set; } = 0;
    public int UniCount { get; set; } = 0;

    //届けた皿数
    public int DeliveredCount { get; set; } = 0;

    bool isStart = false;
    bool isResult = false;

    private List<int> OrderList = new List<int>();

    void Start()
    {
        BGMM = GameObject.Find("GameManager").GetComponent<BGMManager>();
        RedyText.SetActive(false);
        GoText.SetActive(false);
        EndText.SetActive(false);
        Result.SetActive(false);
        ToTitleText.SetActive(false);
    }

    void Update()
    {
        //ゲーム開始コルーチンを起動
        if (!isStart && Input.GetMouseButtonDown(0))
        {
            isStart = true;
            StartCoroutine(GameStart());
        }

        //ゲーム終了コルーチンを起動
        if (!isResult && GameManager.GM_Instance.IsGameEnd)
        {
            isResult = true;
            StartCoroutine(GameEnd());
        }

        //オーダー画面の寿司の個数をテキストに反映
        Text_magurocount.text = MaguroCount.ToString();
        Text_salmoncount.text = SalmonCount.ToString();
        Text_ebicount.text = EbiCount.ToString();
        Text_ikuracount.text = IkuraCount.ToString();
        Text_unicount.text = UniCount.ToString();

        //オーダーをリセットし新規オーダー
        if (GameManager.GM_Instance.IsPlaying)
        {
            OrderReset();
        }
    }

    /// <summary>
    /// ゲーム開始時UI処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameStart()
    {
        InfomationTexts.SetActive(false);
        RedyText.SetActive(true);
        yield return new WaitForSeconds(2f);

        RedyText.SetActive(false);
        GoText.SetActive(true);
        SESource_Start.PlayOneShot(SEClip_Start);
        yield return new WaitForSeconds(1f);

        GoText.SetActive(false);
        BGMM.Play(BGMManager.BGMType.Playing);
        GameManager.GM_Instance.CanControl = true;
        GameManager.GM_Instance.IsPlaying = true;
    }

    /// <summary>
    /// ゲーム終了時UI処理
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameEnd()
    {
        EndText.SetActive(true);
        BGMM.Stop(BGMManager.BGMType.Playing);
        SESource_End.PlayOneShot(SEClip_End);
        yield return new WaitForSeconds(2f);

        EndText.SetActive(false);
        Text_deliveredcount.text = DeliveredCount.ToString();
        Result.SetActive(true);
        yield return new WaitForSeconds(1f);

        GameManager.GM_Instance.CanSceneChange = true;
        ToTitleText.SetActive(true);
    }

    /// <summary>
    /// オーダー処理
    /// 0~4までのランダムな数値をリストに格納した後各数値の個数でどの寿司がいくつオーダーされたかを判定
    /// </summary>
    private void Order()
    {
        OrderList = new List<int>();
        for (int i = 0; i < OrderCount; ++i)
        {
            OrderList.Add(Random.Range(0, 5));
        }
        foreach (int sushitype in OrderList)
        {
            switch (sushitype)
            {
                case 0:
                    MaguroCount++;
                    break;
                case 1:
                    SalmonCount++;
                    break;
                case 2:
                    EbiCount++;
                    break;
                case 3:
                    IkuraCount++;
                    break;
                case 4:
                    UniCount++;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 提供済みオーダーの消去と新規オーダー
    /// </summary>
    private void OrderReset()
    {
        if (MaguroCount == 0 &&
            SalmonCount == 0 &&
            EbiCount == 0 &&
            IkuraCount == 0 &&
            UniCount == 0)
        {
            if (OrderList.Count != 0)
            {
                OrderList.Clear();
            }
            Order();
        }
    }
}
