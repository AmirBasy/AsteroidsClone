using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour, ICreation
{
    public int scoreToGive = 10;
    public int scoreDestroyed = 100;

    public AudioClip sound_asteroidSplit;
    public AudioClip sound_asteroidDestroy;

    [HideInInspector] public System.Action<AudioClip> OnSound;

    protected GameManager gameManager;

    protected virtual void Awake()
    {
        gameManager = GameManager.instance;
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        //if reached limit call crossScreen
        if (other.gameObject.CompareTag("Limit"))
            CrossScreen(this.transform);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if hit shot
        if (other.gameObject.CompareTag("Shot"))
        {
            HittedShot(other);
        }
        
        //if hit player, add damage
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.AddDamage();
        }
    }

    #region protected API

    protected virtual void HittedShot(Collider other)
    {
        Shot shot = other.GetComponentInParent<Shot>();

        //only if is a player shot (not alien)
        if (shot.playerShot)
        {
            //check size, than split or destroy
            if (transform.localScale.sqrMagnitude > 10)
            {
                gameManager.AddScore(scoreToGive);

                Vector3 shotDirection = shot.direction;
                SplitAsteroid(shotDirection);

                //sound
                OnSound(sound_asteroidSplit);
            }
            else
            {
                gameManager.AddScore(scoreDestroyed);

                Die();

                //sound
                OnSound(sound_asteroidDestroy);
            }

            //destroy shot
            Destroy(other.gameObject);
        }
    }

    protected virtual void CrossScreen(Transform tr)
    {
        //if out of the screen, teleport to the other side
        tr.position = gameManager.CrossScreen(tr.position, 1, 0);
    }

    #region split

    protected virtual void SplitAsteroid(Vector3 shotDirection)
    {
        //get asteroids spawn to right and left from the directon of the shot
        Vector3 forward = shotDirection;
        Vector3 right = Vector3.Cross(forward, Vector3.up);
        Vector3 left = -right;

        //create two half asteroids
        CreateHalfAsteroid(right);
        CreateHalfAsteroid(left);

        //destroy this asteroid
        Die();
    }

    protected virtual void CreateHalfAsteroid(Vector3 pos)
    {
        //create half asteroid
        GameObject halfAsteroid = Instantiate(gameObject);

        Vector3 position = GetPositionHalfAst(pos);
        Vector3 size = GetSizeHalfAst();
        Vector3 direction = GetDirectionHalfAst(pos);
        float speed = GetSpeedHalfAst();

        halfAsteroid.GetComponent<ICreation>().Create(position, size, direction, speed, OnSound);
    }

    protected virtual Vector3 GetPositionHalfAst(Vector3 pos)
    {
        return transform.position + pos;
    }

    protected virtual Vector3 GetSizeHalfAst()
    {
        return transform.localScale / 2;
    }

    protected virtual Vector3 GetDirectionHalfAst(Vector3 pos)
    {
        return pos;
    }

    protected virtual float GetSpeedHalfAst()
    {
        return Random.Range(300, 500);
    }

    #endregion

    protected virtual void Die()
    {
        //remove from list and destroy asteroid
        gameManager.asteroids.Remove(this.gameObject);

        Destroy(gameObject);
    }

    #endregion

    #region public API

    public virtual void Create(Vector3 position, Vector3 size, Vector3 direction, float speed, System.Action<AudioClip> soundFunction)
    {
        //add asteroid to the list
        gameManager.asteroids.Add(this.gameObject);

        transform.position = position;
        transform.localScale = size;
        OnSound = soundFunction;

        //add force to rigidbody in random direction
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * speed);
    }

    #endregion
}
