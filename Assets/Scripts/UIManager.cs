
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // handle to text
    [SerializeField]
    private TMP_Text _scoreText;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
    }
    public void UpdateScore(int _currentScore)
    {
        _scoreText.text = "Score: " + _currentScore;
    }
}
