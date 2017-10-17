using ArkNet.Model.BaseModels;

namespace ArkNet.Model.Loader
{
    public class ArkLoaderStatus : ArkResponseBase
    {
        public bool Loaded { get; set; }
        public long Now { get; set; }
        public long BlocksCount { get; set; }
    }
}