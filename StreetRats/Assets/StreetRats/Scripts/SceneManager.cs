/*  SceneManager
 * 
 *  Author: Jimmy Nguyen
 *  Email:  Jimmy@Jimmyworks.net
 *  
 *  Description:
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StreetRats
{
    public enum GameState
    {
        START,
        INTRO,
        MAIN_MENU,
        LEVEL_1,
        LEVEL_2,
        EXIT,
        COUNT
    }

    public enum GameTransition
    {
        START_GAME,
        LOAD_INTRO,
        LOAD_MAINMENU,
        LOAD_LVL1,
        LOAD_LVL2,
        END_GAME,
        COUNT
    };

    public class SceneManager : MonoBehaviour
    {
        [SerializeField]
        private string 
            IntroScene, 
            MainMenuScene, 
            Level1Scene, 
            Level2Scene;

        private static SceneManager singleton;
        private Action[,] finiteStateMachine;
        private string activeScene;
        private GameState currentState;
        public GameState state { get { return this.currentState; } }
        Dictionary<GameState, string> sceneDictionary;

        private void Start()
        {
            this.VerifySingleton();
            this.InitializeFiniteStateMachine();
            this.InitializeSceneDictionary();

            this.currentState = GameState.START;
            ProcessEvent(GameTransition.START_GAME);
        }

        /// <summary>
        /// Initialize the Deterministic Finite State Machine with the possible transitions it will support
        /// </summary>
        private void InitializeFiniteStateMachine()
        {
            // DFS Machine where matrix is states x transitions
            this.finiteStateMachine = new Action[(int)GameState.COUNT, (int)GameTransition.COUNT]
            {
                                        // Transitions //
                    // States //        StartGame      LoadIntro        LoadMainMenu    Load_Lvl1      Load_Lvl2     EndGame
                    /* Start      */    { StartGame,   null,            null,           null,          null,         null }, 
                    /* Intro      */    { null,        null,            LoadMainMenu,   LoadLevel1,    null,         null }, 
                    /* Main Menu  */    { null,        LoadIntro,       null,           LoadLevel1,    LoadLevel2,   EndGame },
                    /* Level 1    */    { null,        null,            null,           LoadLevel1,    LoadLevel2,   EndGame },
                    /* Level 2    */    { null,        null,            LoadMainMenu,   null,          LoadLevel2,   EndGame },
                    /* Exit       */    { null,        null,            null,           null,          null,         null },
            };
        }

        /// <summary>
        /// Public method to transition between scenes
        /// </summary>
        /// <param name="transition"> 
        /// Game Transition event which is the edge that the finite-state machine 
        /// will use to move from one state node to the next 
        /// </param>
        public static void ProcessEvent(GameTransition transition)
        {
            singleton.finiteStateMachine[(int)singleton.currentState, (int)transition].Invoke();
        }

        private void StartGame()
        {
            // Handle State Transition
            this.currentState = GameState.INTRO;
            this.activeScene = IntroScene;

            // Load Intro Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(IntroScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        private void LoadIntro()
        {
            // Handle State Transition
            this.currentState = GameState.INTRO;

            // Remove Old Scene and Load New Scene
            this.UnloadOld_LoadNew(IntroScene);
        }

        private void LoadMainMenu()
        {
            // Handle State Transition
            this.currentState = GameState.MAIN_MENU;

            // Remove Old Scene and Load New Scene
            this.UnloadOld_LoadNew(MainMenuScene);
        }

        private void LoadLevel1()
        {
            // Handle State Transition
            this.currentState = GameState.LEVEL_1;

            // Remove Old Scene and Load New Scene
            this.UnloadOld_LoadNew(Level1Scene);
        }

        private void LoadLevel2()
        {
            // Handle State Transition
            this.currentState = GameState.LEVEL_2;

            // Remove Old Scene and Load New Scene
            this.UnloadOld_LoadNew(Level2Scene);
        }

        private void ReturnMainMenu()
        {
            // Handle State Transition
            this.currentState = GameState.MAIN_MENU;

            // Remove Old Scene and Load New Scene
            this.UnloadOld_LoadNew(MainMenuScene);
        }

        private void EndGame()
        {
            // Handle State Transition
            this.currentState = GameState.EXIT;

            Application.Quit();
            Debug.Log("Quiting game...");

        }

        private void UnloadOld_LoadNew(string newScene)
        {
            // Unload the Main Menu Scene
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetSceneByName(this.activeScene));
            // Load the Intro Scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(newScene, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            this.activeScene = newScene;
        }

        private void VerifySingleton()
        {
            if (singleton == null)
            {
                singleton = this;
            }
            else
            {
                Debug.LogWarning("Two StreetRats.SceneManager components exist in project.  Deleting this instance...");
                Destroy(this);
            }
        }

        /// <summary>
        /// Initialize the scene dictionaries
        /// </summary>
        private void InitializeSceneDictionary()
        {
            this.sceneDictionary = new Dictionary<GameState, string>()
            {
                { GameState.INTRO, IntroScene },
                { GameState.MAIN_MENU, MainMenuScene },
                { GameState.LEVEL_1, Level1Scene },
                { GameState.LEVEL_2, Level2Scene },
            };

            this.VerifyAllScenes();
        }

        /// <summary>
        /// Verify that all scenes listed actually exist and are loadable
        /// </summary>
        private void VerifyAllScenes()
        {
            foreach (string scene in this.sceneDictionary.Values)
            {
                if (!Application.CanStreamedLevelBeLoaded(scene))
                {
                    throw new Exception("Missing scene reference or scene name mismatch: " + scene);
                }
            }
        }

        /// <summary>
        /// Loads next default scene
        /// </summary>
        public static void LoadNextScene()
        {
            string sceneName = string.Empty;

            // Check scene state machine
            throw new NotImplementedException();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}

