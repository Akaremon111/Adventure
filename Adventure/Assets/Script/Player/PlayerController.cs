using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerManager playerManager;

    // Playerのスピード
    private int PlayerSpeed;

    private void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();

    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        PlayerSpeed = playerManager.PlayerData.Speed;
        var InputX = Input.GetAxisRaw("Horizontal");
        var InputZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(InputX, 0, InputZ).normalized;
        transform.position += moveDirection * Time.deltaTime * PlayerSpeed;
    }
}
