using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // ใส่ลูกกอล์ฟตรงนี้
    public Vector3 offset = new Vector3(0, 5, -10); // ตำแหน่งกล้องสัมพันธ์กับลูก
    public float followSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // กล้องหันมองลูกตลอด แต่ไม่หมุนตามการหมุนของลูก
        transform.LookAt(target.position);
    }
}
