using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レーンを流す
/// </summary>
public class SushiLaneScroll : MonoBehaviour
{
    [SerializeField]
    private float ScrollSpeed;
    [SerializeField]
    private float ReturnPos;
    [SerializeField]
    private float GobackPos;

    void Update()
    {
        transform.Translate(ScrollSpeed, 0f, 0f);
        if (transform.position.x < ReturnPos)
        {
            transform.position = new Vector3(GobackPos, transform.position.y, transform.position.z);
        }
    }
}
