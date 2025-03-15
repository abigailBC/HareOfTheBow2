using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

}
