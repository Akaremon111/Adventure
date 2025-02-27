using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp.Net;
using WebSocketSharp;
using TMPro;

public class ClientManager : MonoBehaviour
{
    // WebSocketのインスタンス
    public WebSocket wsManager
    { get; private set; } = new WebSocket("ws://127.0.0.1:1234/");

    private void Awake()
    {
        // サーバーに接続
        wsManager.Connect();
    }
}
