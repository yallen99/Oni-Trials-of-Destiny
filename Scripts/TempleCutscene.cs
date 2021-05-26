using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempleCutscene : MonoBehaviour
{
    [SerializeField] private GameObject torch;
    [SerializeField] private GameObject cameraCuts;
    [SerializeField] private GameObject pressE;
    [SerializeField] private GameObject seal;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(AnimateCamera());
        }
    }

    private IEnumerator AnimateCamera()
    {
        cameraCuts.SetActive(true);
        pressE.SetActive(false);
        yield return new WaitForSeconds(2);
        torch.SetActive(true);
        yield return new WaitForSeconds(1);
        seal.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Main Menu");
    }
}
