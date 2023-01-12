using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_ : MonoBehaviour
{
    //인포
    int currentStage = 1; //현재 스테이지
    int credit = 0; //현재 재산
    public Text stageTxt, creditTxt;

    //업그레이드

    int abilityIncome, employeeIncome, buildingIncome; //현재 수익
    int abilityCost, employeeCost, buildingCost; //업그레이드 시 지불 비용
    public Text abilityTxt, employeeTxt, buildingTxt;

    public SpriteRenderer background;
    public Sprite[] stageImage;
    int buildingCount;

    public GameObject[] cheatBtn;

    void Start()
    {
        stageTxt.text = "Stage: " + currentStage;
        background.sprite = stageImage[currentStage - 1];

        abilityIncome = 10;
        employeeIncome = 50;
        buildingIncome = 200;

        abilityCost = 100;
        employeeCost = 500;
        buildingCost = 20000;

        abilityTxt.text = "income : +" + abilityIncome + " per buy\nCost : +" + abilityCost + " per buy";
        employeeTxt.text = "income : +" + employeeIncome + " per buy\nCost : +" + employeeCost + " per buy";
        buildingTxt.text = "income : +" + buildingIncome + " per buy\nCost : +" + buildingCost + " per buy";

        StartCoroutine(ChangeCredit());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            for (int i = 0; i<= cheatBtn.Length; i++)
                cheatBtn[i].SetActive(false);
    }

    IEnumerator ChangeCredit()
    {
        while (true)
        {
            credit += abilityIncome + employeeIncome + buildingIncome;
            creditTxt.text = "Credit: " + credit;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void AbilityBtn() //업그레이드 버튼
    {
        cheatBtn[0].SetActive(true);
        cheatBtn[1].SetActive(false);
        cheatBtn[2].SetActive(false);
    }

    public void EmployeeBtn()
    {
        cheatBtn[0].SetActive(false);
        cheatBtn[1].SetActive(true);
        cheatBtn[2].SetActive(false);

        if (currentStage <= 1) cheatBtn[1].GetComponent<Button>().interactable = false;
        else cheatBtn[1].GetComponent<Button>().interactable = true;
    }

    public void BuildingBtn()
    {
        cheatBtn[0].SetActive(false);
        cheatBtn[1].SetActive(false);
        cheatBtn[2].SetActive(true);
    }


    public void AbilityCheat()
    {
        if (credit >= abilityCost)
        {
            credit -= abilityCost;
            abilityIncome *= 2;
            abilityCost *= 3;
            abilityTxt.text = "income : +" + abilityIncome + " per buy\nCost : +" + abilityCost + " per buy";
            cheatBtn[0].SetActive(false);
        }
        else return;
    }

    public void EmployeeCheat()
    {
        if (credit >= employeeCost)
        {
            credit -= employeeCost;
            employeeIncome *= 2;
            employeeCost *= 3;
            employeeTxt.text = "income : +" + employeeIncome + " per buy\nCost : +" + employeeCost + " per buy";
            cheatBtn[1].SetActive(false);
        }
        else return;
    }

    public void BuildingCheat()
    {
        if (credit >= buildingCost)
        {
            credit -= buildingCost;
            buildingIncome *= 2;
            buildingCost *= 3;
            buildingCount++;
            buildingTxt.text = "income : +" + buildingIncome + " per buy\nCost : +" + buildingCost + " per buy";
            cheatBtn[2].SetActive(false);
        }
        else return;

        if (buildingCount % 3 == 0)
        {
            currentStage++;
            stageTxt.text = "Stage: " + currentStage;
            background.sprite = stageImage[currentStage - 1];
        }
    }
}