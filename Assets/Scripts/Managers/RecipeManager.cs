﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class RecipeManager : Singleton<RecipeManager>
{
    public GameObject defaultResult;

    private Dictionary<Mixture, GameObject> recipes;

    void Start()
    {
		recipes = new Dictionary<Mixture, GameObject>();
		string[] allRecipes = AssetDatabase.FindAssets("t:Mixture", new [] {"Assets/ScriptableAssets/Recipes"});
		foreach(string s in allRecipes)
		{
			Mixture m = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(Mixture)) as Mixture;
			m = Object.Instantiate(m) as Mixture;
			m.OrderSelf();
			GameObject temp = m.result;
			m.result = null;
			//Debug.Log(m.GetHashCode());
			recipes[m] = temp;
			//recipes.Add(m, temp);
		}
        //load our asset database into the dictionary
    }

	//trys to get a value by sorting our mixtures and hashing them into the dictionary
	//if we fail we return a default
    public GameObject GetResult(Mixture m)
    {
		m.OrderSelf();
		//Debug.Log(m);
		//Debug.Log(m.GetHashCode());
        GameObject ret;
        if(recipes.TryGetValue(m, out ret))
        {
            return ret;
        }
        else
        {
            return defaultResult;
        }
    }
}
