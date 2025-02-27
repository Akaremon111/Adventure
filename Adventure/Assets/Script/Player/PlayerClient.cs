using UnityEngine;
using WebSocketSharp.Net;
using WebSocketSharp;

public class PlayerClient : MonoBehaviour
{
    // �X�N���v�g�̎擾
    [SerializeField]
    private ClientManager clientManager;

    // WebSocket�̃C���X�^���X
    private WebSocket ws;

    /// <summary>
    /// ���݂�Player�̃|�W�V����
    /// </summary>
    private Vector3 PlayerPos;

    private void Awake()
    {
        // wsManager�̎擾
        ws = clientManager.wsManager;

        // Server�ɐڑ�
        JoinServer();
    }

    /// <summary>
    /// �T�[�o�[�ɐڑ�����
    /// </summary>
    private void JoinServer()
    {
        // �T�[�o�[�ɐڑ�
        ws = new WebSocket("ws://127.0.0.1:1234/");
        ws.Connect();

        //ws.OnMessage += (sender, e) => testPos(e.Data);
    }

    private void Update()
    {
        // Player�̍��W�̎擾
        getPosition();

        // Player�̏���Server���ɑ���
        SendPlayerInfo();
    }

    /// <summary>
    /// Player�̍��W�̎擾
    /// </summary>
    private void getPosition()
    {
        // Player�̍��W�̍X�V
        PlayerPos = transform.position;
    }

    /// <summary>
    /// Player�̏���Server�ɓn��
    /// </summary>
    private void SendPlayerInfo()
    {
        string moveData;

        moveData = $"{PlayerPos.x},{PlayerPos.y},{PlayerPos.z}";

        ws.Send(moveData);
    }
}