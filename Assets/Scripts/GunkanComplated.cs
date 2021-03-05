using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunkanComplated : MonoBehaviour
{
    private ObjectManager OBM;

    void Start()
    {
        OBM = GameObject.Find("GameManager").GetComponent<ObjectManager>();
    }

    public void MakeGunkanSushi()
    {
        foreach (Transform sushi in gameObject.transform.parent)
        {
            if (sushi.CompareTag("Gunkan"))
            {
                sushi.gameObject.SetActive(false);
            }
            switch (OBM.PassSushiType)
            {
                case ObjectManager.SushiType.Ikura:
                    if (sushi.name == "sushi_ikura")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySEGunkan();
                    }
                    break;
                case ObjectManager.SushiType.Uni:
                    if (sushi.name == "sushi_uni")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySEGunkan();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
