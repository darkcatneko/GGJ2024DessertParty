
using Gamemanager;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameReadyManagerView : MonoBehaviour
{
    [SerializeField] string sceneName_;
    bool canInput = false;
    private async void Start()
    {
        await Task.Delay(500);
        canInput = true;
    }
    private void Update()
    {
        if (!canInput) return;
        foreach (var gamepad in Gamepad.all)
        {
            foreach (var control in gamepad.allControls)
            {
                if (control.IsPressed())
                {
                    Debug.Log("pressed");
                    
                    SceneManager.LoadScene(sceneName_);
                }
            }
        }
    }
}
