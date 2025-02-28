using UnityEngine;
using WebSocketSharp.Net;
using WebSocketSharp;

public class PlayerClient : MonoBehaviour
{
    // スクリプトの取得
    [SerializeField]
    private ClientManager clientManager;

    // WebSocketのインスタンス
    private WebSocket ws;

    /// <summary>
    /// 現在のPlayerのポジション
    /// </summary>
    private Vector3 PlayerPos;

    private void Awake()
    {
        // wsManagerの取得
        ws = clientManager.wsManager;
    }

    private void Update()
    {
        // Playerの座標の取得
        getPosition();

        // Playerの情報をServer側に送る
        SendPlayerInfo();
    }

    /// <summary>
    /// Playerの座標の取得
    /// </summary>
    private void getPosition()
    {
        // Playerの座標の更新
        PlayerPos = transform.position;
    }

    /// <summary>
    /// Playerの情報をServerに渡す
    /// </summary>
    private void SendPlayerInfo()
    {
        string moveData;

        moveData = $"{PlayerPos.x},{PlayerPos.y},{PlayerPos.z}";

        ws.Send(moveData);
    }
}