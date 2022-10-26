using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Move : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(new Vector3(10f * Time.deltaTime, 0, 0));
    }
}
