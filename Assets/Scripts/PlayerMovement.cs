using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region PUBLIC_VARIABLES

    public float hSpeed = 10f;
    public float vSpeed = 10f;
    public Rigidbody mRigidbody;

    #endregion

    #region PRIVATE_VARIABLES

    private float _horizontalMovement;
    private float _verticalMovement;

    #endregion

    #region UNITY_EVENTS

    void Start()
    {
        //
    }

    #endregion


    void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        var targetPositon = new Vector3(_verticalMovement*vSpeed, 0f, -_horizontalMovement*hSpeed)*Time.deltaTime;
        mRigidbody.MovePosition(mRigidbody.position + targetPositon);
    }
}
