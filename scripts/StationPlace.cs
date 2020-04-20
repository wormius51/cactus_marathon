using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationPlace : MonoBehaviour
{
    public float sellRatio = 0.5f;
    public float fireIntervalMultiplier = 0.5f;
    public float intensityMultiplierMultiplier = 1.5f;
    public GameObject spawOptions;
    public GameObject upgradeOptions;
    public UpgradeOption[] upgradeOptionsChildren;
    public Transform spawnPoint;
    public Station station {get; private set;}

    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void interact() {
        if (active) {
            unInteract();
            return;
        }
        if (station == null) {
            spawOptions.SetActive(true);
            upgradeOptions.SetActive(false);
        } else {
            upgradeOptions.SetActive(true);
            spawOptions.SetActive(false);
        }
        active = true;
    }

    public void unInteract() {
        active = false;
        spawOptions.SetActive(false);
        upgradeOptions.SetActive(false);
    }

    public void spawStation(Station stationPrefab) {
        if (station == null) {
            station = Instantiate(stationPrefab.gameObject).GetComponent<Station>();
            station.transform.position = spawnPoint.position;
            GameManager.instance.money -= station.price;
        }
    }

    public void sell() {
        if (station != null) {
            GameManager.instance.money += (int)(station.price * sellRatio);
            Destroy(station.gameObject);
            foreach (UpgradeOption upgradeOption in upgradeOptionsChildren) {
                upgradeOption.resetPrice();
            }
        }
    }

    public void upgradeSpeed() {
        if (station != null) {
            station.fireInterval *= fireIntervalMultiplier;
        }
    }

    public void upgradeIntensity() {
        if (station != null) {
            station.intensityMultiplier *= intensityMultiplierMultiplier;
        }
    }
}
