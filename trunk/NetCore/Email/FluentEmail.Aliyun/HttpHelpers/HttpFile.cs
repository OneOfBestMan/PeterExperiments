using System.IO;

namespace FluentEmail.Aliyun.HttpHelpers
{
    public class HttpFile
    {
        public string ParameterName { get; set; }
        public string Filename { get; set; }
        public Stream Data { get; set; }
        public string ContentType { get; set; }
    }
}
