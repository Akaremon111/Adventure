using UnityEngine;
using WebSocketSharp.Net;
using WebSocketSharp;
using UnityEngine.EventSystems;
using System;


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
        // "Player:{number} Position:{x,y,z}" �`����z��
        int index = data.IndexOf("Position:");
        if (index != -1)
        {
            // "Position:" �̌�낾�����擾
            string positionString = data.Substring(index + 9).Trim();

            // �u,�v�ŋ�؂�
            string[] positionData = positionString.Split(',');

            // PositionData�����ׂĎ擾�o������L���X�g���s��
            if (positionData.Length >= 3)
            {
                x = float.Parse(positionData[0]);
                y = float.Parse(positionData[1]);
                z = float.Parse(positionData[2]);
            }

            Debug.Log($"��M�f�[�^: {data}");
            Debug.Log($"X���W: {x}");
            Debug.Log($"Y���W: {y}");
            Debug.Log($"Z���W: {z}");
        }
    }
}