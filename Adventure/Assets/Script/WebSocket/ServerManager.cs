using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp.Server;
using WebSocketSharp;

public class ServerManager : MonoBehaviour
{
    // WebSocket�̃C���X�^���X
    WebSocketServer ws;

    private void Awake()
    {
        OpenServer();
    }

    /// <summary>
    /// �T�[�o�[�̋N��
    /// </summary>
    private void OpenServer()
    {
        // �|�[�g�ԍ����w��
        ws = new WebSocketServer("ws://127.0.0.1:1234");

        // �ڑ����Ă����l�̏���
        ws.AddWebSocketService<wsBehavior>("/");

        // �T�[�o�̗����グ
        ws.Start();
    }

    /// <summary>
    /// WebSocketBehavior�p���N���X
    /// </summary>
    public class wsBehavior : WebSocketBehavior
    {
        // �T�[�o�[�ɐڑ����Ă���l���Ǘ����郊�X�g
        public static List<wsBehavior> clientList = new List<wsBehavior>();

        // �ڑ��҂ɔԍ���U�邽�߂̕ϐ��B
        static int globalpNumber = 0;

        // ���g�̔ԍ�
        int pNumber;

        /// <summary>
        /// ���O�C�����Ă����Ƃ��ɌĂ΂�郁�\�b�h
        /// </summary>
        protected override void OnOpen()
        {
            // ���O�C�����Ă����l�ɔԍ������āA���X�g�ɕۑ�
            globalpNumber++;
            this.pNumber = globalpNumber;
            clientList.Add(this);

            Debug.Log("Player" + this.pNumber + " Login. (" + this.ID + ")");

            // �S���Ƀ��b�Z�[�W�𑗂�
            foreach (wsBehavior client in clientList)
            {
                client.Send("Player:" + pNumber + " Login.");
            }
        }

        /// <summary>
        /// ���O�A�E�g���Ă����Ƃ��ɌĂ΂�郁�\�b�h
        /// </summary>
        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Log("Player" + this.pNumber + " Logout. (" + this.ID + ")");

            // ���O�A�E�g�����l�����X�g����폜�B
            clientList.Remove(this);

            // �S���Ƀ��b�Z�[�W�𑗂�
            foreach (wsBehavior client in clientList)
            {
                client.Send("Player:" + pNumber + " Logout.");
            }
        }
    }
}