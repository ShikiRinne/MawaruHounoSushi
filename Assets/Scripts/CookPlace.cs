using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 調理スペース側の処理
/// </summary>
public class CookPlace : MonoBehaviour
{
    private GameManager GM;
    private ObjectManager OBM;

    [SerializeField]
    private GameObject Sushi_Sara;

    [SerializeField]
    private AudioSource SESource_PutCook;
    [SerializeField]
    private AudioClip SEClip_PutCook;

    void Start()
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        OBM = GameObject.Find("GameManager").GetComponent<ObjectManager>();
    }

    /// <summary>
    /// 皿が置かれたときの処理
    /// 同じ場所に重複して皿が置かれることの回避
    /// EventTrigger_Drop
    /// </summary>
    public void PutSara()
    {
        if (OBM.PassSushiType == ObjectManager.SushiType.Sara && GM.CanControl)
        {
            switch (gameObject.name)
            {
                case "cookspace1":
                    if (!OBM.IsSpace1)
                    {
                        Instantiate(Sushi_Sara, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3f), Quaternion.identity);
                        SESource_PutCook.PlayOneShot(SEClip_PutCook);
                        OBM.IsSpace1 = true;
                    }
                    break;
                case "cookspace2":
                    if (!OBM.IsSpace2)
                    {
                        Instantiate(Sushi_Sara, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3f), Quaternion.identity);
                        SESource_PutCook.PlayOneShot(SEClip_PutCook);
                        OBM.IsSpace2 = true;
                    }
                    break;
                case "cookspace3":
                    if (!OBM.IsSpace3)
                    {
                        Instantiate(Sushi_Sara, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -3f), Quaternion.identity);
                        SESource_PutCook.PlayOneShot(SEClip_PutCook);
                        OBM.IsSpace3 = true;
                    }
                    break;
            }
        }
    }
}
