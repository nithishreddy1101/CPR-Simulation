using UnityEngine;

public class MoveTwoDown : MonoBehaviour
{
    [SerializeField] private Transform transformA;
    [SerializeField] private Transform transformB;
    [SerializeField] private float moveDistance = 0.01f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transformA != null)
            {
                transformA.position += Vector3.down * moveDistance;
            }

            if (transformB != null)
            {
                transformB.position += Vector3.down * moveDistance;
            }

            Debug.Log("Moved both transforms down.");
        }
    }
}
