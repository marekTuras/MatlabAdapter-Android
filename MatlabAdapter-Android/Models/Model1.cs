using System.Collections.Generic;

namespace MatlabAdapter_Android.Models
{
    public class Model1
    {
        public List<Block> listOfBlocks;
    }
    public class Block
    {
        public string simulinkName { get; set; }
        public string defaultValue { get; set; }

        public Block(string simulinkName, string defaultValue)
        {
            this.simulinkName = simulinkName ?? "";
            this.defaultValue = defaultValue ?? "";
        }
    }
}
