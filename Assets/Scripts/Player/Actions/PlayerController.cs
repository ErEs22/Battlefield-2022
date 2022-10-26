using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [SerializeField] CinemachineVirtualCamera aimVirtualCamera;

    [SerializeField] float normalSensitivity;

    [SerializeField] float aimSensitivity;

    [SerializeField] LayerMask hitLayerMask;

    [SerializeField] Transform debugTarget;

    [SerializeField] float projectileSpeed = 10f;

    [SerializeField] float shootInterval = 0.1f;

    [SerializeField] Projectile projectile;

    [SerializeField] GameObject muzzleFlare;

    [SerializeField] Transform muzzlePoint;

    [SerializeField] GameObject miniMapCamera;

    public PlayerAnimatorHandler playerAnimatorHandler;

    float lastFireTime = 0f;

    ThirdPersonController thirdPersonController;

    StarterAssetsInputs starterAssetsInputs;

    NetworkObjectPool networkObjectPool;

    PlayerStatus playerStatus;

    RaycastHit hitInfo;

    public CinemachineVirtualCamera normalCamera;

    public CinemachineVirtualCamera aimCamera;

    private void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
        networkObjectPool = FindObjectOfType<NetworkObjectPool>();
    }

    public override void OnNetworkSpawn()
    {
        transform.position = ReSpawner.Instance.GetReSpawnPosition();
        if (!IsOwner)
        {
            return;
        }
        miniMapCamera.SetActive(true);
        normalCamera = GameObject.FindGameObjectWithTag("NormalCamera").GetComponent<CinemachineVirtualCamera>();
        aimCamera = GameObject.FindGameObjectWithTag("AimCamera").GetComponent<CinemachineVirtualCamera>();
        normalCamera.Follow = thirdPersonController.cameraFollow;
        aimCamera.Follow = thirdPersonController.cameraFollow;
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (playerStatus.death)
        {
            normalCamera.Follow = null;
            aimCamera.Follow = null;
            return;
        }
        else
        {
            normalCamera.Follow = thirdPersonController.cameraFollow;
            aimCamera.Follow = thirdPersonController.cameraFollow;
        }
        Vector2 screenMiddle = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector3 mouseRaycastPoint = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(screenMiddle);
        if (Physics.Raycast(ray, out hitInfo, 999f, hitLayerMask))
        {
            debugTarget.position = hitInfo.point;
            mouseRaycastPoint = hitInfo.point;
        }
        if (starterAssetsInputs.aim)
        {
            aimCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            playerAnimatorHandler.SetAnimatorBool("Aiming", true);
            playerAnimatorHandler.UpdateAimMoveParameters(starterAssetsInputs.move.x, starterAssetsInputs.move.y);

            Vector3 worldAimingTarget = mouseRaycastPoint;
            worldAimingTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimingTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            lastFireTime -= Time.deltaTime;
            if (Input.GetMouseButton(0) && lastFireTime <= 0f)
            {
                lastFireTime = shootInterval;
                playerAnimatorHandler.SetAnimatorBool("Shoot", true);
                Vector3 bulletDir = (mouseRaycastPoint - muzzlePoint.position).normalized;
                ShootServerRpc(bulletDir, GetComponent<NetworkObject>().NetworkObjectId, GetComponent<NetworkObject>().OwnerClientId);
            }
            else
            {
                playerAnimatorHandler.SetAnimatorBool("Shoot", false);
            }
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            playerAnimatorHandler.SetAnimatorBool("Aiming", false);
            playerAnimatorHandler.UpdateMoveParameters(starterAssetsInputs.move.normalized.magnitude);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void ShootServerRpc(Vector3 dir, ulong id, ulong ownerID)
    {
        Shoot(dir, id, ownerID);
        ShootClientRpc();
    }

    [ClientRpc]
    void ShootClientRpc()
    {
        Instantiate(muzzleFlare, muzzlePoint.position, muzzlePoint.rotation);
    }

    void Shoot(Vector3 dir, ulong id, ulong ownerID)
    {
        var newprojectile = networkObjectPool.GetNetworkObject(projectile.gameObject, muzzlePoint.position, Quaternion.LookRotation(dir));
        newprojectile.GetComponent<Projectile>().Init(projectileSpeed);
        newprojectile.GetComponent<Projectile>().damage = 10;
        newprojectile.GetComponent<Projectile>().teamID.Value = id;
        newprojectile.GetComponent<Projectile>().ownerID = ownerID;
        newprojectile.GetComponent<Projectile>().WaitForDisable();
        newprojectile.Spawn(true);
        // StartCoroutine(ReturnProjectileObject(newprojectile, projectile.gameObject));
        // newprojectile.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
    }

    IEnumerator ReturnProjectileObject(NetworkObject networkObject, GameObject prefab)
    {
        yield return new WaitForSeconds(5f);
        networkObjectPool.ReturnNetworkObjectWithUse(networkObject, prefab);
    }
}
