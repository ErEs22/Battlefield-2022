using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerShootHandler : NetworkBehaviour
{
    [SerializeField] float projectileSpeed = 10f;

    [SerializeField] float shootInterval = 0.1f;

    [SerializeField] Projectile projectile;

    [SerializeField] Transform muzzlePoint;

    public PlayerAnimatorHandler playerAnimatorHandler;

    NetworkObjectPool networkObjectPool;

    float lastFireTime = 0f;

    private void Awake()
    {
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
        networkObjectPool = FindObjectOfType<NetworkObjectPool>();
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        lastFireTime -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && lastFireTime <= 0f)
        {
            lastFireTime = shootInterval;
            RequestShootServerRpc();
        }
    }

    [ServerRpc]
    void RequestShootServerRpc()
    {
        ShootClientRpc();
    }

    [ClientRpc]
    void ShootClientRpc()
    {
        Shoot();
    }

    void Shoot()
    {
        // var newprojectile = Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation);
        var newprojectile = networkObjectPool.GetNetworkObject(projectile.gameObject, muzzlePoint.position, muzzlePoint.rotation).GetComponent<Projectile>();
        playerAnimatorHandler.PlayTargetAnimation("Fire");
        newprojectile.Init(projectileSpeed);
        newprojectile.damage = 30;
        print("SHOOT");
        // newprojectile.teamID = GetComponent<NetworkObject>().OwnerClientId;
        // newprojectile.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }
}
