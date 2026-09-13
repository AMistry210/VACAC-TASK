using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;

    [Header("Next Conveyor(s) — fill both to create a branching split")]
    public Conveyor nextConveyorA;
    public Conveyor nextConveyorB;

    [Header("Filter (leave both ticked to allow everything)")]
    public bool allowBoxes = true;
    public bool allowCans = true;

    public bool AllowsProduct(ProductType type)
    {
        if (type == ProductType.Box) return allowBoxes;
        if (type == ProductType.Can) return allowCans;
        return true;
    }
}