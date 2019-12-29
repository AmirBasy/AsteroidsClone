using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementComponent : MonoBehaviour
{
    public GameObject _spaceship;
    public GameObject _gunOffset;

    private Vector3 _dir;

    private void Awake()
    {
        _spaceship = GameObject.Find("Ship");
        _gunOffset = GameObject.Find("gun offset");
    }

    private Vector3 GetProjectileDirection()
    {
        return (_gunOffset.transform.position - _spaceship.transform.position).normalized;
    }

    public void UpdateProjectileMovement(Projectile _projectile, float _speed)
    {

        if(_dir == new Vector3 (0,0,0))
        {
            _dir = GetProjectileDirection();
        }

        _projectile.transform.position += _dir * _speed * Time.deltaTime;

    }


}
