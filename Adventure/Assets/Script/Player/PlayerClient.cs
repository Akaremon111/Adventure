using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp.Net;
using WebSocketSharp;
using TMPro;

public class PlayerClient : MonoBehaviour
{
    // WebSocketのインスタンス
    public WebSocket ws;

    /// <summary>
    /// 現在のPlayerのポジション
    /// </summary>
    private Vector3 PlayerPos;

    /// <summary>
    /// 各ベクトルの値
    /// </summary>
    private float PosX;
    private float PosY;
    private float PosZ;

    /// <summary>
    /// Serverに渡すポジションの値
    /// </summary>
    private string ValueX;
    private string ValueY;
    private string ValueZ;

    private void Awake()
    {
        // Serverに接続
        JoinServer();
    }

    /// <summary>
    /// サーバーに接続する
    /// </summary>
    private void JoinServer()
    {
        // サーバーに接続
        ws = new WebSocket("ws://127.0.0.1:1234/");
        ws.Connect();

        //ws.OnMessage += (sender, e) => testPos(e.Data);
    }

    private void Update()
    {
        // Playerの座標の取得
        getPosition();

        // 取得したPlayerの情報をstringにキャストする
        PlayerInfoCast();

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

        // 各ベクトルの座標
        PosX = PlayerPos.x;
        PosY = PlayerPos.y;
        PosZ = PlayerPos.z;
    }

    /// <summary>
    /// Playerの情報の型変換
    /// </summary>
    private void PlayerInfoCast()
    {
        // 各ベクトルの値をfloatからstringに変換する
        ValueX = PosX.ToString();
        ValueY = PosY.ToString();
        ValueZ = PosZ.ToString();
    }

    /// <summary>
    /// Playerの情報をServerに渡す
    /// </summary>
    private void SendPlayerInfo()
    {
        string moveDate;

        moveDate = $"{PlayerPos.x},{PlayerPos.y},{PlayerPos.z}";

        ws.Send(moveDate);
    }

    //private void testPos(string date)
    //{
    //    Debug.Log(date);
    //}
}