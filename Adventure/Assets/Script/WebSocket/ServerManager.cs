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

    private void OpenServer()
    {
        //ポート番号を指定
        ws = new WebSocketServer("ws://127.0.0.1:1234");
        //クライアントからの通信時の挙動を定義したクラス、「ExWebSocketBehavior」を登録
        ws.AddWebSocketService<ExWebSocketBehavior>("/");

        //サーバ起動
        ws.Start();
    }

    /// <summary>
    /// WebSocketBehavior継承クラス
    /// </summary>
    public class ExWebSocketBehavior : WebSocketBehavior
    {
        //現在接続している人を管理するリスト。
        public static List<ExWebSocketBehavior> clientList = new List<ExWebSocketBehavior>();

        //接続者に番号を振るための変数。
        static int globalSeq = 0;

        //自身の番号
        int seq;

        /// <summary>
        /// ログインしてきたときに呼ばれるメソッド
        /// </summary>
        protected override void OnOpen()
        {
            // ログインしてきた人には、番号をつけて、リストに登録。
            globalSeq++;
            this.seq = globalSeq;
            clientList.Add(this);

            Debug.Log("Seq" + this.seq + " Login. (" + this.ID + ")");

            //接続者全員にメッセージを送る
            foreach (ExWebSocketBehavior client in clientList)
            {
                client.Send("Seq:" + seq + " Login.");
            }
        }

        /// <summary>
        /// 誰かがメッセージを送信してきたときに呼ばれるメソッド
        /// </summary>
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.Log("Seq:" + seq + "..." + e.Data);
            //接続者全員にメッセージを送る
            foreach (ExWebSocketBehavior client in clientList)
            {
                client.Send("Seq:" + seq + "..." + e.Data);
            }
        }

        /// <summary>
        /// ログアウトしてきたときに呼ばれるメソッド
        /// </summary>
        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Log("Seq" + this.seq + " Logout. (" + this.ID + ")");

            //ログアウトした人を、リストから削除。
            clientList.Remove(this);

            //接続者全員にメッセージを送る
            foreach (ExWebSocketBehavior client in clientList)
            {
                client.Send("Seq:" + seq + " Logout.");
            }
        }
    }
}