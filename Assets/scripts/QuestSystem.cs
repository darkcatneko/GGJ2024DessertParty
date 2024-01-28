using System;
using UnityEngine;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem instance;
    public Quest[] PlayerQuests = new Quest[4];
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        genStartQuest();
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnPlayerFinishQuest, cmd => { PlayerQuests[(int)cmd.FinishPlayerIdentity] = genNewQuest((int)cmd.FinishPlayerIdentity); });
    }
    void genStartQuest()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerQuests[i] = genNewQuest(i);
        }
    }

    Quest genNewQuest(int FormPlayer)
    {
        var randomToPlayer = UnityEngine.Random.Range(0, 4);
        while (randomToPlayer== FormPlayer) { randomToPlayer = UnityEngine.Random.Range(0, 4); }
        var randomDessert = UnityEngine.Random.Range(0, 10);
        var result = new Quest();
        result.FromPlayer = (PlayerIdentity)FormPlayer;
        result.ToPlayer = (PlayerIdentity)randomToPlayer;
        result.DessertType = (DessertType)randomDessert;
        result.Answer = genAnswer(randomDessert);
        return result;
    }
    int[] genAnswer(int randomDessert)
    {
        var result = new int[3];
        switch ((DessertType)randomDessert)
        {
            case DessertType.RedBeanCake:
                result = GenRedBeanCake();
                break;
            case DessertType.HoneycombToffee:
                result = GenHoneycombToffee();
                break;
            case DessertType.Pudding:
                result = GenPudding();
                break;
            case DessertType.PineappleCake:
                result = GenPineappleCake();
                break;
            case DessertType.BehTeungGuai:
                result = GenBehTeungGuai();
                break;
            case DessertType.Popsicle:
                result = GenPopsicle();
                break;
            case DessertType.EggCake:
                result = GenEggCake();
                break;
            case DessertType.KueLapis:
                result = GenKueLapis();
                break;
            case DessertType.ShavedIce:
                result = GenShavedIce();
                break;
            case DessertType.TuTuKue:
                result = GenTuTuKue();
                break;
        }
        return result;
    }
    int[] GenRedBeanCake()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = 7;
        result[2] = randomIngredientForType(IngredientMainType.Beans);
        Array.Sort(result);
        return result;
    }

    int[] GenHoneycombToffee()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = 6;
        result[2] = randomIngredientForType(IngredientMainType.Beans);
        Array.Sort(result);
        return result;
    }
    int[] GenPudding()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = randomIngredientForType(IngredientMainType.Egg);
        result[2] = 13;
        Array.Sort(result);
        return result;
    }

    int[] GenPineappleCake()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = randomIngredientForType(IngredientMainType.Beans);
        result[2] = 13;
        Array.Sort(result);
        return result;
    }
    int[] GenBehTeungGuai()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = randomIngredientForType(IngredientMainType.Powder);
        result[2] = randomIngredientForType(IngredientMainType.Beans);
        Array.Sort(result);
        return result;
    }

    int[] GenPopsicle()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = randomIngredientForType(IngredientMainType.Water);
        result[2] = randomIngredientForType(IngredientMainType.Beans);
        Array.Sort(result);
        return result;
    }
    int[] GenEggCake()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = 13;
        result[2] = randomIngredientForType(IngredientMainType.Egg);
        Array.Sort(result);
        return result;
    }

    int[] GenKueLapis()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Sugar);
        result[1] = 12;
        result[2] = randomIngredientForType(IngredientMainType.Powder);
        Array.Sort(result);
        return result;
    }

    int[] GenShavedIce()
    {
        var result = new int[3];
        result[0] = randomIngredientForType(IngredientMainType.Water);
        result[1] = randomIngredientForType(IngredientMainType.Sugar);
        result[2] = randomIngredientForType(IngredientMainType.Beans);
        Array.Sort(result);
        return result;
    }
    int[] GenTuTuKue()
    {
        var result = new int[3];
        result[0] = 9;
        result[1] = 5;
        result[2] = randomIngredientForType(IngredientMainType.Beans);
        Array.Sort(result);
        return result;
    }
    int randomIngredientForType(IngredientMainType type)
    {
        var result = 0;
        switch (type)
        {
            case IngredientMainType.Sugar:
                result = UnityEngine.Random.Range(0, 5);
                break;
            case IngredientMainType.Powder:
                result = UnityEngine.Random.Range(5, 11);
                break;
            case IngredientMainType.Water:
                result = UnityEngine.Random.Range(11, 14);
                break;
            case IngredientMainType.Beans:
                result = UnityEngine.Random.Range(14, 17);
                break;
            case IngredientMainType.Egg:
                result = UnityEngine.Random.Range(17, 22);
                break;
        }
        return result;

    }


}

[Serializable]
public struct Quest
{
    public PlayerIdentity FromPlayer;
    public PlayerIdentity ToPlayer;
    public DessertType DessertType;
    public int[] Answer;
}

public enum DessertType
{
    RedBeanCake,
    HoneycombToffee,
    Pudding,
    PineappleCake,
    BehTeungGuai,
    Popsicle,
    EggCake,
    KueLapis,
    ShavedIce,
    TuTuKue,
}

public enum IngredientMainType
{
    Sugar,
    Powder,
    Water,
    Beans,
    Egg,
}

