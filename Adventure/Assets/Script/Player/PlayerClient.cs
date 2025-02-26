using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp.Net;
using WebSocketSharp;
using TMPro;

public class PlayerClient : MonoBehaviour
{
    // WebSocket�̃C���X�^���X
    public WebSocket ws;

    /// <summary>
    /// ���݂�Player�̃|�W�V����
    /// </summary>
    private Vector3 PlayerPos;

    /// <summary>
    /// �e�x�N�g���̒l
    /// </summary>
    private float PosX;
    private float PosY;
    private float PosZ;

    /// <summary>
    /// Server�ɓn���|�W�V�����̒l
    /// </summary>
    private string ValueX;
    private string ValueY;
    private string ValueZ;

    private void Awake()
    {
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

        // �擾����Player�̏���string�ɃL���X�g����
        PlayerInfoCast();

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

        // �e�x�N�g���̍��W
        PosX = PlayerPos.x;
        PosY = PlayerPos.y;
        PosZ = PlayerPos.z;
    }

    /// <summary>
    /// Player�̏��̌^�ϊ�
    /// </summary>
    private void PlayerInfoCast()
    {
        // �e�x�N�g���̒l��float����string�ɕϊ�����
        ValueX = PosX.ToString();
        ValueY = PosY.ToString();
        ValueZ = PosZ.ToString();
    }

    /// <summary>
    /// Player�̏���Server�ɓn��
    /// </summary>
    private void SendPlayerInfo()
    {
        string moveDate;

        moveDate = $"{PlayerPos.x},{PlayerPos.y},{PlayerPos.z}";

        ws.Send(moveDate);
    }

    //private void testPos(string date)
    //{
    //    Debug.Log(date);
    //}
}