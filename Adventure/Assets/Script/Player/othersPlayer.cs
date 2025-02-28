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
            float.TryParse(positionData[0], out x);
            float.TryParse(positionData[1], out y);
            float.TryParse(positionData[2], out z);
        }

        Debug.Log("X���W" + x);
        Debug.Log("Y���W" + y);
        Debug.Log("X���W" + z);
    }
}