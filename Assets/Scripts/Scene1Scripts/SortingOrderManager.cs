using UnityEngine;

public class SortingOrderManager : MonoBehaviour
{
    public int largestSortingOrder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        largestSortingOrder = 0;
    }

    public int getLargestSortingOrder()
    {
        return largestSortingOrder;
    }

    public void setLargestSortingOrder(int largest)
    {
        largestSortingOrder = largest;
    }
}
