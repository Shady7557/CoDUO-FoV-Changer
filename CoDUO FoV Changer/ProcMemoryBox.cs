using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer
{
    public class ProcessMemoryBox : ComboBox
    {
        private readonly Dictionary<int, Memory> _memoryIndex = new Dictionary<int, Memory>();
        public void AddProcessMemory(Memory memory)
        {
            var newIndex = Items.Count < 1 ? 0 : Items.Count;

            _memoryIndex[newIndex] = memory ?? throw new ArgumentNullException(nameof(memory));
            Items.Add(memory.ToString());

        }

        public Memory GetMemoryFromIndex(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

            if (_memoryIndex.TryGetValue(index, out Memory memory)) return memory;

            return null;
        }
    }
}
