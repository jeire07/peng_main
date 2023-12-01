using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    //public static LevelManager Level;
    //public static ResourceManager Resource;

    //private ResourceManager _resource;
    //private LevelManager _level;

    public ResourceManager Resource;// { get { return _resource; } }

    public LevelManager LvInstance;// { get { return _level; } set { Instance._level = value; } }

    public GameObject Player;
    public GameObject Monster;
    public GameObject Bullet;
    public GameObject Door;

    // field for EndPanel
    public GameObject EndPanel;

    public float ElapsedTime; //{ get; private set; }
    public float BestAliveTime { get; private set; }
    public float AliveTime { get; private set; }
    public int BestKill { get; private set; }
    public int Kill { get; private set; }

    public bool _isRunning = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(Instance);

        GameObject _level = Resources.Load<GameObject>("Prefabs/LevelManager");
        GameObject _resource = Resources.Load<GameObject>("Prefabs/ResourceManager");
        Instantiate(_level);
        Instantiate(_resource);
    }

    // Start is called before the first frame update
    void Start()
    {
        ElapsedTime = 0.0f;
        AliveTime = 0;
        Kill = 0;
        _isRunning = true;

        Time.timeScale = 1.0f;
        InvokeRepeating("MakeMonster", 0.0f, 2.0f);
        //InvokeRepeating("MakeBullet", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            ElapsedTime += Time.deltaTime;
        }

        if ((ElapsedTime > 10.0f) && _isRunning)
        {
            GameOver();
        }
    }

    //public void MakeBullet()
    //{
    //    float x = Player.transform.position.x;
    //    float y = Player.transform.position.y + 2.0f;
    //    Instantiate(Bullet, new Vector3(x, y, 0), Quaternion.identity);
    //}

    private void MakeMonster()
    {
        float x = Door.transform.position.x;
        float y = Door.transform.position.y;
        Instantiate(Monster, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void GameOver()
    {
        GameObject endPanel = Resources.Load<GameObject>("Prefabs/EndPanel");
        Instantiate(endPanel);
        Time.timeScale = 0.0f;
        _isRunning = false;
    }
}
