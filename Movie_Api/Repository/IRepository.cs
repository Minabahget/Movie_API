namespace Movie_Api.Repository
{

    public interface IRepositry<Entity> where Entity : class
    {
        public bool Add(Entity entity);

        public List<Entity> GetAll();

        public Entity Details(int id);

        public bool Update(Entity entity);

        public bool Remove(Entity entity);

    }
}

