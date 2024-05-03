namespace Entities
{

    /// <summary>
    /// Represents an entity in the game.
    /// </summary>
    public class Entity
    {
        public int Id { get; }
        private Dictionary<Type, object> components = new Dictionary<Type, object>();

        public Entity(int id)
        {
            this.Id = id;
        }

        public void AddComponent<T>(T component)
        {
            components[typeof(T)] = component;
        }

        public T GetComponent<T>() where T : class
        {
            if (components.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }
            return null;
        }
    }


}