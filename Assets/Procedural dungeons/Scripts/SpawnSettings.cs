using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// script offers a way for the user to set up the dungeon generator in play mode
/// </summary>
public class SpawnSettings : MonoBehaviour {

    public static SpawnSettings _instance;

    [Header("grid Sizes")]
    public int gridWorldSizeX;
    public int gridWorldSizeY;

    [Header("node")]
    public int nodeDiameter;

    public InputField sizeX, sizeY, diameter;

    public void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this);

            } else {
            Destroy(this);
            }
        }


    //parses the values from the (integer only) inputfields and then loads the next scene in the build
    public void SetValue() {
        gridWorldSizeX = int.Parse(sizeX.text);
        gridWorldSizeY = int.Parse(sizeY.text);
        nodeDiameter = int.Parse(diameter.text);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
