using HPACK_Codec.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HPACK_Codec
{
	public class HPACK_Encode
	{
		/// <summary>
		/// 编码
		/// </summary>
		/// <param name="name">键</param>
		/// <param name="value">值</param>
		/// <param name="hpackDynamicTable">动态表</param>
		/// <param name="EnableHuffman">启用哈夫曼</param>
		/// <param name="indexType">索引类型，默认加入动态表</param>
		/// <returns></returns>
		public static List<byte> Encode(string name, string value, HpackDynamicTable hpackDynamicTable, bool EnableHuffman = true, IndexType indexType = IndexType.IndexedName)
		{

			List<int> nameMatches = new List<int>();

			for (int i = 0; i < HpackStaticTable.StaticTable.Length; i++)
			{
				if (HpackStaticTable.StaticTable[i].Name == name)
				{
					if (HpackStaticTable.StaticTable[i].Value == value)
					{
						return new() { (byte)(0x80 | i + 1) };
					}
					nameMatches.Add(i + 1);
				}
			}
			for (int i = 0; i < hpackDynamicTable.GetSize(); i++)
			{
				if (hpackDynamicTable._dynamicTable[i].Name == name)
				{
					if (hpackDynamicTable._dynamicTable[i].Value == value)
					{
						return EncodeLen(i + 62, 7, 0x80);
					}
					nameMatches.Add(i + 62);
				}
			}

			if (nameMatches.Count > 0)
			{
				// Indexed Name

				(byte N, byte index_Type) = GetPrefixByte(indexType);
				List<byte> result = EncodeLen(nameMatches[0], N, index_Type);

				EncodeValue(result, value, EnableHuffman);
				if (indexType == IndexType.IndexedName || indexType == IndexType.Index)
				{
					hpackDynamicTable.AddEntry(name, value);
				}
				return result;
			}
			else
			{
				// New Name
				List<byte> result = new() { (byte)indexType };

				EncodeValue(result, name, EnableHuffman);
				EncodeValue(result, value, EnableHuffman);

				if (indexType == IndexType.IndexedName|| indexType == IndexType.Index)
				{
					hpackDynamicTable.AddEntry(name, value);
				}
				return result;
			}
		}
		/// <summary>
		/// 整数编码
		/// </summary>
		/// <param name="length">需要编码的值</param>
		/// <param name="N">可用bit</param>
		/// <param name="IndexType">前缀</param>
		/// <returns></returns>
		public static List<byte> EncodeLen(int length, int N, byte IndexType)
		{
			// 初始化结果字节列表
			List<byte> encodedBytes = new List<byte>();

			// 从第一个字节获取有效位的掩码，并处理第一个字节的数值
			int mask = (1 << N) - 1;
			byte firstByte = (byte)(IndexType | (length < mask ? length : mask));
			encodedBytes.Add(firstByte);
			if (length < mask)
			{
				return encodedBytes;
			}
			length -= mask; // 减去已编码的部分

			// 按7位分组编码剩余部分
			while (length > 0)
			{
				// 获取低7位
				byte nextByte = (byte)(length & 0x7F);
				length >>= 7; // 右移7位，为下一次编码准备

				// 若还有剩余位数，则MSB设置为1，否则MSB保持0
				if (length > 0)
				{
					nextByte |= 0x80;
				}

				// 将当前字节添加到列表
				encodedBytes.Add(nextByte);
			}

			return encodedBytes;
		}

		private static (byte, byte) GetPrefixByte(IndexType indexType)
		{
			return indexType switch
			{
				IndexType.IndexedName => (6, 0x40),
				IndexType.NoIndexing => (4, 0x00),
				IndexType.NeverIndexed => (4, 0x10),
				_ => throw new ArgumentOutOfRangeException(nameof(indexType))
			};
		}
		private static byte[] HuffmanEncode(string input)
		{

			StringBuilder bitStream = new StringBuilder();
			foreach (char c in input)
			{
				if (HuffmanEncodeTable.Table.TryGetValue(c, out SymbolCode symbolCode))
				{
					bitStream.Append(symbolCode.BitSequence);
				}
				else
				{
					Console.WriteLine($"字符 '{c}' 无法被编码：未在霍夫曼编码表中找到对应的编码。");
				}
			}
			int paddingBits = (8 - bitStream.Length % 8) % 8;

			if (paddingBits > 0)
			{
				bitStream.Append('1', paddingBits);

			}
			byte[] encodedBytes = new byte[bitStream.Length / 8];
			for (int i = 0; i < bitStream.Length; i += 8)
			{
				string byteString = bitStream.ToString(i, Math.Min(8, bitStream.Length - i));
				encodedBytes[i / 8] = Convert.ToByte(byteString, 2);

			}
			return encodedBytes;
		}

		private static void EncodeValue(List<byte> result, string value, bool enableHuffman)
		{
			byte[] encodedValue = enableHuffman ? HuffmanEncode(value) : Encoding.ASCII.GetBytes(value);
			result.AddRange(enableHuffman ? EncodeLen(encodedValue.Length, 7, 0x80) : EncodeLen(encodedValue.Length, 7, 0x00));
			result.AddRange(encodedValue);
		}


	}
}
