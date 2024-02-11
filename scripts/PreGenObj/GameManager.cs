using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Physics_Engine.scripts.PreGenObj
{
    public class GameManager
    {


        //Dictionary with priority list for each scene
        Dictionary<scenes,Scene> scenesInGame = new Dictionary<scenes,Scene>();
        uint currentScene = 0;//use uint so the nuber is always poitive
        CollisionComponent _collisionComponent;
        bool debug;

        public GameManager(CollisionComponent _collisionComponent, bool debug) 
        {
            this._collisionComponent = _collisionComponent;
            this.debug = debug;
        }


        public void Initialize(int SceneNum, CollisionComponent _collisionComponent)
        {
            //finds all scenes in files and loads them into the dictionary when the file is loaded

            scenesInGame[(scenes)currentScene].Initialize(_collisionComponent);//initialises each scene dependint on what is the current scene
            
        }

        public void Load(int SceneNum)
        {
            //performs load functions
        }

        public void Update(int SceneNum, CollisionComponent _collisionComponent, GameTime _GTtime, GraphicsDeviceManager _graphics)
        {
            //performs update function
            scenesInGame[(scenes)currentScene].Update(_GTtime, _collisionComponent, _graphics);
        }

        public void DebugDraw(int SceneNum, SpriteBatch _spriteBatch)
        {
            scenesInGame[(scenes)currentScene].DebugDraw(_spriteBatch);
        }
        public void Draw(int SceneNum, SpriteBatch _spriteBatch)
        {
            scenesInGame[(scenes)currentScene].Draw(_spriteBatch);
        }

        public void Save() 
        {
            //saves any data when game closed, people will need to write this themselves
        }

        public void loadScene(int nextScene)
        {
            //needs to clear the collision components
            foreach(GameObject _object in scenesInGame[(scenes)currentScene].objects)
            {
                if(_object.collider != null)
                {
                    _collisionComponent.Remove(_object.collider);
                }
                
            }
            //re-initialize each scene
        }

        public void AddScene(scenes scene, Scene madeScene)
        {
           scenesInGame.Add(scene, madeScene); 
        }
        

    }
    //template for scenes to create
    //might need to be a class idk
    public interface Scene
    {
        List<GameObject> objects { get; set; }//need to make priority list or even que or stack
        int SceneNum { get; set; }

        //need to change the loops to have something more robust

        public void Initialize(CollisionComponent _collisionComponent)
        {
            //will need to clear the collision components 
            

            foreach(GameObject _object in objects)
            {
                if (_object.collider != null)
                {
                    _collisionComponent.Insert(_object.collider);//adds the collider to the 
                }
            }
        }
        public void Load()
        {
            // temp code
            foreach (var item in objects)
            {
                item.Load();
            }
        }

        public void Update(GameTime _gameTime, CollisionComponent _collisionComponent, GraphicsDeviceManager _graphics)
        {
            // temp code
            foreach (var item in objects)
            {
                item.Update(_graphics);
            }
            _collisionComponent.Update(_gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            // temp code
            foreach (var item in objects)
            {
                item.Draw(_spriteBatch);
            }
        }
        public void DebugDraw(SpriteBatch _spriteBatch)
        {
            // temp code
            foreach (var item in objects)
            {
                item.DebugDraw(_spriteBatch);
            }
        }

    }


}
