namespace ArkNet.Model
{
    public class ArkLoaderStatusSync
    {
        public string Id { get; set; }
        public bool Syncing { get; set; }
        public long Blocks { get; set; }
        public long Height { get; set; }
    }
}