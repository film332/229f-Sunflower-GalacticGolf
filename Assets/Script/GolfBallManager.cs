using UnityEngine;
using TMPro;
[System.Serializable]
public class GolfBallManager : MonoBehaviour
{
    public GolfBallData[] balls;
    public Transform spawnPoint;
    public TextMeshProUGUI currentBallText;

    private GameObject currentBall;
    private int currentIndex = 0;

    void Start()
    {
        SpawnBall(0);
    }

    public void NextBall()
    {
        currentIndex = (currentIndex + 1) % balls.Length;
        SpawnBall(currentIndex);
    }

    void SpawnBall(int index)
    {
        if (currentBall != null)
            Destroy(currentBall);

        GolfBallData data = balls[index];
        currentBall = Instantiate(data.ballPrefab, spawnPoint.position, Quaternion.identity);

        // ตั้งค่ามวลให้กับ GolfShooter
        GolfShooter shooter = currentBall.GetComponent<GolfShooter>();
        shooter.ballMass = data.mass;

         if (currentBallText != null)
        {
        string ballName = "";

        if (index == 0)
            ballName = "Earth";
        else if (index == 1)
            ballName = "Jupiter";
        else if (index == 2)
            ballName = "Pluto";

        currentBallText.text = "Ball: " + ballName;
        }
    }
}
