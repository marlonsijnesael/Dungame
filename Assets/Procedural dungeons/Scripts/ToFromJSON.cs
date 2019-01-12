using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFromJSON : MonoBehaviour {

    public static SaveDataClass SaveDungeonStructure(GameObject _spawner) {
        var result = new SaveDataClass();
        result.amountOfRooms = _spawner.transform.childCount;
     
        for (int i = 0; i < _spawner.transform.childCount; i++) {
            GameObject child = _spawner.transform.GetChild(i).gameObject;
            }

        return result;
        }

    public void ConvertToJSON(SaveDataClass _saveData) {
        JsonUtility.ToJson(_saveData);
        }
    }
