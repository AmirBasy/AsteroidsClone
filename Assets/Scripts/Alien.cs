using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, ICreation
{
    public int scoreToGive = 1000;
    public float speed = 3;
    public float rateOfFire = 5;
    public GameObject shotReference;

    public AudioClip sound_alienShot;
    public AudioClip sound_alienDestroy;

    [HideInInspector] public System.Action<AudioClip> OnSound;

    protected Vector3 direction;
    protected float timerShot;

    protected GameManager gameManager;

    protected virtual void Awake()
    {
        gameManager = GameManager.instance;

        timerShot = Time.time + rateOfFire;
    }

    protected virtual void Update()
    {
        Move(transform);
        timerShot = CheckShot(timerShot, rateOfFire);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if hit shot
        if (other.gameObject.CompareTag("Shot"))
        {
            HittedShot(other);
        }

        //if hit player, add damage
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.AddDamage();
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        //if reached limit call crossScreen
        if (other.gameObject.CompareTag("Limit"))
            CrossScreen(this.transform);
    }

    #region protected API

    protected virtual void HittedShot(Collider other)
    {
        Shot shot = other.GetComponentInParent<Shot>();

        //only if is a player shot (not alien)
        if (shot.playerShot)
        {
            //add score
            gameManager.AddScore(scoreToGive);

            //destroy shot
            Destroy(other.gameObject);

            //destroy alien
            Die();
        }
    }

    protected virtual void Move(Transform tr)
    {
        tr.position += direction * speed * Time.deltaTime;
    }

    protected virtual float CheckShot(float time, float rate)
    {
        //check timer than shot
        if (time < Time.time)
        {
            time = Time.time + rate;

            Shot(shotReference);
        }

        return time;
    }

    protected virtual void Shot(GameObject prefab)
    {
        //instantiate and set shot
        GameObject newShot = IstantiateShot(prefab);
        bool isPlayerShot = false;

        newShot.GetComponent<Shot>().CreateShot(GetDownPosition(this.transform), GetPlayerDirection(this.transform), isPlayerShot);

        //sound
        OnSound(sound_alienShot);
    }

    protected virtual GameObject IstantiateShot(GameObject prefab)
    {
        //spawn shot
        return Instantiate(prefab);
    }

    protected virtual Vector3 GetDownPosition(Transform tr)
    {
        //the back of the alien (looking from up, it is the down of the alien)
        return tr.position - tr.forward;
    }

    protected virtual Vector3 GetPlayerDirection(Transform tr)
    {
        //from spawn shot to player
        return (gameManager.actualShip.transform.position - GetDownPosition(tr)).normalized;
    }


    protected virtual void CrossScreen(Transform tr)
    {
        //if out of the screen, teleport to the other side
        tr.position = gameManager.CrossScreen(tr.position, 1, 0);
    }

    protected virtual void Die()
    {
        //reset in gameManager
        gameManager.DestroyAlien();

        Destroy(gameObject);

        //sound
        OnSound(sound_alienDestroy);
    }

    #endregion

    #region public API

    public virtual void Create(Vector3 position, Vector3 size, Vector3 direction, float speed, System.Action<AudioClip> soundFunction)
    {
        transform.position = position;
        transform.localScale = size;
        this.direction = direction;
        this.speed = speed;
        OnSound = soundFunction;
    }

    #endregion
}
