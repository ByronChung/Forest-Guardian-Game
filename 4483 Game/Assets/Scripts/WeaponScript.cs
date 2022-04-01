using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    int totalWeapons = 1;
    public int currentWeaponIndex;
    public int[] bulletCount;
    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;

    public AudioClip[] gunSFX;
    // Start is called before the first frame update
    async void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for(int i = 0; i < totalWeapons; i++){
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }

        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Swap back to the pistol if they run out of ammo on the rifle
        if (currentWeaponIndex == 1 && bulletCount[0] == 0){
            swapToPistol();
        }
        // Swap back to the pistol if they run out of ammo on the shotgun
        if (currentWeaponIndex == 2 && bulletCount[1] == 0){
            swapToPistol();
        }

        // Swap to the pistol if the player pressed '1'
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            if (currentWeaponIndex != 0){
                swapToPistol();
            }
        }
        // Swap to the rifle if the player pressed '2'
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            if (currentWeaponIndex != 1 && bulletCount[0] > 0){
                swapToRifle();
            }
        }
        // Swap to the shotgun if the player pressed '3'
        else if(Input.GetKeyDown(KeyCode.Alpha3)){
            if (currentWeaponIndex != 2 && bulletCount[1] > 0){
                swapToShotgun();
            }
        }
    }

    private void swapToPistol(){
        guns[currentWeaponIndex].SetActive(false);
        guns[0].SetActive(true);
        currentWeaponIndex = 0;
    }
    private void swapToRifle(){
        guns[currentWeaponIndex].SetActive(false);
        guns[1].SetActive(true);
        currentWeaponIndex = 1;
    }

    private void swapToShotgun(){
        guns[currentWeaponIndex].SetActive(false);
        guns[2].SetActive(true);
        currentWeaponIndex = 2;
    }
}
