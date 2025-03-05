using UnityEngine;
using WebSocketSharp.Net;
using WebSocketSharp;
using UnityEngine.EventSystems;
using System;


public class othersPlayer : MonoBehaviour
{
    // スクリプトの取得
    [SerializeField]
    private ClientManager clientManager;

    // WebSocketのインスタンス
    private WebSocket ws;

    float x;
    float y;
    float z;

    private void Awake()
    {
        // wsManagerの取得
        ws = clientManager.wsManager;

        // サーバーにtestPos関数を登録する
        ws.OnMessage += (sender, e) => PlayerData(e.Data);
    }
    private void Update()
    {
        // 取得した値でプレイヤーの位置を更新
        transform.position = new Vector3(x, y, z);
    }

    private void PlayerData(string data)
    {
        string pData = data;
        string PlayerNumber = pData.Substring(0, 8);

        Debug.Log(PlayerNumber);

        // "Player:{number} Position:{x,y,z}" 形式を想定
        int index = data.IndexOf("Position:");
        if (index != -1)
        {
            // "Position:" の後ろだけを取得
            string positionString = data.Substring(index + 9).Trim();

            // 「,」で区切る
            string[] positionData = positionString.Split(',');

            // PositionDataがすべて取得出来たらキャストを行う
            if (positionData.Length >= 3)
            {
                x = float.Parse(positionData[0]);
                y = float.Parse(positionData[1]);
                z = float.Parse(positionData[2]);
            }
        }
    }
}