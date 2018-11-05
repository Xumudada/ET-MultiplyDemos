using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
    [ObjectSystem]
    public class GamerComponentAwakeSystem : AwakeSystem<GamerComponent>
    {
        public override void Awake(GamerComponent self)
        {
            self.Awake();
        }
    }

    public class GamerComponent : Component
    {
        public static GamerComponent Instance { get; private set; }

        public User MyUser;

        /// <summary>
        /// UserID Gamer
        /// </summary>
        private readonly Dictionary<long, Gamer> idGamers = new Dictionary<long, Gamer>();

        public void Awake()
        {
            Instance = this;
        }

        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            base.Dispose();

            foreach (Gamer gamer in this.idGamers.Values)
            {
                gamer.Dispose();
            }

            this.idGamers.Clear();

            Instance = null;
        }

        public void Add(Gamer gamer)
        {
            this.idGamers.Add(gamer.UserID, gamer);
        }

        public Gamer Get(long userid)
        {
            Gamer gamer;
            this.idGamers.TryGetValue(userid, out gamer);
            return gamer;
        }

        public void Remove(long userid)
        {
            Gamer gamer;
            this.idGamers.TryGetValue(userid, out gamer);
            this.idGamers.Remove(userid);
            gamer?.Dispose();
        }

        public void RemoveNoDispose(long userid)
        {
            this.idGamers.Remove(userid);
        }

        public int Count
        {
            get
            {
                return this.idGamers.Count;
            }
        }

        public Gamer[] GetAll()
        {
            return this.idGamers.Values.ToArray();
        }
    }
}