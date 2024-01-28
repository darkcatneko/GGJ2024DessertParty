using UnityEngine.UI;
using System;
using UnityEngine;

public class OrderUIUpdater : MonoBehaviour
{
    [SerializeField] Sprite[] dessertImages_;
    [SerializeField] Sprite[] forPlayerImages_;
    [SerializeField] Sprite[] ingredientImages_;
    [SerializeField] OrderUI[] allOrderUI;
    void Start()
    {
        GameManager.Instance.MainGameEvent.SetSubscribe(GameManager.Instance.MainGameEvent.OnGameQuestUIUpdate, cmd => { updateUI(cmd.Quest); });
    }

    void updateUI(Quest quest)
    {
        var target = allOrderUI[(int)quest.FromPlayer];
        target.OrderUIImage.sprite = dessertImages_[(int)quest.DessertType];
        target.ForPlayerImage.sprite = forPlayerImages_[(int)quest.ToPlayer];
        target.Ingredient1Image.sprite = ingredientImages_[quest.Answer[0]];
        target.Ingredient1Image.sprite = ingredientImages_[quest.Answer[1]];
        target.Ingredient1Image.sprite = ingredientImages_[quest.Answer[2]];
    }
}
[Serializable]
public struct OrderUI
{
    public Image OrderUIImage;
    public Image ForPlayerImage;
    public Image Ingredient1Image;
    public Image Ingredient2Image;
    public Image Ingredient3Image;
    
}
