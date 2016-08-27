using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float verticalDiff = 10f;
    public float elevationAngle = 40f;
    public float angle = 60f;

    private float horizontalDiff;
    private float s;
    private float c;
    private float lengthRatios;
    private Vector2 dir;

    void Start()
    {
        HorizontalCalc();
    }

    void LateUpdate()
    {
        HorizontalCalc();

        s = Mathf.Sin(angle*Mathf.Deg2Rad);
        c = Mathf.Cos(angle*Mathf.Deg2Rad);
        dir = new Vector2(s, c).normalized*horizontalDiff;
        var targetPos = Vector3.zero;
        targetPos.x = target.position.x - dir.x;
        targetPos.z = target.position.z - dir.y;
        targetPos.y = target.position.y + verticalDiff;
        transform.position = targetPos;

        transform.LookAt(target);

    }

    void HorizontalCalc()
    {
        horizontalDiff = verticalDiff/Mathf.Tan(elevationAngle*Mathf.Deg2Rad);
    }
    
}
