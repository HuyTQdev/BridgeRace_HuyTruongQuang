using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private Image fill;
    [SerializeField] TextMeshProUGUI text;

    private void Start()
    {
        StartCoroutine(LoadLevelAsync(scene));
    }

    private IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            fill.fillAmount = loadOperation.progress;
            text.text = (int)(loadOperation.progress * 100) + "%";
            yield return null;
        }
        yield return new WaitForSeconds(.2f);

    }
}
