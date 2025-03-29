using UnityEngine;

public class MeteorMover : MonoBehaviour
{
    public float moveDistance = 5f;      // ระยะที่เคลื่อน
    public float moveSpeed = 2f;         // ความเร็ว
    private Vector3 startPos;
    private bool goingForward = true;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = startPos + transform.forward * movement;
    }
}
