using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : Controller
{
    [Header("Movement")]
    public GameObject _spaceship;

    public float _maxVelocity = 3;
    public float _rotationSpeed = 3;

    private Rigidbody2D rb;

    [Header("Runtime")]
    public float _currentSpeed;

    [Header("Melee")]
    private bool _canShot = true;
    private bool _isShoting;
    private ProjectileManager _pm;
    public GameObject _gunOffset;

    

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();

        rb.mass = 1f;
        rb.gravityScale = 0.0f;
        rb.angularDrag = 1f;

        _pm = this.gameObject.GetComponent<ProjectileManager>();

    }

    protected new void Update()
    {
        _forwardAxis = Input.GetAxis("Vertical");
        _rightAxis = Input.GetAxis("Horizontal");

        ThrustForward(_forwardAxis);
        Rotate(transform, _rightAxis * _rotationSpeed);
        ClampVelocity();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _pm.SpawnProjectile(_gunOffset);
            Debug.Log(_gunOffset.transform.position);
        }
        
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -_maxVelocity, _maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -_maxVelocity, _maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    private void ThrustForward(float amount)
    {

        rb.AddRelativeForce(Vector2.up * amount * 3);
        
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, amount, 0);
    }

    

}
