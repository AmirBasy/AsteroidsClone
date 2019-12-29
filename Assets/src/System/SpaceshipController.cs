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

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();

        rb.mass = 1f;
        rb.gravityScale = 0.0f;
        rb.angularDrag = 1f;

    }

    protected new void Update()
    {
        _forwardAxis = Input.GetAxis("Vertical");
        _rightAxis = Input.GetAxis("Horizontal");

        ThrustForward(_forwardAxis);
        Rotate(transform, _rightAxis * _rotationSpeed);
        ClampVelocity();
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
        Debug.Log(Vector2.up * amount);
        
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, amount, 0);
    }

}
