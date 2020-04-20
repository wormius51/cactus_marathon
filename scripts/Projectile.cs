using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float intensity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void effectRunner(Runner runner) {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.tag.Equals("runner")) {
            effectRunner(other.transform.GetComponent<Runner>());
            Destroy(gameObject);
        }
    }
}
