using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : Obstacle
{
    private event Action OnHit;
    private Vector3 _originalSize;
    public Vector3 _minSize; //the size value to have for being destroyed

    GameManager _gm;

    #region Unity Callbacks
    private void Awake()
    {
        OnHit += AudioManager.current.PlayExplotion;
    }
    private void Start()
    {
        _gm = FindObjectsOfType<GameManager>()[0];

        _originalSize = this.gameObject.transform.localScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint _hitpoint = collision.GetContact(0);

        if (collision.gameObject.GetComponent<Projectile>() != null)
        { 

            if (this.gameObject.transform.localScale == _minSize)
            {
                _gm.AddScore(25);
                OnHit.Invoke();
                Destroy(this.gameObject);
            }
            else
            {
                DecreaseAsteroidScale();

                GameObject[] _debrisInstance = new GameObject[5];
                _debrisInstance = SpawnDebris(_hitpoint.point + (_hitpoint.point.normalized * 0.1f));

                for (int i = 0; i< _debrisInstance.Length; i++)
                {
                    _debrisInstance[i].gameObject.GetComponent<DebrisMovementComponent>()._owner = this.gameObject;
                    _debrisInstance[i].gameObject.GetComponent<DebrisMovementComponent>()._direction = _debrisInstance[i].gameObject.GetComponent<DebrisMovementComponent>().GetRotatedVector(_debrisInstance[i].gameObject.GetComponent<DebrisMovementComponent>().GetDirection(), Random.Range(-45,45));
                    _debrisInstance[i].gameObject.GetComponent<DebrisMovementComponent>()._ownerSet = true;
                }

            }
        }

       
    }

    #endregion
    #region Debris Spawner
    [Header("Debris Spawner")]
    public GameObject _debrisPrefab;
    
    public float _radius;

    private GameObject[] SpawnDebris(Vector3 _collision)
    {
        int _itemToSpawn = 5;
        
        GameObject[] _result = new GameObject[_itemToSpawn];

        for (int i =0; i<5; i++)
        {
            _result[i] = Instantiate(_debrisPrefab);

            _result[i].gameObject.transform.position = _collision;
        }

        return _result;
    }

    
    #endregion
    
    private void DecreaseAsteroidScale()
    {
        gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

    


}

