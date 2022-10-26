using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 
/// </summary>
[System.Serializable]public class Pool
{
    public GameObject Prefab => prefab;
    public int Size => size;
    public int RuntimeSize => queue.Count;
    [SerializeField] GameObject prefab;
    [SerializeField] int size = 1;
    Queue<GameObject> queue;
    Transform parent;
    public void Initialize(Transform parent)
    {
        queue = new Queue<GameObject>();
        this.parent = parent;
        for (int i = 0; i < size; i++)
        {
            queue.Enqueue(Copy());
        }
    }
    private GameObject Copy()
    {
        var copy = GameObject.Instantiate(prefab,parent);
        copy.SetActive(false);
        return copy;
    }
    private GameObject AvailableObject()
    {
        GameObject availableObject = null;
        if(queue.Count > 0 && !queue.Peek().activeSelf)
        {
            availableObject = queue.Dequeue();
        }
        else
        {
            //queue.Enqueue(Copy());
            availableObject = Copy();
        }
        queue.Enqueue(availableObject);
        return availableObject;
    }
    public GameObject PreparedObject()
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position,Quaternion rotation)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        return preparedObject;
    }
    public GameObject PreparedObject(Vector3 position,Quaternion rotation,Vector3 localScale)
    {
        GameObject preparedObject = AvailableObject();
        preparedObject.SetActive(true);
        preparedObject.transform.position = position;
        preparedObject.transform.rotation = rotation;
        preparedObject.transform.localScale = localScale;
        return preparedObject;
    }
    public void Return(GameObject gameObject)
    {
        //gameObject.SetActive(false);
        queue.Enqueue(gameObject);
    }
}
