using ArkNet.Service;
using System.Numerics;

namespace ArkNet.Model
{
    public class ArkError
    {
        public bool Success { get; set; }
        public string Error { get; set; }

        public ArkError()
        {
            Success = true;
        }
    }
}