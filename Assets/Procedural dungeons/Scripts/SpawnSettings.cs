using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// script offers a way for the user to set up the dungeon generator in play mode
/// </summary>
public class SpawnSettings : MonoBehaviour {

    public static SpawnSettings _instance;
    public InputField levelName;
    public GameObject scrollView;
    public GameObject generateButton;
    public GameObject levelButton;
    public Text levels;
    [Header("grid Sizes")]
    public int gridWorldSizeX;
    public int gridWorldSizeY;

    [Header("node")]
    public int nodeDiameter;

    public InputField sizeX, sizeY, diameter;

    public bool useStandardSettings = false;
    public bool switchInput = false;
    public string loadingPath;
    private GameObject activeObject;
    private int activeScene = 0;

    public void Awake() {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(this);

            } else {
            Destroy(this);
            }
        }
   
    public string[] levelAssets() {
        TextAsset[] LevelFiles = Resources.LoadAll<TextAsset>("LevelData");
        string[] levelsStr = new string[LevelFiles.Length];
        for (int i = 0; i < levelsStr.Length; i++) {
            levelsStr[i] = LevelFiles[i].name;
            Debug.Log(levelsStr[i]);
            }
        return levelsStr;
        }

    public void Activate(GameObject _go) {
        if (activeObject != null) {
            activeObject.SetActive(false);
            }
        _go.SetActive(true);
        activeObject = _go;
        }
    
    public void ChangeScene(int _index) {
        activeScene = _index;
        SceneManager.LoadScene(_index);
        }

    public void LoadLevel() {
        scrollView.SetActive(true);

        string[] files = levelAssets();
        for (int i = 0; i < files.Length; i++) {
            //ItemGameObject is my prefab pointer that i previous made a public property  
            //and  assigned a prefab to it
            GameObject Button = Instantiate(levelButton) as GameObject;
            var i2 = i;
            Button.GetComponent<Button>().onClick.AddListener(delegate { LoadFromJson(files[i2]); });

            if (scrollView != null) {
                Button.transform.SetParent(scrollView.transform, false);
                Vector3 rectPos = Button.GetComponent<RectTransform>().position;
                Button.GetComponent<RectTransform>().position = new Vector3(rectPos.x, rectPos.y - (100 * i2), rectPos.z);
                Button.GetComponentInChildren<Text>().text = files[i2];
                }
            }
        }

    public void LoadFromJson(string _path) {
        Debug.Log("working : " + _path);
        loadingPath = "Assets/Resources/LevelData/" + _path + ".JSON";
        switchInput = true;
        ChangeScene(activeScene + 1);
        }
    public void StandardValues() {
        gridWorldSizeX = 350;
        gridWorldSizeY = 350;
        nodeDiameter = 10;
        ChangeScene(activeScene+1);
        }

    //parses the values from the (integer only) inputfields and then loads the next scene in the build
    public void SetValue() {
        gridWorldSizeX = int.Parse(sizeX.text);
        gridWorldSizeY = int.Parse(sizeY.text);
        nodeDiameter = int.Parse(diameter.text);
        ChangeScene(activeScene + 1);
        }
    }
