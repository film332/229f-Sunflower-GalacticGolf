using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        transform.LookAt(target.position);
    }

    // ✅ เพิ่มฟังก์ชันนี้ เพื่อให้เรียกจากภายนอกได้
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
