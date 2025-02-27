using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp.Server;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    // WebSocketのインスタンス
    WebSocketServer ws;

    private void Awake()
    {
        OpenServer();
    }

    /// <summary>
    /// サーバーの起動
    /// </summary>
    private void OpenServer()
    {
        // ポート番号を指定
        ws = new WebSocketServer("ws://127.0.0.1:1234");

        // 接続してきた人の処理
        ws.AddWebSocketService<wsBehavior>("/");

        // サーバの立ち上げ
        ws.Start();
    }

    /// <summary>
    /// WebSocketBehavior継承クラス
    /// </summary>
    public class wsBehavior : WebSocketBehavior
    {
        // サーバーに接続している人を管理するリスト
        public static List<wsBehavior> clientList = new List<wsBehavior>();

        // 接続者に番号を振るための変数。
        static int globalpNumber = 0;

        // 自身の番号
        int pNumber;

        /// <summary>
        /// ログインしてきたときに呼ばれるメソッド
        /// </summary>
        protected override void OnOpen()
        {
            // ログインしてきた人に番号をつけて、リストに保存
            globalpNumber++;
            this.pNumber = globalpNumber;
            clientList.Add(this);

            Debug.Log("Player" + this.pNumber + " Login. (" + this.ID + ")");

            // 全員にメッセージを送る
            foreach (wsBehavior client in clientList)
            {
                client.Send("Player:" + pNumber + " Login.");
            }
        }

        /// <summary>
        /// ログアウトしてきたときに呼ばれるメソッド
        /// </summary>
        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Log("Player" + this.pNumber + " Logout. (" + this.ID + ")");

            // ログアウトした人をリストから削除。
            clientList.Remove(this);

            // 全員にメッセージを送る
            foreach (wsBehavior client in clientList)
            {
                client.Send("Player:" + pNumber + " Logout.");
            }
        }
    }
}