using UnityEngine;

public enum ProductType { Box, Can }

public class Product : MonoBehaviour
{
    public ProductType type;
    public Conveyor startingConveyor;
    public float speed = 2f;

    public static int deliveredCount = 0;

    private Conveyor currentConveyor;
    private float progress = 0f;

    void Start()
    {
        if (startingConveyor != null)
        {
            currentConveyor = startingConveyor;
            progress = 0f;

            transform.position = currentConveyor.startPoint.position;
        }
    }

    void Update()
    {
        if (currentConveyor == null)
            return;

        Vector3 start = currentConveyor.startPoint.position;
        Vector3 end = currentConveyor.endPoint.position;
        float distance = Vector3.Distance(start, end);

        progress += (currentConveyor.speed / distance) * Time.deltaTime;

        transform.position = Vector3.Lerp(start, end, progress);

        Vector3 direction = (end - start).normalized;
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

        if (progress >= 1f)
        {
            bool deadEnd = currentConveyor.nextConveyorA == null && currentConveyor.nextConveyorB == null;

            Conveyor next = ChooseNextConveyor(currentConveyor);

            if (next != null)
            {
                currentConveyor = next;
                progress = 0f;

                transform.position = currentConveyor.startPoint.position;
            }
            else if (deadEnd)
            {
                deliveredCount++;
                Debug.Log("DELIVERED " + type + " — total now " + deliveredCount);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("REJECTED " + type + " at " + currentConveyor.name);
                Destroy(gameObject);
            }
        }
    }

    Conveyor ChooseNextConveyor(Conveyor current)
    {
        if (current.nextConveyorA != null && current.nextConveyorA.AllowsProduct(type))
            return current.nextConveyorA;

        if (current.nextConveyorB != null && current.nextConveyorB.AllowsProduct(type))
            return current.nextConveyorB;

        return null;
    }
}