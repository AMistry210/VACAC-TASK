using UnityEngine;
using UnityEngine.UI;

public class DeliveryCounterUI : MonoBehaviour
{
    public Text counterText;

    void Update()
    {
        counterText.text = "Products delivered: " + Product.deliveredCount;
    }
}