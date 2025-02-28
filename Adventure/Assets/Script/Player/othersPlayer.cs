using UnityEngine;
using WebSocketSharp.Net;
using WebSocketSharp;
using UnityEngine.EventSystems;


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

    // Start is called before the first frame update
    private void Awake()
    {
        // wsManagerの取得
        ws = clientManager.wsManager;

        // サーバーにtestPos関数を登録する
        ws.OnMessage += (sender, e) => testPos(e.Data);
    }

    // Update is called once per frame
    void Update()
    {
        // 取得した値でプレイヤーの位置を更新
        transform.position = new Vector3(x, y, z);
    }

    private void testPos(string data)
    {
        string[] positionData = data.Split(',');
        if (positionData.Length >= 3)
        {
            float.TryParse(positionData[0], out x);
            float.TryParse(positionData[1], out y);
            float.TryParse(positionData[2], out z);
        }

        Debug.Log("X座標" + x);
        Debug.Log("Y座標" + y);
        Debug.Log("X座標" + z);
    }
}