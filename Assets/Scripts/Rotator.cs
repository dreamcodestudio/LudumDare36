using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public Vector3 angles;

    void Update()
    {
        transform.Rotate(angles * Time.deltaTime);
    }
}
