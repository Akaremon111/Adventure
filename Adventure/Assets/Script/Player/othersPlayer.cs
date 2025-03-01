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

        // �T�[�o�[��testPos�֐���o�^����
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
        string[] positionData = data.Split(',');
        if (positionData.Length >= 3)
        {
            x = float.Parse(positionData[0]);
            y = float.Parse(positionData[1]);
            z = float.Parse(positionData[2]);
        }

        Debug.Log("X���W" + x);
        Debug.Log("Y���W" + y);
        Debug.Log("X���W" + z);
    }
}