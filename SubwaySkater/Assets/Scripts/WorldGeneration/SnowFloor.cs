using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;
    [SerializeField] private float offsetSpeed = 0.115f;

    private void Update()
    {
        transform.position = Vector3.forward * player.transform.position.z;
        material.SetVector("_Offset", new Vector2(0, -transform.position.z * offsetSpeed));
    }
}
