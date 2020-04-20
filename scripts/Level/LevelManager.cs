using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance {get; private set;}

    public Transform spawnErea;
    public Transform[] pathPoints;
    public Wave[] waves;
    public int waveIndex {get; private set;}
    private float timeSinceSpawn;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (GameManager.instance != null) {
            GameManager.instance.score = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (waveIndex < waves.Length) {
            GameManager.instance.updateWaveTimer(waves[waveIndex].timeTillStart - timeSinceSpawn);
            if (timeSinceSpawn >= waves[waveIndex].timeTillStart)
                startWave();
            
        } 
    }

    public void startWave() {
        if (waveIndex < waves.Length) {
            GameManager gm = GameManager.instance;
            spawnPrefab(gm.cactusPrefab, waves[waveIndex].cactuses);
            spawnPrefab(gm.fatusPrefab, waves[waveIndex].fatuses);
            waveIndex++;
            timeSinceSpawn = 0;
        }
    }

    private void spawnPrefab(GameObject prefab, int amount) {
        for (int i = 0; i < amount; i++) {
            spawnPrefab(prefab);
        }
    }

    private void spawnPrefab(GameObject prefab) {
        GameObject g = Instantiate(prefab);
        float x = spawnErea.position.x + (Random.Range(-1, 1) * spawnErea.localScale.x / 2);
        float z = spawnErea.position.z + (Random.Range(-1, 1) * spawnErea.localScale.z / 2);
        float y = spawnErea.position.y;
        g.transform.position = new Vector3(x, y, z);
    }

    public int getMaxScore() {
        GameManager gm = GameManager.instance;
        int sum = 0;
        foreach(Wave wave in waves) {
            sum += wave.cactuses * gm.cactusValue;
            sum += wave.fatuses * gm.fatusValue;
        }
        return sum;
    }
}
