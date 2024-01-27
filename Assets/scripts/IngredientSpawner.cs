using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -3.8f;
    public float maxY = 2.7f;

    public GameObject[] IngredientPrefab;
    [SerializeField] Animator[] cannonAnimators_;
    [SerializeField] GameObject[] cannonSpawnPos_;
    [SerializeField] Vector2[] cannonDir_;

    private float randomX;
    private float randomY;

    void Start()
    {
        IngredientTypes(GenerateShuffledArray(0, 21));
    }
    void Update()
    {
        //int[] shuffledArray = GenerateShuffledArray(0, 21);
        //Debug.Log("Shuffled Array: " + string.Join(", ", shuffledArray));

    }
    int[] GenerateShuffledArray(int min, int max)
    {
        int[] array = GenerateSequentialArray(min, max);
        ShuffleArray(array);
        return array;
    }

    int[] GenerateSequentialArray(int min, int max)
    {
        int[] array = new int[max - min + 1];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = min + i;
        }
        return array;
    }

    void ShuffleArray(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
            
        }

    } 
    public void RandomPosition()
    {
        randomX = Random.Range(minX, maxX);
        randomY = Random.Range(minY, maxY);
        //this.gameObject.transform.position = new(randomX, randomY);
    }
    public void IngredientTypes(int[] shuffledArray)
    {
        foreach (int index in shuffledArray)
        {
            if (index >= 0 && index < IngredientPrefab.Length)
            {
                GameObject IngredientPrefabs = IngredientPrefab [index];
                RandomPosition();
                Instantiate(IngredientPrefabs, transform.position = new Vector2(randomX, randomY), Quaternion.identity);
            }
            else
            {
                Debug.LogError("Invalid material index: " + index);
            }
        }
    }

    void spawnIngredient(IngredientType ingredientType)
    {
        var randomNum = Random.Range(0, 8);
        var spawnPos = cannonSpawnPos_[randomNum].transform.position;
        var prefab = IngredientPrefab[(int)ingredientType];
        var ingredientObject = Instantiate(prefab, spawnPos, Quaternion.identity);
        ingredientObject.gameObject.GetComponent<Rigidbody2D>().AddForce(cannonDir_[randomNum]);
        cannonAnimators_[randomNum].CrossFadeInFixedTime("Boom", 0.05f);
    }
}
