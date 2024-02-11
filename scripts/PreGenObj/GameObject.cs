using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame_Physics_Engine.scripts.Colliders;
using MonoGame_Physics_Engine.scripts.Physics_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame_Physics_Engine.scripts.FunctionLibrary;

namespace MonoGame_Physics_Engine.scripts.PreGenObj
{
    public class GameObject
    {
        //parameters

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }//rotation on the z axis(toward the screen) this is only for the drawing of the object
        public Vector2 Scale { get; set; }

        public Rigidbody rb { get; set; }
        
        public ICollider collider { get; set; }

        public Texture2D tex2D { get; set; }

        public List<Component> components { get; set; }

        public Color colour { get; set; }

        public GameObject(Vector2 position, float rotation, Vector2 scale, Rigidbody rb, ICollider collider, Texture2D tex2D, Color colour)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
            this.rb = rb;
            this.collider = collider;
            this.tex2D = tex2D;
            this.colour = colour;
        }

        //suubroutines
        public virtual void Load()
        {
            //temp code
            if(components !=  null)
            {
                foreach (var component in components)
                {
                    component.Update();
                }

            }
        }
        

        public virtual void Update(GraphicsDeviceManager _graphics, float inputForceX = 0, float inputForceY = 0)
        {
            Vector2 _V2inputForce = new Vector2(inputForceX,inputForceY);//allows for the user to apply a force to rb in game object.
            //temp code
            if(rb != null)
            {
                rb.Update(_graphics, _V2inputForce);
            }
            if(components !=  null)
            {
                foreach (var component in components)
                {
                    component.Update();
                }

            }
        }

        public virtual void Draw(SpriteBatch _spriteBatch)
        {
            if(tex2D != null)//this allows it to catch if there is no texture so users can have just an empty 
            {
                _spriteBatch.Draw(
                tex2D,
                Position,
                null,
                colour,
                Rotation,
                new Vector2(tex2D.Width / 2, tex2D.Height / 2) * Scale,//this will multiply each 
                Vector2.One,
                SpriteEffects.None,
                0f
                );
            }

            
        }

        public virtual void DebugDraw(SpriteBatch _spriteBatch)//not required but optional if people want it
        {
            /*try
            {
                rb.DebugDraw(_spriteBatch);
            }
            catch
            {
                //doesn't do anything
            }
            try
            {
                collider.DebugDraw(_spriteBatch);
            }
            catch
            {
                //doesn't do anything
            }*/
            if(components !=  null)
            {
                foreach (var component in components)
                {
                    component.DebugDraw(_spriteBatch);
                }

            }
        }

    }
    public interface Component//this is an interface to add various scripts to a game object(e.g a movement script)NEED TO MAKE EXAMPLE
    {
        public void Load();
        
        public void Update();

        public void Draw(SpriteBatch _spriteBatch);

        public void DebugDraw(SpriteBatch _spriteBatch);
        
    }
}
