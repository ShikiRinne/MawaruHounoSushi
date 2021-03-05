using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画面をフェードさせる
/// </summary>
public class FadeScreen : MonoBehaviour
{
    public static FadeScreen FS_Instance = null;

    private Color ImageColor;

    [SerializeField]
    private Image FadeImage;

    [SerializeField]
    private float FadeTime;
    private float FadeDir;
    private float Alpha;

    public float PassFadeTime
    {
        get { return FadeTime; }
        private set { FadeTime = value; }
    }

    void Awake()
    {
        //インスタンス生成、既に存在する場合重複回避のため削除
        if (FS_Instance == null)
        {
            FS_Instance = GetComponent<FadeScreen>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //画像の色で初期化
        ImageColor = FadeImage.color;
        Alpha = FadeImage.color.a;
    }

    void Update()
    {
        
    }

    public IEnumerator FadeIn()
    {
        FadeDir = -1;
        if (Alpha != 1)
        {
            Alpha = 1;
        }

        while (FadeImage.color.a > 0)
        {
            CalcAlpha(FadeDir);
            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        FadeDir = 1;
        if (Alpha != 0)
        {
            Alpha = 0;
        }

        while (FadeImage.color.a < 1)
        {
            CalcAlpha(FadeDir);
            yield return null;
        }
    }

    /// <summary>
    /// アルファ値の加減算
    /// </summary>
    /// <param name="dir"></param>
    public void CalcAlpha(float dir)
    {
        Alpha += dir * (Time.deltaTime / FadeTime);
        Alpha = Mathf.Clamp01(Alpha);
        ImageColor = new Color(ImageColor.r, ImageColor.g, ImageColor.b, Alpha);
        FadeImage.color = ImageColor;
    }
}
