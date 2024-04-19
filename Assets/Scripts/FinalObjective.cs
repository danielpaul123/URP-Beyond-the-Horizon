using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalObjective : MonoBehaviour
{
    [Header("Vehicle button")]
    [SerializeField] private KeyCode vehicleButton = KeyCode.F;

    [Header("Generator Sound Effects and radius")]
    private float radius = 3f;
    public playerscript player;

    private void Update()
    {
        if(Input.GetKeyDown(vehicleButton) && Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
            Cursor.lockState = CursorLockMode.None;

            ObjectivesComplete.occurrence.GetObjectivesDone(true, true, true, true);
        }
    }
}