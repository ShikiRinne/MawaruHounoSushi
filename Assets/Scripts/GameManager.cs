using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の処理
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager GM_Instance = null;
    private BGMManager BGMM;

    private GameObject Fade;

    [SerializeField]
    private float TimeLimit;

    public bool CanSceneChange { get; set; } = false;
    public bool IsPlaying { get; set; } = false;
    public bool IsGameEnd { get; set; } = false;
    public bool CanControl { get; set; } = false;

    public enum GameState
    {
        Title,
        Play,
    }
    public GameState PassState { get; private set; }

    void Awake()
    {
        //インスタンス生成、既に存在する場合重複回避のため削除
        if (GM_Instance == null)
        {
            GM_Instance = GetComponent<GameManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BGMM = GetComponent<BGMManager>();
        Fade = GameObject.Find("FadeCanvas");
        StartCoroutine(GameStart());
        PassState = GameState.Title;
    }

    void Update()
    {
        switch (PassState)
        {
            case GameState.Title:
                //Playシーンへ
                if (CanSceneChange && Input.GetMouseButtonDown(0))
                {
                    CanSceneChange = false;
                    IsGameEnd = false;
                    StartCoroutine(SceneChange("Play"));
                }
                break;
            case GameState.Play:
                //開始したら制限時間をカウント
                if (IsPlaying)
                {
                    TimeLimit -= Time.deltaTime;
                }
                //制限時間オーバー
                if (TimeLimit <= 0)
                {
                    IsPlaying = false;
                    CanControl = false;
                    IsGameEnd = true;
                    //Titleシーンへ
                    if (CanSceneChange && Input.GetMouseButtonDown(0))
                    {
                        CanSceneChange = false;
                        StartCoroutine(SceneChange("Title"));
                    }
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// シーンの切り替え
    /// </summary>
    /// <param name="next"></param>
    /// <returns></returns>
    private IEnumerator SceneChange(string next)
    {
        if (!Fade.activeSelf)
        {
            Fade.SetActive(true);
        }
        StartCoroutine(FadeScreen.FS_Instance.FadeOut());
        yield return new WaitForSeconds(FadeScreen.FS_Instance.PassFadeTime);

        switch (next)
        {
            case "Title":
                SceneManager.LoadScene(next);
                BGMM.Play(BGMManager.BGMType.Opening);
                PassState = GameState.Title;
                break;
            case "Play":
                SceneManager.LoadScene(next);
                BGMM.Stop(BGMManager.BGMType.Opening);
                PassState = GameState.Play;
                break;
            default:
                break;
        }

        StartCoroutine(FadeScreen.FS_Instance.FadeIn());
        yield return new WaitForSeconds(FadeScreen.FS_Instance.PassFadeTime);
        Fade.SetActive(false);
        CanSceneChange = false;
    }

    /// <summary>
    /// ゲーム開始して最初のフェードイン
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart()
    {
        yield return StartCoroutine(FadeScreen.FS_Instance.FadeIn());
        CanSceneChange = true;
    }
}
