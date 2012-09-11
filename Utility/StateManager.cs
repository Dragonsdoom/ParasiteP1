using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1.Utility
{
    /// <summary>
    /// StateManager manages the GameStates.
    /// </summary>
    public static class StateManager
    {
        static LinkedList<GameState> currentStates = new LinkedList<GameState>();
        static Game1 game;

        /// <summary>
        /// Simply adds the inital state to the internal collection.
        /// </summary>
        public static void InitalizeStateManager<T>(Game newGame) where T : GameState
        {
            game = (Game1)newGame;
            T classInstance = Activator.CreateInstance(typeof(T), game) as T;
            currentStates.AddLast(classInstance);
        }

        /// <summary>
        /// Adds a new state to the StateManager's collection. Removes duplicate types.
        /// </summary>
        /// <typeparam name="T">Any subclass of GameState</typeparam>
        public static void QueueState<T>() where T : GameState
        {
            //Type classType = typeof(T);
            //ConstructorInfo classConstructor = classType.GetConstructor(new Type[] { classType.GetType() });
            T classInstance = Activator.CreateInstance(typeof(T), game) as T;

            {
                for (int i = 0; i < currentStates.Count; i++)
                {
                    if (currentStates.ElementAt(i) is T)
                    {
                        currentStates.Remove(currentStates.ElementAt(i));
                    }
                }
                currentStates.Clear();
                currentStates.AddLast(classInstance);
            }
        }

        /// <summary>
        /// Returns the current state.
        /// </summary>
        /// <returns>The current state.</returns>
        public static GameState GetCurrentState()
        {
            return currentStates.Last();
        }

        /// <summary>
        /// Checks to see if the StateManager currently contains a type of GameState.
        /// </summary>
        /// <typeparam name="T">The type to check for.</typeparam>
        /// <returns>true or false.</returns>
        public static bool Contains<T>() where T : GameState
        {
            foreach (GameState s in currentStates)
            {
                if (s is T) return true;
            }
            return false;
        }

        /// <summary>
        /// Updates the active state.
        /// </summary>
        public static void UpdateInActiveStates()
        {
            currentStates.Last().Update();
        }

        /// <summary>
        /// Draws the 2D elements of the active state.
        /// </summary>
        /// <param name="sb">SpriteBatch</param>
        public static void DrawInActiveStates(SpriteBatch sb)
        {
            foreach (GameState s in currentStates)
            { 
                s.Draw(sb); 
            }
        }
        
        /// <summary>
        /// Draws the 3D elements of the active state.
        /// </summary>
        public static void Draw3DInActiveStates()
        {
            currentStates.Last().Draw3D();
        }
    }
}
