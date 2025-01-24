using UnityEngine;
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    public Button startButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startButton.onClick.Invoke();
        }
    }
}