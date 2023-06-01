using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// TODO: add new
public enum WordType
{
    StartWord,
    EndWord,
    IsWord,
    NotWord
}
/// TODO: add new
public enum BlockType
{
    Rock,
    Player,
    IsWord,
    Push,
    Stop,
    Flag,
    Win,
    You,
    Wall,
    Skull,
    Defeat,
    Sink,
    Water, 
    Lava, 
    Melt, 
    Hot
}


public class gameController : MonoBehaviour
{
    public GameObject winScreen;
    static readonly int WIDTH = 50;
    List<GameBlock> blocks = new List<GameBlock>();

    List<GameBlock> grid = new List<GameBlock>(new GameBlock[WIDTH * WIDTH]);
    

    GameBlock playerBlock;
    Scene scene;

    HashSet<List<GameBlock>> rules = new HashSet<List<GameBlock>>();
    GameBlock[] ignore = new GameBlock[WIDTH * WIDTH];

    public AudioSource sfx_src;
    public AudioClip[] game_sfx = new AudioClip[5];
    void Start()
    {
        sfx_src.loop = false;
        List<GameObject> gameObjects = new List<GameObject>();
        scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(gameObjects);

        foreach (var obj in gameObjects)
        {


            /// TODO: add new
            if (obj.tag.StartsWith("NotWord")
                || obj.tag.StartsWith("StartWord")
                || obj.tag.StartsWith("EndWord")
                || obj.tag.StartsWith("IsWord"))
            {
                WordType wordType = getWordType(obj);
                BlockType blockType = getBlockType(obj);

                obj.transform.position = new Vector3((float)Math.Round(obj.transform.position.x), obj.transform.position.y, (float)Math.Round(obj.transform.position.z));

                var block = new GameBlock(obj, wordType, blockType);

                blocks.Add(block);

                int x = (int)block.gameObject.transform.position.x;
                int z = (int)block.gameObject.transform.position.z;

                grid[(z * WIDTH) + x] = block;

            } 
        }
        check_iss();
        applyRules();

    }

    /// TODO: add new
    BlockType getBlockType(GameObject obj)
    {
        if (obj.tag.EndsWith("rock"))
        {
            return BlockType.Rock;
        }
        else if (obj.tag.EndsWith("player"))
        {
            return BlockType.Player;
        }
        else if (obj.tag.EndsWith("is"))
        {
            return BlockType.IsWord;
        }
        else if (obj.tag.EndsWith("push"))
        {
            return BlockType.Push;
        }
        else if (obj.tag.EndsWith("flag"))
        {
            return BlockType.Flag;
        }
        else if (obj.tag.EndsWith("win"))
        {
            return BlockType.Win;
        }
        else if (obj.tag.EndsWith("wall"))
        {
            return BlockType.Wall;
        }
        else if (obj.tag.EndsWith("you"))
        {
            return BlockType.You;
        }
        else if (obj.tag.EndsWith("defeat"))
        {
            return BlockType.Defeat;
        }
        else if (obj.tag.EndsWith("sink"))
        {
            return BlockType.Sink;
        }
        else if (obj.tag.EndsWith("water"))
        {
            return BlockType.Water;
        }
        else if (obj.tag.EndsWith("melt"))
        {
            return BlockType.Melt;
        }
        else if (obj.tag.EndsWith("hot"))
        {
            return BlockType.Hot;
        }
        else if (obj.tag.EndsWith("skull"))
        {
            return BlockType.Skull;
        }
        else
        {
            return BlockType.Stop;
        }
    }

    /// TODO: add new
    WordType getWordType(GameObject obj)
    {
        if (obj.tag.StartsWith("NotWord"))
        {
            return WordType.NotWord;
        }
        else if (obj.tag.StartsWith("StartWord"))
        {
            return WordType.StartWord;
        }
        else if (obj.tag.StartsWith("EndWord"))
        {
            return WordType.EndWord;
        }
        else
        {
            return WordType.IsWord;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMap();
        }
        move_player();
        check_iss();
        applyRules();
        checkWin();
    }

