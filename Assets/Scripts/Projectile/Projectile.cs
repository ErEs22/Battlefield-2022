using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Projectile : NetworkBehaviour
{
    [SerializeField] float waitForReturnTime = 5f;

    [SerializeField] GameObject HitVFX;

    [HideInInspector] public NetworkVariable<float> speed;

    [HideInInspector] public int damage;

    public ulong ownerID;

    public NetworkVariable<ulong> teamID;

    private void OnEnable()
    {
    }

    private void Update()
    {
        transform.Translate(transform.forward * speed.Value * Time.deltaTime, Space.World);
        // GetComponent<Rigidbody>().velocity = transform.forward * speed.Value;
    }

    public void Init(float speed)
    {
        this.speed.Value = speed;
    }

    public void WaitForDisable()
    {
        Invoke(nameof(DestroyProjectile), waitForReturnTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        HitServerRpc();
        if (!IsOwner)
        {
            return;
        }
        PlayerStatus playerStatus;
        if (other.TryGetComponent<PlayerStatus>(out playerStatus))
        {
            ulong id = playerStatus.GetComponent<NetworkObject>().NetworkObjectId;

            ulong clientID = playerStatus.GetComponent<NetworkObject>().OwnerClientId;
            TriggerServerRpc(id, clientID);
        }
        DestroyProjectile();
    }

    [ServerRpc(RequireOwnership = false)]
    void HitServerRpc()
    {
        HitClientRpc();
    }

    [ClientRpc]
    void HitClientRpc()
    {
        Hit();
    }

    void Hit()
    {
        Instantiate(HitVFX, transform.position, Quaternion.identity);
    }

    [ServerRpc]
    void TriggerServerRpc(ulong id, ulong clientID)
    {
        if (id == teamID.Value)
        {
            return;
        }
        NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<PlayerStatus>().TakeDamage(damage, ownerID);
    }

    // [ClientRpc]
    // void TriggerClientRpc(ulong id, ulong clientID)
    // {
    //     if (id == teamID.Value)
    //     {
    //         return;
    //     }
    //     NetworkManager.Singleton.ConnectedClients[clientID].PlayerObject.GetComponent<PlayerStatus>().TakeDamage(damage, clientID);
    // }

    void DestroyProjectile()
    {
        if (!NetworkObject.IsSpawned || !IsServer)
        {
            return;
        }

        NetworkObject.Despawn(true);
    }
}
