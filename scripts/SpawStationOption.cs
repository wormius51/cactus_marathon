using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawStationOption : StationPlaceOption
{
    public Station stationPrefab;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = stationPrefab.transform.name;
        priceText.text = stationPrefab.price + "";
    }

    // Update is called once per frame
    void Update()
    {
        Color textsColor = GameManager.instance.money >= stationPrefab.price ? availableColor : notEnoughMoneyColor;
        nameText.color = textsColor;
        priceText.color = textsColor;
    }

    private void interact() {
        if (GameManager.instance.money >= stationPrefab.price) {
            stationPlace.spawStation(stationPrefab);
        }
    }
}
