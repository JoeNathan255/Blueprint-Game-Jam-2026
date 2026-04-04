using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    public void OnMainMenuButtonClicked()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
