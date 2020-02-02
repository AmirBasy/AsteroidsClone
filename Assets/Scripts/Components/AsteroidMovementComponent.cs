using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Cordinates
{
    N,
    S,
    E,
    W,
    NE, 
    NW,
    SE,
    SW
}

public class AsteroidMovementComponent : Component
{
    private Rigidbody _rb;

    private Dictionary<Cordinates, Vector3> _directions = new Dictionary<Cordinates, Vector3>();

    private Vector3 _direction = new Vector3(0, 0, 0);
    private Vector3 _rotation = new Vector3(0, 0, 0);

    public float _movementSpeed = 1;
    public float _rotationSpeed = 1;

    protected void Awake()
    {
        //_rb = this.gameObject.GetComponent<Rigidbody>();
        _rb = gameObject.AddComponent<Rigidbody>();

        _rb.useGravity = false;

        _directions.Add(Cordinates.N, new Vector3(0, 1, 0));
        _directions.Add(Cordinates.S, new Vector3(0, -1, 0));
        _directions.Add(Cordinates.E, new Vector3(1, 0, 0));
        _directions.Add(Cordinates.W, new Vector3(-1, 0, 0));
        _directions.Add(Cordinates.NE, new Vector3(1, 1, 0));
        _directions.Add(Cordinates.NW, new Vector3(-1, 1, 0));
        _directions.Add(Cordinates.SE, new Vector3(1, -1, 0));
        _directions.Add(Cordinates.SW, new Vector3(-1, -1, 0));
    }
    protected void Start()
    {
        Initializer();
    }
    protected void Update()
    {
        UpdateMovement();
        UpdateRotationMovement();
    }

    protected void Initializer()
    {
        int _r = Random.Range(0, 1);
        Cordinates _d = (Cordinates)Random.Range(0, 7);

        _direction = _directions[_d]; 

        if (_r == 0) _rotation = new Vector3(0, 0, -1);
        else _rotation = new Vector3(0, 0, 1);
    }
    private void UpdateMovement()
    {
        _rb.AddRelativeForce(_direction * _movementSpeed * Time.deltaTime);
    }
    private void UpdateRotationMovement()
    {
        _rb.AddRelativeTorque(_rotation * _rotationSpeed * Time.deltaTime);
    }
    public void ChangeMovementDirection(Vector3 _hitPoint)
    {
        _direction = GetNewDirection(_hitPoint);
    }

    /*Functions*/
    private Vector3 GetNewDirection(Vector3 _hitPoint)
    {
        return -(_hitPoint.normalized);
    }
}

