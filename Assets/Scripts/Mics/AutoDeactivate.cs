using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 
/// </summary>
public class AutoDeactivate : MonoBehaviour
{
    [SerializeField] bool destroy;
    [SerializeField] float waitTime;
    void OnEnable()
    {
        StartCoroutine(nameof(DisableCoroutine));
    }
    IEnumerator DisableCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        if (destroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
