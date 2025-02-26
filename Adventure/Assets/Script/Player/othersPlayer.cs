using UnityEngine;
using WebSocketSharp.Net;
using WebSocketSharp;
using UnityEngine.EventSystems;


public class othersPlayer : MonoBehaviour
{
    private WebSocket ws;

    float x;
    float y;
    float z;

    // Start is called before the first frame update
    private void Awake()
    {
        ws = new WebSocket("ws://127.0.0.1:1234/");

        ws.OnMessage += (sender, e) => testPos(e.Data);
    }

    // Update is called once per frame
    void Update()
    {
        // 取得した値でプレイヤーの位置を更新
        transform.position = new Vector3(x, y, z);
    }

    private void testPos(string date)
    {
        Debug.Log(date);

        string[] positionData = date.Split(',');
        if (positionData.Length == 3)
        {
            x = float.Parse(positionData[0]);
            y = float.Parse(positionData[1]);
            z = float.Parse(positionData[2]);
        }
    }
}
