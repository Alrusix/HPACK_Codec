using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPACK_Codec.core
{
	public struct HeaderField
	{
		public string Name;
		public string Value;
		public IndexType IndexType;

		public HeaderField(string name, string value, IndexType indexType)
		{
			Name = name;
			Value = value;
			IndexType = indexType;
		}
	}
}
