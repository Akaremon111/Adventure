using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    /// <summary>
    /// Playerのステータスデータ
    /// </summary>
    public Status PlayerData
    { get; private set; }


    private void Start()
    {
        // Jsonファイルからデータを読み込む(バグ回避のためにStartを使用)
        getJsonDate();
    }

    /// <summary>
    /// LoadJsonからPlayerのステータスのデータを取得する
    /// </summary>
    private void getJsonDate()
    {
        // Playerのデータを取得
        PlayerData = System.Array.Find(LoadJson.Instance.EntityList.entity, e => e.Type == "Player");
    }

    private void Update()
    {
        
    }
}