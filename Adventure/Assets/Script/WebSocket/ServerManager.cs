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

    private void OpenServer()
    {
        //�|�[�g�ԍ����w��
        ws = new WebSocketServer("ws://127.0.0.1:1234");
        //�N���C�A���g����̒ʐM���̋������`�����N���X�A�uExWebSocketBehavior�v��o�^
        ws.AddWebSocketService<ExWebSocketBehavior>("/");

        //�T�[�o�N��
        ws.Start();
    }

    /// <summary>
    /// WebSocketBehavior�p���N���X
    /// </summary>
    public class ExWebSocketBehavior : WebSocketBehavior
    {
        //���ݐڑ����Ă���l���Ǘ����郊�X�g�B
        public static List<ExWebSocketBehavior> clientList = new List<ExWebSocketBehavior>();

        //�ڑ��҂ɔԍ���U�邽�߂̕ϐ��B
        static int globalSeq = 0;

        //���g�̔ԍ�
        int seq;

        /// <summary>
        /// ���O�C�����Ă����Ƃ��ɌĂ΂�郁�\�b�h
        /// </summary>
        protected override void OnOpen()
        {
            // ���O�C�����Ă����l�ɂ́A�ԍ������āA���X�g�ɓo�^�B
            globalSeq++;
            this.seq = globalSeq;
            clientList.Add(this);

            Debug.Log("Seq" + this.seq + " Login. (" + this.ID + ")");

            //�ڑ��ґS���Ƀ��b�Z�[�W�𑗂�
            foreach (ExWebSocketBehavior client in clientList)
            {
                client.Send("Seq:" + seq + " Login.");
            }
        }

        /// <summary>
        /// �N�������b�Z�[�W�𑗐M���Ă����Ƃ��ɌĂ΂�郁�\�b�h
        /// </summary>
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.Log("Seq:" + seq + "..." + e.Data);
            //�ڑ��ґS���Ƀ��b�Z�[�W�𑗂�
            foreach (ExWebSocketBehavior client in clientList)
            {
                client.Send("Seq:" + seq + "..." + e.Data);
            }
        }

        /// <summary>
        /// ���O�A�E�g���Ă����Ƃ��ɌĂ΂�郁�\�b�h
        /// </summary>
        protected override void OnClose(CloseEventArgs e)
        {
            Debug.Log("Seq" + this.seq + " Logout. (" + this.ID + ")");

            //���O�A�E�g�����l���A���X�g����폜�B
            clientList.Remove(this);

            //�ڑ��ґS���Ƀ��b�Z�[�W�𑗂�
            foreach (ExWebSocketBehavior client in clientList)
            {
                client.Send("Seq:" + seq + " Logout.");
            }
        }
    }
}