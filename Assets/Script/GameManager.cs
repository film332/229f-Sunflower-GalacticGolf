using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Transform currentBall;  // ตัวลูกกอล์ฟที่ใช้งานอยู่

    void Awake()
    {
        if (instance == null) instance = this;
    }

    public void SetCurrentBall(Transform ball)
    {
        currentBall = ball;
    }
}
