using UnityEngine;

public class ConveyorPlacer : MonoBehaviour
{
    public GameObject[] conveyorPrefabs;
    public LayerMask groundLayer;
    public LayerMask deletableLayer;
    public float snapDistance = 1f;

    private GameObject currentPiece;

    void Update()
    {
        for (int i = 0; i < conveyorPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (currentPiece != null)
                    Destroy(currentPiece);

                currentPiece = Instantiate(conveyorPrefabs[i]);
            }
        }

        if (currentPiece == null && Input.GetKeyDown(KeyCode.Q))
        {
            TryDeleteUnderMouse();
        }

        if (currentPiece == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            currentPiece.transform.position = hit.point;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Renderer rend = currentPiece.GetComponentInChildren<Renderer>();
            Vector3 center = rend != null ? rend.bounds.center : currentPiece.transform.position;
            currentPiece.transform.RotateAround(center, Vector3.up, 90f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            TrySnapToExisting();
            currentPiece = null;
        }
    }

    void TrySnapToExisting()
    {
        Conveyor newConveyor = currentPiece.GetComponent<Conveyor>();

        if (newConveyor == null)
        {
            return;
        }

        Conveyor[] allConveyors = FindObjectsByType<Conveyor>();

        foreach (Conveyor other in allConveyors)
        {
            if (other == newConveyor)
                continue;

            float dist = Vector3.Distance(newConveyor.startPoint.position, other.endPoint.position);

            if (dist <= snapDistance)
            {
                Vector3 offset = currentPiece.transform.position - newConveyor.startPoint.position;
                currentPiece.transform.position = other.endPoint.position + offset;

                if (other.nextConveyorA == null)
                {
                    other.nextConveyorA = newConveyor;
                    Debug.Log("Linked new piece into " + other.name + " as Next Conveyor A");
                }
                else if (other.nextConveyorB == null)
                {
                    other.nextConveyorB = newConveyor;
                    Debug.Log("Linked new piece into " + other.name + " as Next Conveyor B");
                }
                else
                {
                    Debug.LogWarning(other.name + " already has both Next Conveyor A and B filled — new piece placed but NOT linked to anything.");
                }



                return;
            }
        }

        Debug.LogWarning("No existing conveyor endpoint found within snap distance (" + snapDistance + ") — new piece placed but NOT linked to anything.");
    }

    void TryDeleteUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, deletableLayer))
        {
            Destroy(hit.transform.root.gameObject);
        }
    }
}