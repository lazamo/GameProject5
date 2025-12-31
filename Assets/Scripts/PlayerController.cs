using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
public class PlayerController : MonoBehaviour
{
    private Rigidbody PlayerRb;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public float speed = 5.0f;
    private float powerUpForce = 15.0f;
    public bool hasPowerup = false;
    public bool gameIsOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float ForwardInput = Input.GetAxis("Vertical");
        PlayerRb.AddForce(focalPoint.transform.forward * ForwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.1f, 0);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup")) 
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountDownRoutine() 
    {
        yield return new WaitForSeconds(5);
        powerupIndicator.gameObject.SetActive(false);
        hasPowerup = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidBody = collision.rigidbody;
            Vector3 awayFromPLayer = ((collision.gameObject.transform.position - transform.position).normalized);
            enemyRigidBody.AddForce(awayFromPLayer * powerUpForce, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            
        }
    }

    

}
