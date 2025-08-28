using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPACK_Codec.core
{
	public class HpackDynamicTable
	{
		public List<(string Name, string Value)> _dynamicTable;// 动态表存储头部名称和值的列表
        public int _maxSize;// 动态表的最大大小
		private int _currentSize;// 当前动态表的大小

		public HpackDynamicTable(int maxSize = 4096)
		{
			_dynamicTable = new List<(string Name, string Value)>();
			_maxSize = maxSize;
			_currentSize = 0;
		}
		// 向动态表中添加新条目
		public void AddEntry(string name, string value)
		{
			int entrySize = name.Length + value.Length;// 计算新条目的大小
													   // 如果添加的条目会超过动态表大小，移除旧条目
			while (_currentSize + entrySize > _maxSize && _dynamicTable.Count > 0)
			{
				RemoveOldestEntry();// 移除最旧的条目
			}
			_dynamicTable.Insert(0, (name, value)); // 插入新条目
			Log.Info!("添加动态表条目: " + name + ": " + value);
            _currentSize += entrySize;
		}
		// 从动态表中获取条目
		public (string Name, string Value) GetEntry(int index)
		{
			if (index >= 0 && index < _dynamicTable.Count)
			{
				return _dynamicTable[index];// 返回指定索引的条目
			}
			throw new ArgumentOutOfRangeException("无效的动态表索引");
		}
		// 移除最老的条目
		private void RemoveOldestEntry()
		{
			if (_dynamicTable.Count > 0)
			{
				var oldestEntry = _dynamicTable[^1];// 获取最后一个条目
				_currentSize -= oldestEntry.Name.Length + oldestEntry.Value.Length;
				_dynamicTable.RemoveAt(_dynamicTable.Count - 1);
				Log.Info!("移除动态表最老的条目: " + oldestEntry.Name + ": " + oldestEntry.Value);
            }
		}
		// 获取动态表大小
		public int GetSize() => _dynamicTable.Count;
		
		public int GetCurrentSize() => _currentSize; // 返回当前动态表的大小
													
		public void Clear()
		{
			_dynamicTable.Clear();
			_currentSize = 0;
		}
        // 修改动态表大小
		public void UpdateMaxSize(int newSize)
		{
			Log.Info!("新动态表大小："+newSize);

            _maxSize = newSize;
			while (_currentSize > _maxSize && _dynamicTable.Count > 0)
			{
				RemoveOldestEntry();
			}
        }
    }

}
