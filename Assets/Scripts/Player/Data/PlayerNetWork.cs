using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetWork : NetworkBehaviour
{
    NetworkVariable<PlayerNetWorkData> netState = new(writePerm: NetworkVariableWritePermission.Owner);

    Vector3 vel;

    float rotVel;

    [SerializeField] float cheapInterpolationTime = 0.1f;

    PlayerStatus playerStatus;

    private void Awake()
    {
        playerStatus = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        if (playerStatus.death)
        {
            return;
        }
        print("SyncTransform" + playerStatus.gameObject.transform.position);
        if (IsOwner)
        {
            netState.Value = new PlayerNetWorkData()
            {
                Position = transform.position,
                Rotation = transform.rotation.eulerAngles
            };
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, netState.Value.Position, ref vel, cheapInterpolationTime);
            transform.rotation = Quaternion.Euler
            (
                0,
                Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, netState.Value.Rotation.y, ref rotVel, cheapInterpolationTime),
                0
            );
        }
    }

    struct PlayerNetWorkData : INetworkSerializable
    {
        float x, y, z;
        short yRot;

        internal Vector3 Position
        {
            get => new Vector3(x, y, z);
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
        }

        internal Vector3 Rotation
        {
            get => new Vector3(0, yRot, 0);
            set => yRot = (short)value.y;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref x);
            serializer.SerializeValue(ref y);
            serializer.SerializeValue(ref z);
            serializer.SerializeValue(ref yRot);
        }
    }
}
