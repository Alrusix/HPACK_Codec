using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPACK_Codec.core
{
	public class HpackStaticTable
	{
		public static readonly (string Name, string Value)[] StaticTable = new (string, string)[]
		{
		(":authority", ""),
		(":method", "GET"),
		(":method", "POST"),
		(":path", "/"),
		(":path", "/index.html"),
		(":scheme", "http"),
		(":scheme", "https"),
		(":status", "200"),
		(":status", "204"),
		(":status", "206"),
		(":status", "304"),
		(":status", "400"),
		(":status", "404"),
		(":status", "500"),
		("accept-charset", ""),
		("accept-encoding", "gzip, deflate"),
		("accept-language", ""),
		("accept-ranges", ""),
		("accept", ""),
		("access-control-allow-origin", ""),
		("age", ""),
		("allow", ""),
		("authorization", ""),
		("cache-control", ""),
		("content-disposition", ""),
		("content-encoding", ""),
		("content-language", ""),
		("content-length", ""),
		("content-location", ""),
		("content-range", ""),
		("content-type", ""),
		("cookie", ""),
		("date", ""),
		("etag", ""),
		("expect", ""),
		("expires", ""),
		("from", ""),
		("host", ""),
		("if-match", ""),
		("if-modified-since", ""),
		("if-none-match", ""),
		("if-range", ""),
		("if-unmodified-since", ""),
		("last-modified", ""),
		("link", ""),
		("location", ""),
		("max-forwards", ""),
		("proxy-authenticate", ""),
		("proxy-authorization", ""),
		("range", ""),
		("referer", ""),
		("refresh", ""),
		("retry-after", ""),
		("server", ""),
		("set-cookie", ""),
		("strict-transport-security", ""),
		("transfer-encoding", ""),
		("user-agent", ""),
		("vary", ""),
		("via", ""),
		("www-authenticate", "")
		};

		public static (string Name, string Value) GetEntry(int index)
		{
			if (index >= 0 && index < StaticTable.Length)
			{
				return StaticTable[index];
			}
			throw new ArgumentOutOfRangeException("无效的静态表索引");
		}
		// 获取静态表的大小
		public static int GetSize() => StaticTable.Length;
	}

}
