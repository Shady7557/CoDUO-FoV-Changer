using System;
using System.Collections.Generic;
using System.IO;

namespace ShadyPool
{
    internal static class Pool<T> where T : class
    {
        public static Pool.PoolCollection<T> Collection;
    }

    public static class Pool
    {
        public static Dictionary<Type, ICollection> Directory = new Dictionary<Type, ICollection>();

        public static void FreeList<T>(ref List<T> obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            obj.Clear();
            FreeInternal(ref obj);

            if (obj != null)
                throw new SystemException(nameof(FreeList) + " failed to set obj to null");
        }

        public static void FreeMemoryStream(ref MemoryStream obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            obj.Position = 0L;
            obj.SetLength(0L);
            FreeInternal(ref obj);
            if (obj != null)
                throw new SystemException(nameof(FreeMemoryStream) + " failed to set memory stream to null");
        }

        public static void Free<T>(ref T obj) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            FreeInternal(ref obj);
        }

        public static void FreeDynamic<T>(ref T obj) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (!Directory.TryGetValue(obj.GetType(), out ICollection collection))
                throw new ArgumentException("Failed to find collection for type " + obj.GetType());

            collection.Add(obj);
            obj = default;
        }

        private static void FreeInternal<T>(ref T obj) where T : class
        {
            FindCollection<T>().Add(obj);
            obj = default;
        }

        public static T Get<T>() where T : class, new()
        {
            var collection = FindCollection<T>();
            if (collection.ItemsInStack > 0)
            {
                collection.ItemsInStack--;
                collection.ItemsInUse++;
                T obj = collection.Buffer[collection.ItemsInStack];
                collection.Buffer[collection.ItemsInStack] = default;
                if (obj is IPooled pooled)
                    pooled.LeavePool();
                collection.ItemsTaken++;
                return obj;
            }

            collection.ItemsCreated++;
            collection.ItemsInUse++;

            return Activator.CreateInstance<T>();
        }

        public static List<T> GetList<T>() => Get<List<T>>();

        public static void ResizeBuffer<T>(int size) where T : class => Array.Resize(ref FindCollection<T>().Buffer, size);

        public static void FillBuffer<T>(int count = 2147483647) where T : class, new()
        {
            var collection = FindCollection<T>();
            for (int index = 0; index < count && collection.ItemsInStack < collection.Buffer.Length; ++index)
            {
                collection.Buffer[collection.ItemsInStack] = Activator.CreateInstance<T>();
                collection.ItemsInStack++;
            }
        }

        public static PoolCollection<T> FindCollection<T>() where T : class
        {
            var poolCollection = Pool<T>.Collection;
            if (poolCollection == null)
            {
                poolCollection = new PoolCollection<T>();
                Pool<T>.Collection = poolCollection;
                Directory.Add(typeof(T), Pool<T>.Collection);
            }
            return poolCollection;
        }

        public static void Clear()
        {
            foreach (var kvp in Directory)
                kvp.Value.Reset();
        }

        public interface IPooled
        {
            void EnterPool();

            void LeavePool();
        }

        public interface ICollection
        {
            long ItemsInStack { get; }

            long ItemsInUse { get; }

            long ItemsCreated { get; }

            long ItemsTaken { get; }

            long ItemsSpilled { get; }

            void Reset();

            void Add(object obj);
        }

        public class PoolCollection<T> : ICollection where T : class
        {
            public T[] Buffer;

            public long ItemsInStack { get; set; }

            public long ItemsInUse { get; set; }

            public long ItemsCreated { get; set; }

            public long ItemsTaken { get; set; }

            public long ItemsSpilled { get; set; }

            public PoolCollection() => Reset();

            public void Reset()
            {
                Buffer = new T[512];
                ItemsInStack = 0L;
                ItemsInUse = 0L;
                ItemsCreated = 0L;
                ItemsTaken = 0L;
                ItemsSpilled = 0L;
            }

            public void Add(T obj)
            {
                if (ItemsInStack >= Buffer.Length)
                {
                    ItemsSpilled++;
                    ItemsInUse--;
                }
                else
                {
                    Buffer[ItemsInStack] = obj;

                    ItemsInStack++;
                    ItemsInUse--;

                    if (obj is IPooled pooled)
                        pooled.EnterPool();
                }
            }

            void ICollection.Add(object obj) => Add((T)obj);
        }
    }
}
