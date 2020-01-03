using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    GameManager _gm;
    public GameObject _offset;
    public GameObject _projectilePrefab;

    private GameObject _projectile;

    [Header("Actor Movement")]
    public Vector3 _velocity = new Vector3(-5, 0, 0);

    [Header("Projectile Movement")]
    public float speed = 5f;

    private float _time;

    #region UnityCallbacks
    protected void Start()
    {
        _gm = Object.FindObjectsOfType<GameManager>()[0]; 
    }
    protected void Update()
    {
        RecordTime();

        if(TimeIsOver(1))
        {
            //_projectile = SpawnProjectile(); FIXME!!

        }

    }
    protected void FixedUpdate()
    {
        if(_projectile!=null)
        {
            LoadDirectionalMovement(_projectile);
        }
        UpdateMovement();
    }
    #endregion
    #region Collisions
    protected void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Projectile>()!=null)
        {
            _gm.AddScore(100);
            Destroy(this.gameObject);
        }
    }
    #endregion
    #region EnemyMovement
    private void UpdateMovement()
    {
        gameObject.transform.position += _velocity * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    #endregion
    #region ProjectileMovement
    private GameObject SpawnProjectile()
    {
        GameObject _item = Instantiate(_projectilePrefab);
        _item.gameObject.transform.position = _offset.gameObject.transform.position;

        return _item;
    }
    private Vector3 GetProjectileDirection()
    {
        return (_offset.gameObject.transform.position - new Vector3(0, -1, 0)).normalized;
    }
    private void LoadDirectionalMovement(GameObject _actor)
    {
        _actor.gameObject.transform.position += GetProjectileDirection() * speed * Time.deltaTime;
    }
    #endregion
    #region Timer
    private void RecordTime()
    {
        _time += Time.deltaTime;
    }
    private void ResetTime()
    {
        _time = 0.0f;
    }
    bool TimeIsOver(float amount)
    {
        if (Mathf.FloorToInt(_time) == amount)
            return true;
        else return false;
    }
    #endregion

}
