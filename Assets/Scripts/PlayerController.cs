using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 move = new Vector3(moveX, 0, moveZ) * _moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }
}
