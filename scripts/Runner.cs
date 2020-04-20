using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public int value = 1;
    public Transform staminaBar;

    public float maxStamina = 100;

    private float Stamina = 100;
    public float stamina {
        get {
            return Stamina;
        }
        set {
            Stamina = value < maxStamina ? value : maxStamina;
            staminaBar.localScale = new Vector3(1, stamina / maxStamina, 1);
        }
    }

    public float baseSpeed;
    private float speed;

    public float staminaLossRate;

    public float speedChangeRatio = 0.05f;
    public float boostRatio = 1;

    public bool cameraShake;

    private int pathPointIndex;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        speed = baseSpeed;
        animator = GetComponent<Animator>();
        animator.SetFloat("offset", Random.value);
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = LevelManager.instance.pathPoints[pathPointIndex];
        float distance = speed * Time.deltaTime;
        moveTo(target, distance);
        
        stamina -= staminaLossRate * Time.deltaTime;
        if (stamina <= 0) {
            stamina = 0;
            selfDestruct();
        }
        speed = Mathf.Lerp(speed, baseSpeed * stamina / maxStamina, speedChangeRatio);
        animator.speed = speed;
    }

    private void moveTo(Transform target, float distance) {
        Vector3 move = target.position - transform.position;
        move.Normalize();
        transform.position += move * distance;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform == LevelManager.instance.pathPoints[pathPointIndex]) {
            if (pathPointIndex < LevelManager.instance.pathPoints.Length - 1) {
                pathPointIndex++;
            }
        }
        if (other.transform.tag.Equals("finish")) {
            if (cameraShake)
                GameManager.instance.fatusFinished = true;
            GameManager.instance.score += value;
            GameManager.instance.money += value * 10;
            Destroy(gameObject);
        }
    }

    private void selfDestruct() {
        GameManager.instance.missedScore += value;
        Destroy(gameObject);
    }

    public void bootsSpeed(float boost) {
        speed += boost * boostRatio;
    }

    private void onStep() {
        if (cameraShake)
            CamraController.instance.shakeCamera();
    }
}
