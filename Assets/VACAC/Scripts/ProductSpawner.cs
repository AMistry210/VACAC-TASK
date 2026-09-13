using UnityEngine;

public class ProductSpawner : MonoBehaviour
{
    public GameObject[] productPrefabs;
    public Conveyor startingConveyor;
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnProduct), 0f, spawnInterval);
    }

    void SpawnProduct()
    {
        int index = Random.Range(0, productPrefabs.Length);

        if (productPrefabs[index] == null)
        {
            Debug.LogWarning("Product Prefabs slot " + index + " is empty or broken — skipping this spawn.");
            return;
        }

        GameObject obj = Instantiate(productPrefabs[index]);
        Product product = obj.GetComponent<Product>();
        product.startingConveyor = startingConveyor;
    }
}