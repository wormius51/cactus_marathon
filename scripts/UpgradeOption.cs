using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UpgradeOption : StationPlaceOption
{
    public int startingPrice;
    private int price;
    public float priceMultiplier = 2;
    public UnityEvent action;
    // Start is called before the first frame update
    void Start()
    {
        price = startingPrice;
        priceText.text = price + "";
    }

    // Update is called once per frame
    void Update()
    {
        Color textsColor = GameManager.instance.money >= price ? availableColor : notEnoughMoneyColor;
        nameText.color = textsColor;
        priceText.color = textsColor;
    }

    private void interact() {
        if (GameManager.instance.money >= price) {
            GameManager.instance.money -= price;
            price = (int)(price * priceMultiplier);
            priceText.text = price + "";
            action.Invoke();
        }
    }

    public void resetPrice() {
        price = startingPrice;
        priceText.text = price + "";
    }
}
