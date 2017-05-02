using GameEngine.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Managers
{
    public class ComponentManager
    {

        private static readonly ComponentManager instance = new ComponentManager();

        private static Dictionary<Type, Dictionary<ulong, Component>> components;

        private static ulong CurrentId;

        private ComponentManager()
        {
            components = new Dictionary<Type, Dictionary<ulong, Component>>();
        }

        public static ulong GetCurrentId()
        {
            return CurrentId;
        }

        public static ulong GetNewId()
        {
            return ++CurrentId;
        }

        public static void StoreComponent(ulong id, Component comp)
        {
            Type type = comp.GetType();

            if (!components.ContainsKey(type))
                components.Add(type, new Dictionary<ulong, Component>());

            components[type].Add(id, comp);
        }

        public static List<ulong> GetAllEntitiesWithComp<T>() where T : Component
        {
            if (components.ContainsKey(typeof(T)))
            {
                return components[typeof(T)].Keys.ToList();
            }
            return null;
        }

        public static T GetComponent<T>(ulong id)
        {
            Type type = typeof(T);

            if (components.ContainsKey(type))
                if (components[type].ContainsKey(id))
                    return (T)(Object)components[type][id];

            return (T)(Object)null;
        }

        public static List<Component> GetComponents<T>()
        {
            Type type = typeof(T);

            if (components.ContainsKey(type))
                return components[type].Values.ToList();

            return new List<Component>();
        }

        public static bool HasComponent<T>(ulong id)
        {
            Type type = typeof(T);
            if (components.ContainsKey(type))
                if (components[type].ContainsKey(id))
                    return true;
            return false;
        }

        public static List<ulong> GetAllIds<T>()
        {
            Type type = typeof(T);

            if (components.ContainsKey(type))
                return components[type].Keys.ToList<ulong>();

            return null;
        }


    }
}