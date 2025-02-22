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
/// 各エンティティのステータス
/// </summary>
[Serializable]
public class Status
{
    /// <summary>
    /// Entityの種類
    /// </summary>
    public string Type;

    /// <summary>
    /// 各Entityの体力
    /// </summary>
    public int HP;

    /// <summary>
    /// 各Entityのスピード
    /// </summary>
    public int Speed;

    /// <summary>
    /// 各Entityの攻撃力
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
    /// LoadJsonのインスタンスをシングルトンにする
    /// </summary>
    public static LoadJson Instance;

    public EntityList EntityList = new EntityList();

    // Jsonファイルのパス
    private string path;

    private void Awake()
    {
        // 関数の読み込み
        singleton();
        LoadJsonDate();
    }

    /// <summary>
    /// シングルトンの処理
    /// </summary>
    private void singleton()
    {
        // Instanceがnullの場合は自信をInstanceとする
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
    /// Jsonファイルからデータを読み込む
    /// </summary>
    private void LoadJsonDate()
    {
        // パスの指定
        path = Application.streamingAssetsPath + "/Status.json";

        // 指定したパスにアクセス
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            EntityList = JsonUtility.FromJson<EntityList>(json);
        }
        // エラー時の処理
        else
        {
            Debug.LogError("Jsonファイルが見つからない");
        }
    }
}