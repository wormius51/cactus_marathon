using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamraController : MonoBehaviour
{
    public static CamraController instance {get; private set;}
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shakeCamera() {
        animator.SetBool("shake", true);
    }

    public void stopCameraShake() {
        animator.SetBool("shake", false);
    }
}
