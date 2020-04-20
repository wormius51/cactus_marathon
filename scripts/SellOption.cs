using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellOption : StationPlaceOption
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        priceText.text = (stationPlace.station.price * stationPlace.sellRatio) + "";
    }

    private void interact() {
        stationPlace.sell();
    }
}
