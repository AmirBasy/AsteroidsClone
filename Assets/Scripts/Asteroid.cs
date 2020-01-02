using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Obstacle
{   
    public Vector3 _originalSize;
    public Vector3 _minSize; //the size value to have for being destroyed

    public float max_radius = 1;
    public float min_radius = 1;
    public float _radius = 0;

    #region Unity Callbacks
    private void Start()
    {
        _originalSize = this.gameObject.transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint _hitpoint = collision.GetContact(0);

        if (collision.gameObject.GetComponent<Projectile>() != null)
        {
            Debug.Log("Projectile!");

            if (this.gameObject.transform.localScale == _minSize)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DecreaseAsteroidScale();
                //GameObject _test = Instantiate(_debrisPrefab);
                //_test.gameObject.transform.position = _hitpoint.point;
                SpawnDebris(GetDebrisSpawnLocations(_hitpoint.point));
            }
        }
        else if (collision.gameObject.GetComponent<Asteroid>() != null)
        {
            
        }

       
    }

    #endregion
    #region Debris Spawner
    public GameObject _debrisPrefab;

    private int _debrisSingleInstanceSpawn;
    private Vector3[] GetDebrisSpawnLocations(Vector3 _collisionPoint)
    {
        int _debrisMaxCount = Random.Range(2, 5);
        _debrisSingleInstanceSpawn = _debrisMaxCount;//record debris

        float _angleScissor = 90f / _debrisMaxCount;

        Vector3 _orientation  = _collisionPoint;

        Vector3[] _result = new Vector3[_debrisMaxCount];

        Vector3 _hp = _collisionPoint;
        Vector3 _direction = _hp - transform.position;

        Vector3 _rotatedDirection = Vector3.zero;

        for (int i = 0; i<_debrisMaxCount; i++)
        {
            if(i==0)
            {
                //_result[i] = new Vector3(Mathf.Cos(-45) + Mathf.Sin(-45), -Mathf.Sin(-45) + Mathf.Cos(-45), 0.0f) * Random.Range(min_radius, max_radius);

                _rotatedDirection = new Vector3(_direction.x * Mathf.Cos(-45) + _direction.y * Mathf.Sin(-45), _direction.x * -Mathf.Sin(-45) + _direction.y * Mathf.Cos(-45), 0.0f);

                _result[i] = _rotatedDirection * (_radius + 1);
                
            }
            else
            {
                //y+++_result[i] = new Vector3(_orientation.x * Mathf.Cos(_angleScissor * (i + 1)) + _orientation.y * Mathf.Sin(_angleScissor * (i + 1)), _orientation.x * -Mathf.Sin(_angleScissor * (i + 1)) + _orientation.y * Mathf.Cos(_angleScissor * (i + 1)), 0.0f);

                _rotatedDirection = new Vector3(_rotatedDirection.x * Mathf.Cos(_angleScissor * (i + 1)) + _rotatedDirection.y * Mathf.Sin(_angleScissor * (i + 1)), _rotatedDirection.x * -Mathf.Sin(_angleScissor * (i + 1)) + _rotatedDirection.y * Mathf.Cos(_angleScissor * (i + 1)), 0.0f);

                _result[i] = _rotatedDirection * (_radius + 1);
                
            }
        }

        return _result;
    }
    private void SpawnDebris(Vector3[] _LocationForSpawn)
    {
        for(int i = 0; i<_debrisSingleInstanceSpawn; i++)
        {
            GameObject _debrisInstance = Instantiate(_debrisPrefab);

            _debrisInstance.transform.position = _LocationForSpawn[i];

            _debrisInstance.gameObject.GetComponent<DebrisMovementComponent>().CALL_RadialDamage(_debrisInstance.gameObject, _LocationForSpawn[i]);
        }
    }
    #endregion
    
    private void DecreaseAsteroidScale()
    {
        gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }
    

}

