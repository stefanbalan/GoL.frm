namespace GoL
{
    public class Generation<TWorld> where TWorld : class, IWorld, new()
    {
        public Generation()
        {
            Born = new TWorld();
            Dead = new TWorld();
            Live = new TWorld();
        }

        public TWorld Born { get; set; }
        public TWorld Dead { get; set; }
        public TWorld Live { get; set; }
    }
}