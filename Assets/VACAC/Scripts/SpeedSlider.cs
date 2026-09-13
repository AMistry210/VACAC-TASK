using UnityEngine;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    public Slider speedSlider;

    private Conveyor[] allConveyors;

    void Start()
    {
        allConveyors = FindObjectsByType<Conveyor>();
    }

    void Update()
    {
        foreach (Conveyor conveyor in allConveyors)
        {
            if (conveyor != null)
                conveyor.speed = speedSlider.value;
        }
    }
}