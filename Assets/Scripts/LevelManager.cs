using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class World
    {
        public string name;
        public List<string> levelIndex;
        //public List<Scene> levelIndex;

        //public World next;

        public World(string _name)
        {
            levelIndex = new List<string>();
            //levelIndex = new List<Scene>();
            name = _name;
        }
    }

    public int currentWorld = 0;
    //New variables
    //private World currentWorld;
    //private World headWorld;

    public int currentLevel = 0;
    public bool isChange = false;

    //public World worldIndex;
    public List<World> worldIndex;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //New startup code
        //headWorld = worldIndex;
        //currentWorld = worldIndex;
    }

    public void Update()
    {
        if (isChange)
        {
            isChange = false;
            Load();
        }
    }

    public void Load()
    {
        SceneManager.LoadScene(worldIndex[currentWorld].levelIndex[currentLevel]);
        //SceneManager.LoadScene(worldIndex.levelIndex[currentLevel].name);
    }

    public void Reset()
    {
        Load();
    }
    public void Next()
    {
        currentLevel++;

        if (currentLevel > worldIndex[currentWorld].levelIndex.Count - 1)
        {
            currentLevel = 0;
            currentWorld++;

            //if(currentWorld.next != null)
            //{
            //    currentWorld = currentWorld.next;
            //}
            //else
            //{
            //    currentWorld = headWorld;
            //}
        }
        if (currentWorld > worldIndex.Count - 1)
        {
            currentWorld = 0;
        }

        Load();
    }

}
