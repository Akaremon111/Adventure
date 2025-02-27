using UnityEngine;
using WebSocketSharp.Net;
using WebSocketSharp;
using UnityEngine.EventSystems;


public class othersPlayer : MonoBehaviour
{
    // �X�N���v�g�̎擾
    [SerializeField]
    private ClientManager clientManager;

    // WebSocket�̃C���X�^���X
    private WebSocket ws;

    float x;
    float y;
    float z;

    // Start is called before the first frame update
    private void Awake()
    {
        // wsManager�̎擾
        ws = clientManager.wsManager;

        ws.OnMessage += (sender, e) => testPos(e.Data);
    }

    // Update is called once per frame
    void Update()
    {
        // �擾�����l�Ńv���C���[�̈ʒu���X�V
        transform.position = new Vector3(x, y, z);
    }

    private void testPos(string data)
    {
        Debug.Log(data);

        string[] positionData = data.Split(',');
        x = float.Parse(positionData[0]);
        y = float.Parse(positionData[1]);
        z = float.Parse(positionData[2]);
    }
}