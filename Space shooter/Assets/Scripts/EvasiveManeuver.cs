using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rigidbody;

	void Start () {
        StartCoroutine(Evade());
        rigidbody = GetComponent<Rigidbody>();
        currentSpeed = rigidbody.velocity.z;
	}

    IEnumerator Evade() {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true) {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    private void FixedUpdate() {
        float newManeuver = Mathf.MoveTowards(rigidbody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rigidbody.velocity = new Vector3(newManeuver, 0, currentSpeed);

        Vector3 enemyPosition = Camera.main.WorldToScreenPoint(transform.position);

        Vector3 clampedScreenSpacePosition = new Vector3(Mathf.Clamp(enemyPosition.x, 0.0f, Screen.width), enemyPosition.y, enemyPosition.z);

        Vector3 clampedWorldSpacePosition = Camera.main.ScreenToWorldPoint(clampedScreenSpacePosition);

        rigidbody.position = new Vector3(clampedWorldSpacePosition.x, 0.0f, clampedWorldSpacePosition.z);

        rigidbody.rotation = Quaternion.Euler(0, 0, -1 * rigidbody.velocity.x);
    }
}