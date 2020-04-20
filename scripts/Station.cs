using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    public float fireInterval = 1;
    public float fireVelocity = 1;
    public float intensityMultiplier = 1;
    public int price = 50;
    public GameObject projectilePrefab;
    private Transform target;
    private float timeSinceFired;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceFired += Time.deltaTime;
        if (timeSinceFired >= fireInterval) {
            if (target != null) {
                timeSinceFired = 0;
                fire();
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.transform.tag.Equals("runner")) {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.transform == target) {
            target = null;
        }
    }

    private void fire() {
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = transform.position;
        Vector3 velocity = target.position - projectile.transform.position;
        velocity.Normalize();
        velocity *= fireVelocity;
        projectile.GetComponent<Rigidbody>().velocity = velocity;
        projectile.GetComponent<Projectile>().intensity *= intensityMultiplier;
    }
}
