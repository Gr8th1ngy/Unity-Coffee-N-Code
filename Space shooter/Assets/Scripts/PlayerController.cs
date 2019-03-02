using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private Rigidbody rigidbody;
    private float nextValidFireTime;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextValidFireTime)
        {
            nextValidFireTime = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = moveVector * speed;

        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 clampedScreenSpacePosition = new Vector3(Mathf.Clamp(playerPosition.x, 0.0f, Screen.width), Mathf.Clamp(playerPosition.y, 0.0f, Screen.height), playerPosition.z);

        Vector3 clampedWorldSpacePosition = Camera.main.ScreenToWorldPoint(clampedScreenSpacePosition);

        rigidbody.position = new Vector3(clampedWorldSpacePosition.x, 0.0f, clampedWorldSpacePosition.z);

        rigidbody.rotation = Quaternion.Euler(0, 0, rigidbody.velocity.x * -tilt);
    }
}