    public void ReturnToMap()
    {
        SceneManager.LoadScene(0);
    }
    void move_player()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            move(playerBlock, 'r');
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            move(playerBlock, 'u');
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            move(playerBlock, 'l');
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            move(playerBlock, 'd');
        }else if (Input.GetKeyDown(KeyCode.R))
        {
            sfx_src.clip = game_sfx[3];
            sfx_src.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void check_iss()
    {
        rules.Clear();

        foreach (GameBlock block in blocks)
        {
            if (block.blockType == BlockType.IsWord)
            {
                int x = (int)block.gameObject.transform.position.x;
                int z = (int)block.gameObject.transform.position.z;

                var blockLeft = grid[z * WIDTH + x - 1];
                var blockRight = grid[z * WIDTH + x + 1];
                var blockUp = grid[(z + 1) * WIDTH + x];
                var blockDown = grid[(z - 1) * WIDTH + x];

                if (blockLeft is not null && blockRight is not null 
                    && blockLeft.wordType == WordType.StartWord
                    && blockRight.wordType == WordType.EndWord)
                {

                    rules.Add(new List<GameBlock> { blockLeft, blockRight });
                   
                }
                if (blockUp is not null 
                    && blockDown is not null 
                    && blockUp.wordType == WordType.StartWord 
                    && blockDown.wordType == WordType.EndWord)
                {

                    rules.Add(new List<GameBlock> { blockUp, blockDown });

                }

            }
        }
    }

    void applyRules()
    {
        playerBlock = null;
        foreach(GameBlock b in blocks)
        {
            b.isWin = false;
            b.isStop = false;
            b.isPush = false;
            b.isSink = false;
            b.isDefeat = false;
            b.isYou = false;
            b.isMelt = false;
            b.isHot = false;


            if (b.wordType != WordType.NotWord)
            {
                b.isPush = true;
            }
            if(b.blockType == BlockType.Player)
            {
                b.isYou = true;
            }

        }

        foreach(var rule in rules)
        {
            foreach (GameBlock b in blocks)
            {
                if(b.wordType == WordType.NotWord)
                {

                    if (b.blockType == rule[0].blockType)
                    {
                        if (rule[1].blockType == BlockType.Push)
                        {
                            b.isPush = true;
                        }
                        else if(rule[1].blockType == BlockType.Stop)
                        {
                            b.isStop = true;

                        }
                        else if (rule[1].blockType == BlockType.Win)
                        {
                            b.isWin = true;

                        }
                        else if (rule[1].blockType == BlockType.Defeat)
                        {
                            b.isDefeat = true;

                        }
                        else if (rule[1].blockType == BlockType.Sink)
                        {
                            b.isSink = true;

                        }
                        else if (rule[1].blockType == BlockType.Melt)
                        {
                            b.isMelt = true;

                        }
                        else if (rule[1].blockType == BlockType.Hot)
                        {
                            b.isHot = true;

                        }
                        else if (rule[1].blockType == BlockType.You)
                        {
                            b.isYou = true;
                            playerBlock = b;

                        }
                    }
                }
            }
        }
    }

    bool checkWin()
    {

        int x = (int)playerBlock.gameObject.transform.position.x;
        int z = (int)playerBlock.gameObject.transform.position.z;
        var currentBlock = grid[z * WIDTH + x];


        if (currentBlock is not null && currentBlock.isWin)
        {
            winScreen.SetActive(true);
            int currentLevel = SceneManager.GetActiveScene().buildIndex;
            if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
            {
                PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            }
            Debug.Log(PlayerPrefs.GetInt("levelsUnlocked") + "Unlocked");
            StartCoroutine(ExecuteAfterTime(1f));
            Debug.Log("Win From Func");
            return true;
        }
        return false;
    }

    bool move(GameBlock block, char direction)
    {
        //Debug.Log(hidden.Count);
        try
        {
            Vector3 newPos;
            Vector3 temp = block.gameObject.transform.position;
            sfx_src.clip = game_sfx[0];
            sfx_src.Play();
            switch (direction)
            {
                case 'u':
                    newPos = new Vector3(block.gameObject.transform.position.x, block.gameObject.transform.position.y, block.gameObject.transform.position.z + 1);
                    break;
                case 'd':
                    newPos = new Vector3(block.gameObject.transform.position.x, block.gameObject.transform.position.y, block.gameObject.transform.position.z - 1);
                    break;
                case 'l':
                    newPos = new Vector3(block.gameObject.transform.position.x - 1, block.gameObject.transform.position.y, block.gameObject.transform.position.z);
                    break;
                case 'r':
                    newPos = new Vector3(block.gameObject.transform.position.x + 1, block.gameObject.transform.position.y, block.gameObject.transform.position.z);
                    break;
                default:
                    newPos = block.gameObject.transform.position;
                    break;
            }

            if (block.isYou)
            {
                switch (direction)
                {
                    case 'u':
                        block.gameObject.transform.rotation = Quaternion.Euler(0, 270, 90);
                        break;
                    case 'd':
                        block.gameObject.transform.rotation = Quaternion.Euler(0, 90, 90);
                        break;
                    case 'l':
                        block.gameObject.transform.rotation = Quaternion.Euler(0, 180, 90);
                        break;
                    case 'r':
                        block.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
                        break;
                    default:
                        block.gameObject.transform.rotation = Quaternion.Euler(0, 90, 90);
                        break;
                }
            }

            if (newPos.x < 0 || newPos.x > WIDTH || newPos.z < 0 || newPos.z > WIDTH)
            {
                return false;
            }
            var currentBlock = grid[((int)newPos.z * WIDTH) + (int)newPos.x];

            Debug.Log($"type: {currentBlock?.blockType}");

            if (currentBlock is null)
            {
                block.gameObject.transform.position = newPos;
                grid[((int)newPos.z * WIDTH) + (int)newPos.x] = block;
                grid[((int)temp.z * WIDTH) + (int)temp.x] = null;

            }
            else if (currentBlock.isStop)
            {
                return false;
            }
            else if (currentBlock.isPush)
            {
                bool check = move(grid[((int)newPos.z * WIDTH) + (int)newPos.x], direction);
                if (check)
                {
                    block.gameObject.transform.position = newPos;
                    grid[((int)newPos.z * WIDTH) + (int)newPos.x] = block;
                    grid[((int)temp.z * WIDTH) + (int)temp.x] = null;
                }
                return check;
            }
            else if (currentBlock.isWin && block.isYou)
            {
                sfx_src.clip = game_sfx[4];
                sfx_src.Play();
                winScreen.SetActive(true);
                block.gameObject.transform.position = newPos;
                grid[((int)newPos.z * WIDTH) + (int)newPos.x] = block;
                grid[((int)temp.z * WIDTH) + (int)temp.x] = null;

                int currentLevel = SceneManager.GetActiveScene().buildIndex;
                if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
                {
                    PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
                }
                Debug.Log(PlayerPrefs.GetInt("levelsUnlocked") + "Unlocked");
                StartCoroutine(ExecuteAfterTime(1f));
            }
            else if (currentBlock.isDefeat)
            {
                Debug.Log($"skull is defeat {currentBlock.isDefeat}");

                if (block.isYou)
                {
                    sfx_src.clip = game_sfx[1];
                    sfx_src.Play();
                    grid.Remove(playerBlock);
                    Destroy(playerBlock.gameObject);
                    blocks.Remove(playerBlock);

                }
                else
                {

                    block.gameObject.transform.position = newPos;
                    grid[((int)newPos.z * WIDTH) + (int)newPos.x] = block;
                    grid[((int)temp.z * WIDTH) + (int)temp.x] = null;
                    return true;
                }
            }
            else if (currentBlock.isSink)
            {
                sfx_src.clip = game_sfx[2];
                sfx_src.Play();
                Destroy(currentBlock.gameObject);
                Destroy(block.gameObject);

                grid[((int)currentBlock.gameObject.transform.position.z * WIDTH) + (int)currentBlock.gameObject.transform.position.x] = null;
                grid[((int)block.gameObject.transform.position.z * WIDTH) + (int)block.gameObject.transform.position.x] = null;


                blocks.Remove(block);
                blocks.Remove(currentBlock);

            }
            else if (currentBlock.isHot)
            {
                if (block.isMelt)
                {
                    sfx_src.clip = game_sfx[2];
                    sfx_src.Play();
                    grid.Remove(playerBlock);
                    Destroy(playerBlock.gameObject);
                    blocks.Remove(playerBlock);

                }
                else
                {

                    block.gameObject.transform.position = newPos;
                    grid[((int)newPos.z * WIDTH) + (int)newPos.x] = block;
                    grid[((int)temp.z * WIDTH) + (int)temp.x] = null;
                    return true;
                }

            }
            else if (!currentBlock.isStop && !currentBlock.isPush)
            {
                ignore[((int)newPos.z * WIDTH) + (int)newPos.x] = grid[((int)newPos.z * WIDTH) + (int)newPos.x];
                block.gameObject.transform.position = newPos;
                grid[((int)newPos.z * WIDTH) + (int)newPos.x] = block;
                grid[((int)temp.z * WIDTH) + (int)temp.x] = null;
            }
            checkback();


            return true;
        }
        catch(NullReferenceException e)
        {
            return false;
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(0);
        // Code to execute after the delay
    }
    void checkback()
    {
        for(int i = 0; i < ignore.Length; i++)
        {
            if (ignore[i] is not null)
            {
                int x = (int)ignore[i].gameObject.transform.position.x;
                int z = (int)ignore[i].gameObject.transform.position.z;

                if (grid[z * WIDTH + x] is null)
                {
                    grid[z * WIDTH + x] = ignore[i];
                    ignore[i] = null;
                }
            }
        }
    }
    
    

    /// TODO: add new
    class GameBlock
    {
        public GameObject gameObject;
        public bool isPush;
        public bool isStop;
        public bool isWin;
        public bool isYou;
        public bool isDefeat;
        public bool isSink;
        public bool isMelt;
        public bool isHot;

        public WordType wordType;
        public BlockType blockType;

        public GameBlock(GameBlock block)
        {
            gameObject = Instantiate(block.gameObject, null);
            wordType = block.wordType;
            blockType = block.blockType;
            isPush = block.isPush;
            isStop = block.isStop;
            isWin = block.isWin;
        }

        public GameBlock(GameObject game, WordType wordType, BlockType blockType)
        {
            gameObject = game;
            this.wordType = wordType;
            this.blockType = blockType;
            isWin = false;
            isStop = false;
            isYou = false;
            isPush = false;
            isDefeat = false;
            isSink = false;
            isMelt = false;
            isHot = false;


            if (wordType != WordType.NotWord)
            {
                isPush = true;
            }
            
            if(blockType == BlockType.Player)
            {
                isYou = true;
            }
        }
    }
}