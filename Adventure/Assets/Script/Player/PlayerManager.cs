using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// Player�̃X�e�[�^�X�f�[�^
    /// </summary>
    public Status PlayerData
    { get; private set; }


    private void Start()
    {
        // Json�t�@�C������f�[�^��ǂݍ���(�o�O����̂��߂�Start���g�p)
        getJsonDate();
    }

    /// <summary>
    /// LoadJson����Player�̃X�e�[�^�X�̃f�[�^���擾����
    /// </summary>
    private void getJsonDate()
    {
        // Player�̃f�[�^���擾
        PlayerData = System.Array.Find(LoadJson.Instance.EntityList.entity, e => e.Type == "Player");
    }

    private void Update()
    {
        
    }
}