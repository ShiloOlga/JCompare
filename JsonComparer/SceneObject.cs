using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace JsonComparer
{
    public class SceneObject
    {
        [JsonProperty("type")]
        public string ObjectType { get; set; }
        [JsonProperty("parent")]
        public string Parent { get; set; }
        [JsonProperty("sprite")]
        public string SpriteName { get; set; }
        [JsonProperty("size")]
        public Size Size { get; set; }
        [JsonProperty("position")]
        public Position Position { get; set; }
    }

    public struct Size
    {
        [JsonProperty("h")]
        public int Height;
        [JsonProperty("w")]
        public int Width;
    }

    public struct Position
    {
        [JsonProperty("x")]
        public double X;
        [JsonProperty("y")]
        public double Y;
        [JsonProperty("z")]
        public int DrawOrder;
    }
}
