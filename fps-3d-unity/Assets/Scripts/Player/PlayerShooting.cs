using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Input")]
    public static Action shootInput;
    public static Action reloadInput;

    public KeyCode reloadKey = KeyCode.R;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            reloadInput?.Invoke();
        }
    }
}
