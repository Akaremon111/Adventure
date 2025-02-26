using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp.Net;
using WebSocketSharp;
using TMPro;

public class ClientManager : MonoBehaviour
{
    public WebSocket ws;
    public TMP_Text chatText;
    public Button sendButton;
    public TMP_InputField messageInput;

    private void Awake()
    {
        //接続処理。接続先サーバと、ポート番号を指定する
        ws = new WebSocket("ws://127.0.0.1:1234/");
        ws.Connect();

        //送信ボタンが押されたときに実行する処理「SendText」を登録する
        sendButton.onClick.AddListener(SendText);
        //サーバからメッセージを受信したときに実行する処理「RecvText」を登録する
        ws.OnMessage += (sender, e) => RecvText(e.Data);
        //サーバとの接続が切れたときに実行する処理「RecvClose」を登録する
        ws.OnClose += (sender, e) => RecvClose();
    }

    //サーバへ、メッセージを送信する
    public void SendText()
    {
        ws.Send(messageInput.text);
    }

    //サーバから受け取ったメッセージを、ChatTextに表示する
    public void RecvText(string text)
    {
        chatText.text += (text + "\n");
    }
    //サーバの接続が切れたときのメッセージを、ChatTextに表示する
    public void RecvClose()
    {
        chatText.text = ("Close.");
    }
}
