using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SlowMoAndReloadScene : MonoBehaviour
{
    public float slowMoDuration = 5f; // ����������ʱ��

    private void Start()
    {
        // ��ʼ������
        Time.timeScale = 1f; // ȷ����ʼʱTime.timeScaleΪ1
        StartCoroutine(SlowMoAndReload());
    }

    private void Update()
    {
        Debug.Log("Time.timeScale: " + Time.timeScale);
    }

    IEnumerator SlowMoAndReload()
    {
        float timer = 0f;
        while (timer < slowMoDuration)
        {
            Time.timeScale = Mathf.Lerp(1f, 0f, timer / slowMoDuration); // ƽ������Time.timeScale
            timer += Time.unscaledDeltaTime; // ʹ��unscaledDeltaTime��ȷ������Time.timeScaleӰ��
            yield return null;
        }

        // ȷ��Time.timeScaleΪ0
        Time.timeScale = 0f;

        // �ȴ�һ֡ʱ�䣬ȷ�����л���ʱ��Ĳ�������ֹͣ
        yield return new WaitForEndOfFrame();

        // ������ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}