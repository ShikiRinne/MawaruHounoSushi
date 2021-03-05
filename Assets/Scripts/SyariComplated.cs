using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyariComplated : MonoBehaviour
{
    private ObjectManager OBM;

    void Start()
    {
        OBM = GameObject.Find("GameManager").GetComponent<ObjectManager>();
    }

    public void MakeSyariSushi()
    {
        foreach (Transform sushi in gameObject.transform.parent)
        {
            if (sushi.CompareTag("Syari"))
            {
                sushi.gameObject.SetActive(false);
            }
            switch (OBM.PassSushiType)
            {
                case ObjectManager.SushiType.Maguro:
                    if (sushi.name == "sushi_maguro")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySESyari();
                    }
                    break;
                case ObjectManager.SushiType.Salmon:
                    if (sushi.name == "sushi_salmon")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySESyari();
                    }
                    break;
                case ObjectManager.SushiType.Ebi:
                    if (sushi.name == "sushi_ebi")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySESyari();
                    }
                    break;
                case ObjectManager.SushiType.Ikura:
                    if (sushi.name == "sushi_ikura")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySESyari();
                    }
                    break;
                case ObjectManager.SushiType.Uni:
                    if (sushi.name == "sushi_uni")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySESyari();
                    }
                    break;
                case ObjectManager.SushiType.Nori:
                    if (sushi.name == "sushi_gunkan")
                    {
                        sushi.gameObject.SetActive(true);
                        OBM.PlaySERoolNori();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
