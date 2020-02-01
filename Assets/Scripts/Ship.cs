using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int life = 3;
    public float acceleration = 6000;
    public float rotationVelocity = 1800;
    public bool invincible = false;
    public GameObject shotReference;

    public AudioClip sound_shipDestroy;
    public AudioClip sound_shipShot;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public System.Action<AudioClip> OnPlayerSound;

    protected Rigidbody rb;
    protected GameManager gameManager;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody>();

        gameManager = GameManager.instance;

        if(gameManager != null)
            gameManager.GetPlayerSoundFunction();
    }
    
    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.A)) Rotate(rb, -transform.up);
        if (Input.GetKey(KeyCode.D)) Rotate(rb, transform.up);
        if (Input.GetKey(KeyCode.W)) Accelerate(rb, transform.forward);
        if (Input.GetKeyDown(KeyCode.Space)) Shot(shotReference);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if hit shot
        if (other.gameObject.CompareTag("Shot"))
        {
            HittedShot(other);
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

        //if alien shot
        if (!shot.playerShot)
        {
            gameManager.AddDamage();
        }
    }

    protected virtual void Rotate(Rigidbody rbToMove, Vector3 direction)
    {
        rbToMove.AddTorque(direction * rotationVelocity * Time.deltaTime);
    }

    protected virtual void Accelerate(Rigidbody rbToMove, Vector3 direction)
    {
        rbToMove.AddForce(direction * acceleration * Time.deltaTime);
    }

    protected virtual void Shot(GameObject prefab)
    {
        //instantiate and set shot
        GameObject newShot = IstantiateShot(prefab);
        bool isPlayerShot = true;

        newShot.GetComponent<Shot>().CreateShot(GetForwardPosition(this.transform), GetForward(this.transform), isPlayerShot);

        //sound
        OnPlayerSound(sound_shipShot);
    }

    protected virtual GameObject IstantiateShot(GameObject prefab)
    {
        //spawn shot
        return Instantiate(prefab);
    }

    protected virtual Vector3 GetForward(Transform tr)
    {
        return tr.forward;
    }

    protected virtual Vector3 GetForwardPosition(Transform tr)
    {
        return tr.position + tr.forward;
    }

    /// <summary>
    /// if reached limit of the screen, teleport to the other side
    /// </summary>
    protected virtual void CrossScreen(Transform tr)
    {
        tr.position = gameManager.CrossScreen(tr.position, 1, 0);
    }

    #endregion
}
