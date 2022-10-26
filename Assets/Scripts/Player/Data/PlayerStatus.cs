using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class PlayerStatus : NetworkBehaviour
{
    public string playerName;

    public NetworkVariable<int> maxHealth = new(100);

    public NetworkVariable<int> currentHealth = new(100);

    public PlayerAnimatorHandler playerAnimatorHandler;

    public Vector3 startPosition;

    TextMeshProUGUI healthText;

    public bool death;

    public override void OnNetworkSpawn()
    {
        startPosition = transform.position;
        // if (!IsOwner)
        // {
        //     return;
        // }
        // print(GameObject.FindGameObjectWithTag("PlayerName"));
        // playerName = FindObjectOfType<FindText>(true).GetComponent<Text>().text;
        playerName = "Player" + OwnerClientId;
        if (IsOwner)
        {
            GameObject.FindGameObjectWithTag("PlayerName").GetComponent<TextMeshProUGUI>().text = playerName;
            healthText = GameObject.FindGameObjectWithTag("PlayerHealth").GetComponent<TextMeshProUGUI>();
        }
        ScoreBoardUIManager.Instance.AddScoreBoard(playerName);
    }

    private void Update()
    {
        if (currentHealth.Value <= 0)
        {
            // playerAnimatorHandler.SetAnimatorBool("Death", true);
            // ReSpawnPlayerServerRpc();
        }
    }

    private void Awake()
    {
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
        // if(IsClient){
        //     RequestInitializeDataServerRpc();
        // }
    }

    [ServerRpc(RequireOwnership = false)]
    void ReSpawnPlayerServerRpc()
    {
        ReSpawnPlayerClientRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void ResetHealthServerRpc()
    {
        currentHealth.Value = maxHealth.Value;
        UpdatePlayerHealthClientRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void ResetPlayerStatusServerRpc(bool value)
    {
        ResetPlayerStatusClientRpc(value);
    }

    [ClientRpc]
    void ResetPlayerStatusClientRpc(bool value)
    {
        death = value;
    }

    [ClientRpc]
    void ReSpawnPlayerClientRpc()
    {
        StartReSpawnPlayerCoroutine();
        transform.position = ReSpawner.Instance.GetReSpawnPosition();
        ResetHealthServerRpc();
    }

    void StartReSpawnPlayerCoroutine()
    {
        StartCoroutine(nameof(ReSpawnPlayerCoroutine));
    }

    IEnumerator ReSpawnPlayerCoroutine()
    {
        yield return new WaitForSeconds(3f);
        ReSpawnPlayer();
        ResetPlayerStatusServerRpc(false);
    }

    void ReSpawnPlayer()
    {
        playerAnimatorHandler.SetAnimatorBool("Death", false);
    }

    [ServerRpc]
    void RequestInitializeDataServerRpc()
    {
        ClientInitizlizeDataClientRpc();
    }

    [ClientRpc]
    void ClientInitizlizeDataClientRpc()
    {
        InitializeData();
    }

    public void InitializeData()
    {
        this.maxHealth.Value = 100;
        this.currentHealth.Value = this.maxHealth.Value;
    }
    public void TakeDamage(int damage, ulong clientID)
    {
        currentHealth.Value -= damage;

        if (currentHealth.Value <= 0)
        {
            currentHealth.Value = 0;
            ResetPlayerStatusClientRpc(true);
            UpdateScoreBoardServerRpc(clientID);
            ReSetPlayerClientRpc();
        }
        UpdatePlayerHealthClientRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    void UpdateScoreBoardServerRpc(ulong clientID)
    {
        UpdateScoreBoardClientRpc(NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<PlayerStatus>().playerName);
    }

    [ClientRpc]
    void UpdateScoreBoardClientRpc(string playerName)
    {
        UpdateScoreBoard(playerName);
        print("UPDATESCORE:" + playerName);
    }

    void UpdateScoreBoard(string playerName)
    {
        if (death)
        {
            return;
        }
        ScoreBoardUIManager.Instance.UpdateScoreBoard(playerName);
    }

    [ClientRpc]
    void ReSetPlayerClientRpc()
    {
        playerAnimatorHandler.SetAnimatorBool("Death", true);
        ReSpawnPlayerServerRpc();
    }

    [ClientRpc]
    void UpdatePlayerHealthClientRpc()
    {
        if (!IsOwner)
        {
            return;
        }
        healthText.text = currentHealth.Value.ToString();
    }
}
