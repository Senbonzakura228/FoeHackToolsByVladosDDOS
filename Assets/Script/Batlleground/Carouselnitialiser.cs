using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carouselnitialiser : MonoBehaviour
{
    public CarouselProvinceItem itemPrefab;

    public void Initialise(Battleground battleground)
    {
        foreach (var province in battleground.map.provinces)
        {
            CarouselProvinceItem PrepearedProvince = Instantiate(itemPrefab, transform);
            if (province?.id != null)
            {
                PrepearedProvince.SetNameById((int) province.id, battleground.map.id);
            }
            else
            {
                PrepearedProvince.SetNameById(0, battleground.map.id);
            }

            PrepearedProvince.SetProvince(province);
        }
    }
}