using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp.Net;
using WebSocketSharp;
using TMPro;

public class ClientManager : MonoBehaviour
{
    public WebSocket ws;
    public TMP_Text chatText;
    public Button sendButton;
    public TMP_InputField messageInput;

    private void Awake()
    {
        //�ڑ������B�ڑ���T�[�o�ƁA�|�[�g�ԍ����w�肷��
        ws = new WebSocket("ws://127.0.0.1:1234/");
        ws.Connect();

        //���M�{�^���������ꂽ�Ƃ��Ɏ��s���鏈���uSendText�v��o�^����
        sendButton.onClick.AddListener(SendText);
        //�T�[�o���烁�b�Z�[�W����M�����Ƃ��Ɏ��s���鏈���uRecvText�v��o�^����
        ws.OnMessage += (sender, e) => RecvText(e.Data);
        //�T�[�o�Ƃ̐ڑ����؂ꂽ�Ƃ��Ɏ��s���鏈���uRecvClose�v��o�^����
        ws.OnClose += (sender, e) => RecvClose();
    }

    //�T�[�o�ցA���b�Z�[�W�𑗐M����
    public void SendText()
    {
        ws.Send(messageInput.text);
    }

    //�T�[�o����󂯎�������b�Z�[�W���AChatText�ɕ\������
    public void RecvText(string text)
    {
        chatText.text += (text + "\n");
    }
    //�T�[�o�̐ڑ����؂ꂽ�Ƃ��̃��b�Z�[�W���AChatText�ɕ\������
    public void RecvClose()
    {
        chatText.text = ("Close.");
    }
}
