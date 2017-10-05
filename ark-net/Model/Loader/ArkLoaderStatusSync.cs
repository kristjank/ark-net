using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Loader
{
    public class ArkLoaderStatusSync : ArkResponseBase
    {
        public string Id { get; set; }
        public bool Syncing { get; set; }
        public long Blocks { get; set; }
        public long Height { get; set; }
    }
}