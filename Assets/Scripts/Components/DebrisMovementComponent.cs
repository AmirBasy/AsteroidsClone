using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisMovementComponent : MonoBehaviour
{
    public float _speed;

    public float _lifetime;

    public bool _canBeDestroyed;

    #region Unity Callbacks

    #endregion
    #region Movement
    private GameObject _owner;

    private Vector3 _direction;

    private bool _canMove = false;

    private void UpdateMovement()
    {
        if(_canMove)
        {
            ApplyRadialDamage();
        }
    }
    protected void FixedUpdate()
    {
        if (_lifetime > 0.0f)
        {
            ListenTime();

            if(LifetimeIsOver())
            {
                Destroy(_owner.gameObject);
            }

        }
    }

    public void CALL_RadialDamage (GameObject _target, Vector3 _origin)
    {
        _owner = _target;
        _direction = _origin;

        _canMove = true;
    }
    protected void ApplyRadialDamage()
    {
        _owner.gameObject.GetComponent<Rigidbody>().AddRelativeForce(_direction * _speed * Time.deltaTime);
        Debug.Log("force: " + (_direction * _speed * Time.deltaTime));
    }
    #endregion
    #region Timer
    protected float _time;

    protected void ListenTime()
    {
        _time = Time.deltaTime;
    }

    protected bool LifetimeIsOver()
    {
        if (Mathf.Floor(_time) == _lifetime) return true;
        else return false;
    }
    #endregion


}


