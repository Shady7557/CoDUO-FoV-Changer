using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CoD_Widescreen_Suite
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
            if (index < 0) 
                throw new ArgumentOutOfRangeException(nameof(index));


            return _memoryIndex.TryGetValue(index, out var memory) ? memory : null;
        }
    }
}
