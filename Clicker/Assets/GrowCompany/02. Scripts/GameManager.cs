using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    //����
    int currentStage = 1; //���� ��������
    int credit = 0;       //���� ���
    public Text stageTxt, creditTxt;

    //���׷��̵�
    //0: �����Ƽ, 1: ���÷���, 2: ����
    int[] income = { 10, 50, 200 };
    int[] cost = { 100, 500, 20000 };
    int[] level = { 1, 1, 1 };
    public Text[] btnTxt, upgradeTxt;

    public GameObject[] cheatBtn;   //ġƮ ��ư
    GameObject thisBtn;             //����� ��ư
    int index;                      //��ư�� ���� �ε��� ��

    public SpriteRenderer background; //���� ���
    public Sprite[] stageImage;       //���� ��� �̹��� �ҽ�


    void Start()
    {
        stageTxt.text = "Stage: " + currentStage;
        background.sprite = stageImage[currentStage - 1];

        StartCoroutine(ChangeCredit());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            for (int i = 0; i < cheatBtn.Length; i++)
                cheatBtn[i].SetActive(false);
    }

    IEnumerator ChangeCredit()
    {
        while (true)
        {
            credit += income[0] + income[1] + income[2];
            creditTxt.text = "Credit: " + credit;
            yield return new WaitForSeconds(0.2f);
        }
    }

    void CheckThisBtn()
    {
        thisBtn = EventSystem.current.currentSelectedGameObject;
        if (thisBtn.name == "AbilityBtn" || thisBtn.name == "ACheatBtn") index = 0;
        else if (thisBtn.name == "EmployeeBtn" || thisBtn.name == "ECheatBtn") index = 1;
        else index = 2;
    }

    public void UpgradeBtn()
    {
        CheckThisBtn();

        for (int i = 0; i < cheatBtn.Length; i++)
            cheatBtn[i].SetActive(false);

        cheatBtn[index].SetActive(true);

        if (currentStage <= 1) cheatBtn[1].GetComponent<Button>().interactable = false;
        else cheatBtn[1].GetComponent<Button>().interactable = true;
    }

    public void CheatBtn()
    {
        CheckThisBtn();

        if (credit >= cost[index])
        {
            credit -= cost[index];
            income[index] *= 2;
            cost[index] *= 3;
            level[index]++;

            btnTxt[index].text = thisBtn.GetComponentInParent<Text>().text + "\n(" + level[index] + ")";
            upgradeTxt[index].text = "income : +" + income[index] + " per buy\nCost : +" + cost[index] + " per buy";

            if (index == 2)
            {
                if (level[index] % 3 == 0)
                {
                    currentStage++;
                    stageTxt.text = "Stage: " + currentStage;
                    background.sprite = stageImage[currentStage - 1];
                }
            }
        }

        cheatBtn[index].SetActive(false);
    }
}