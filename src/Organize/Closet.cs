namespace Organize
{
    public class Closet
    {
        public StorageBin StorageBin { get; private set; }

        public Closet()
        {
            StorageBin = new StorageBin();
        }
    }
}
