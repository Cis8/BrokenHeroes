using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.U2D.Animation;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class HeroesInventory : MonoBehaviour
{
    [SerializeField]
    private HeroesList _ownedHeroes;

    [SerializeField]
    private HeroesList _allHeroes;

    private SpriteLibrary portraits;

    public HeroesList OwnedHeroes { get => _ownedHeroes; set => _ownedHeroes = value; }
    public HeroesList AllHeroes { get => _allHeroes; set => _allHeroes = value; }

    public AsyncOperationHandle<IList<Sprite>> opHandle;

    private IEnumerator Start()
    {
        portraits = gameObject.AddComponent<SpriteLibrary>();
        portraits.spriteLibraryAsset = ScriptableObject.CreateInstance<SpriteLibraryAsset>();
        /*opHandle = Addressables.LoadAssetsAsync<Sprite>(
            "Portrait",
            s =>
            {
                portraits.spriteLibraryAsset.AddCategoryLabel(s, "Portraits", s.name);
            },
            Addressables.MergeMode.Union,
            false);*/

        opHandle = Addressables.LoadAssetsAsync<Sprite>("Portrait", null);
        ////Disabled.Log("1) Waiting for completion...");
        yield return opHandle;
        ////Disabled.Log("2) Sprites fully loaded.");


        foreach (Sprite s in opHandle.Result)
            {
                //////Disabled.Log("Sprite: " + s.name);
                portraits.spriteLibraryAsset.AddCategoryLabel(s, "Portraits", s.name);
            }
        gameObject.transform.parent.Find("InventoryPanel").GetComponent<InventoryPanel>().LoadPortraits();
        

        

        /*if(opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (Sprite s in opHandle.Result)
            {
                portraits.spriteLibraryAsset.AddCategoryLabel(s, "Portraits", s.name);
            }
        }
        else
        {
            throw new System.Exception("Failed in loading portraits' sprites.");
        }*/
        

    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    public Sprite GetPortrait(string name)
    {
        return portraits.spriteLibraryAsset.GetSprite("Portraits", name);
    }
}
