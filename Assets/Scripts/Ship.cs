using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody rigibody;
    public float rotationVelocity = 45;
    public float acceleration;
    public GameObject shotReference;
    public int Life;
    public GameManager gameManager;
    public Camera cam;
    float lastHit;
    float attualetime;
    float endshield;
    public GameObject GunOffset;
    public bool ReciveDamage;
    public float time;

    private void Awake()
    {

    }

    void Rotate(float direction)
    {
        rigibody.AddTorque(transform.up * rotationVelocity * Time.deltaTime * direction);
        //transform.Rotate(Vector3.up * rotationVelocity * Time.deltaTime * direction);
    }

    void Shot()
    {
        GameObject newShot = GameObject.Instantiate(shotReference);
        newShot.gameObject.GetComponent<Shot>().direction = (GameObject.FindObjectOfType<Ship>().GunOffset.transform.position - GameObject.FindObjectOfType<Ship>().gameObject.transform.position).normalized;
        newShot.gameObject.transform.position = GunOffset.transform.position; 
    }

    void Accelerate()
    {
        rigibody.AddForce(transform.forward * acceleration * Time.deltaTime);
        //transform.Translate(Vector3.forward * acceleration * Time.deltaTime);
    }

    void Die()
    {

    }

    void CrossScreen()
    {

        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        //if out of the screen, teleport to the other side

        if (screenPoint.x > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(0, screenPoint.y, screenPoint.z));
        }
        else if (screenPoint.x < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(1, screenPoint.y, screenPoint.z));
        }
        else if (screenPoint.y > 1)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 0, screenPoint.z));
        }
        else if (screenPoint.y < 0)
        {
            transform.position = cam.ViewportToWorldPoint(new Vector3(screenPoint.x, 1, screenPoint.z));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Asteroids>())
        {
            if (CheckTime(2.0f) && !ReciveDamage)
            {
                ReciveDamage = true;
                ResetTime();
            }
            if (ReciveDamage)
            {
                if (Life <= 1)
                {
                    Life -= 1;
                    gameManager.UI.SetCurrentShipLife();
                    gameManager.UI.activeUi();
                    Destroy(this.gameObject);

                }
                else
                {
                    Life -= 1;
                    ReciveDamage = false;
                    transform.position = new Vector3(0, 0, 0);
                    gameManager.UI.SetCurrentShipLife();
                    gameManager.UI.activeUi();
                }
            }
          
        }
    }

    private void RecordTime()
    {
        time += Time.deltaTime;
    }

    private bool CheckTime(float valueToCheck)
    {
        if (time >= valueToCheck)
            return true;
        else return false;
    }

    private void ResetTime()
    {
        time = 0.0f;
    }


    // Start is called before the first frame update
    void Start()
    {
        ReciveDamage = true;
        cam = FindObjectOfType<Camera>();
        //from world point to viewport point

    }

    // Update is called once per frame
    void Update()
    {
        if(ReciveDamage == false)
        {
            RecordTime();
        }

        CrossScreen();

        if (Input.GetKey(KeyCode.A)) Rotate(-1);
        if (Input.GetKey(KeyCode.D)) Rotate(1);
        if (Input.GetKey(KeyCode.W)) Accelerate();
        if (Input.GetKeyDown(KeyCode.Space)) Shot();
      
    }
}
