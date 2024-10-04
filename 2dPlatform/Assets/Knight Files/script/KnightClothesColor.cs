using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightClothesColor : MonoBehaviour
{
    Animator animator;
    public GameObject[] clothesColorAnims; // ��ͬ�汾��ClothesColor����
    public float colorChangeIntensity = 0.2f; // ��ɫ�仯ǿ��
    public float positionChangeIntensity = 0.1f; // λ�ñ仯ǿ��
    public float rotationChangeIntensity = 10f; // ��ת�仯ǿ��

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetClothesColorOn()
    {


        // ���ѡ��һ��ClothesColor����
        int randomIndex = Random.Range(0, clothesColorAnims.Length);
        GameObject selectedAnim = clothesColorAnims[randomIndex];
        // ��������ʵ��
        GameObject effectInstance = Instantiate(selectedAnim, transform.position, Quaternion.identity);

        // ��ȡ����ʵ����Animator���
        Animator clothesColorAnimator = effectInstance.GetComponent<Animator>();

        // Ӧ���������ɫ�仯
        Color effectColor = new Color(
            Random.Range(1 - colorChangeIntensity, 1 + colorChangeIntensity),
            Random.Range(1 - colorChangeIntensity, 1 + colorChangeIntensity),
            Random.Range(1 - colorChangeIntensity, 1 + colorChangeIntensity)
        );
        effectInstance.GetComponent<SpriteRenderer>().color = effectColor;

        // Ӧ�������λ�ñ仯
        Vector3 effectPosition = transform.position + new Vector3(
            Random.Range(-positionChangeIntensity, positionChangeIntensity),
            Random.Range(-positionChangeIntensity, positionChangeIntensity),
            0
        );
        effectInstance.transform.position = effectPosition;

        // Ӧ���������ת�仯
        Quaternion effectRotation = Quaternion.Euler(0, 0, Random.Range(-rotationChangeIntensity, rotationChangeIntensity));
        effectInstance.transform.rotation = effectRotation;
    }
}
