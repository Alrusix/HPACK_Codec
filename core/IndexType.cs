using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPACK_Codec.core
{
	public enum IndexType:byte
	{
		// index为0 则为 Indexed Name，否则为 New Name
		// Indexed Name:  字段的name在动态表或静态表中
		// New Name: 新字段，带有自定义的name和value

		//    0   1   2   3   4   5   6   7
		//  +---+---+---+---+---+---+---+---+
		//  | 1 |        Index (7+)         |
		//  +---+---------------------------+
		//	name和value都存在于动态表或静态表中
		/// <summary>
		/// Indexed Header Field Representation
		/// </summary>
		Index = 0x80,

		//	   0   1   2   3   4   5   6   7
		//   +---+---+---+---+---+---+---+---+
		//   | 0 | 1 |      Index (6+)       |
		//   +---+---+-----------------------+
		/// <summary>
		///  Literal Header Field with Incremental Indexing
		///  加入动态表
		/// </summary>
		IndexedName = 0x40,

		//	   0   1   2   3   4   5   6   7
		//   +---+---+---+---+---+---+---+---+
		//	 | 0 | 0 | 0 | 0 |  Index (4+)   |
		//	 +---+---+-----------------------+
		/// <summary>
		/// Literal Header Field without Indexing
		/// 不加入动态表
		/// </summary>
		NoIndexing = 0x0,

		//	   0   1   2   3   4   5   6   7
		//	 +---+---+---+---+---+---+---+---+
		//	 | 0 | 0 | 0 | 1 |  Index (4+)   |
		//	 +---+---+-----------------------+
		/// <summary>
		/// Literal Header Field Never Indexed
		/// 不加入动态表，代理转发时保持不变
		/// </summary>
		NeverIndexed = 0x10,
	}
}
