using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMovementComponent : MonoBehaviour
{
    [Header("Movement")]
    public float _speed;

    public bool _ownerSet = false;

    public GameObject _owner;

    public Vector3 _direction;

    #region Unity Callbacks

    protected void FixedUpdate()
    {
        if (_ownerSet)
        {
            UpdateMovement();
        }
    }



    #endregion
    #region Movement
    public void UpdateMovement()
    {

        this.gameObject.transform.position += _direction * _speed * Time.deltaTime;

    }
    public Vector3 GetDirection()
    {
        Vector3 _result = this.gameObject.transform.position - _owner.gameObject.transform.position;

        return _result.normalized;

    }
    public Vector3 GetRotatedVector(Vector3 _v, float _angle)
    {
        return new Vector3(_v.x * Mathf.Cos(_angle) + _v.y * Mathf.Sin(_angle), _v.x * -Mathf.Sin(_angle) + _v.y * Mathf.Cos(_angle), 0.0f);
    }
    #endregion

}


