using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    public float speed = 5;
    public int countPoints = 10;
    public Canvas StartCanvas;
    public Text ScoreText;
    public Text WinText;

    private Rigidbody rb;
    private List<GameObject> Points;
    private int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Assets.Scripts.PoolPoints.PoolInitialization();
        WinText.enabled = false;
    }

    void FixedUpdate()
    {
        if (!StartCanvas.enabled)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

            rb.AddForce(movement * speed);
        }
    }

    public void ClickStartButton()
    {
        StartCanvas.enabled = false;
        List<Vector3> pointsCoords = Assets.Scripts.GeneratorPoints.GetPointsCoords(countPoints, transform.position);
        Points = new List<GameObject>();
        for (int i = 0; i < countPoints; i++)
        {
            GameObject newPoint = Assets.Scripts.PoolPoints.Pop();
            newPoint.transform.position = pointsCoords[i];
            Points.Add(newPoint);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            Points.Remove(other.gameObject);
            Assets.Scripts.PoolPoints.Push(other.gameObject);
            score++;
            ScoreText.text = "Score: " + score;
        }
        if (score == countPoints)
            WinText.enabled = true;
    }
}
