using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public enum EntityType
{
    Player,
    Enemy,
    Boss
}

/// <summary>
/// �e�G���e�B�e�B�̃X�e�[�^�X
/// </summary>
[Serializable]
public class Status
{
    /// <summary>
    /// Entity�̎��
    /// </summary>
    public string Type;

    /// <summary>
    /// �eEntity�̗̑�
    /// </summary>
    public int HP;

    /// <summary>
    /// �eEntity�̃X�s�[�h
    /// </summary>
    public int Speed;

    /// <summary>
    /// �eEntity�̍U����
    /// </summary>
    public int Power;
}

[Serializable]
public class EntityList
{
    public Status[] entity;
}
public class LoadJson : MonoBehaviour
{
    /// <summary>
    /// LoadJson�̃C���X�^���X���V���O���g���ɂ���
    /// </summary>
    public static LoadJson Instance;

    public EntityList EntityList = new EntityList();

    // Json�t�@�C���̃p�X
    private string path;

    private void Awake()
    {
        // �֐��̓ǂݍ���
        singleton();
        LoadJsonDate();
    }

    /// <summary>
    /// �V���O���g���̏���
    /// </summary>
    private void singleton()
    {
        // Instance��null�̏ꍇ�͎��M��Instance�Ƃ���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    /// <summary>
    /// Json�t�@�C������f�[�^��ǂݍ���
    /// </summary>
    private void LoadJsonDate()
    {
        // �p�X�̎w��
        path = Application.streamingAssetsPath + "/Status.json";

        // �w�肵���p�X�ɃA�N�Z�X
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            EntityList = JsonUtility.FromJson<EntityList>(json);
        }
        // �G���[���̏���
        else
        {
            Debug.LogError("Json�t�@�C����������Ȃ�");
        }
    }
}