using HPACK_Codec.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPACK_Codec
{
    public class HPACK_Decode
    {
   
        public static List<HeaderField> Decode(Span<byte> data, HpackDynamicTable DynamicTable)
        {
            List<HeaderField> headers = new List<HeaderField>();
            int i = 0;

            while (i < data.Length)
            {

                byte firstByte = data[i++];
                if ((firstByte & 0x80) == 0x80)
                {
                    int len = ParseInteger(data, ref i, 7, firstByte);
                    if (DecodeIndexedField(data, ref i, headers, len, DynamicTable) != 0)
                    {
                        return headers;
                    }
                }
                else if ((firstByte & 0xC0) == 0x40)
                {
                    int index = ParseInteger(data, ref i, 6, firstByte);

                    DecodeLiteralWithIncrementalIndexing(data, ref i, headers, DynamicTable, index);
                }
                else if ((firstByte & 0xF0) == 0x00)
                {
                    int index = ParseInteger(data, ref i, 4, firstByte);
                    DecodeLiteralWithoutIndexing(data, ref i, headers, DynamicTable, index);
                }
                else if ((firstByte & 0xF0) == 0x10)
                {
                    int index = ParseInteger(data, ref i, 4, firstByte);
                    DecodeLiteralNeverIndexed(data, ref i, headers, DynamicTable, index);
                }
                else
                {
                    //Console.WriteLine("没有匹配的字段类型，停止解析");
                    break;
                }
            }

            return headers;
        }


        private static string HuffmanDecode(Span<byte> input)
        {
            // 1. 初始化变量
            var byteCount = 0; // 用于记录解码后的字节数
                               // 估算输出缓冲区的初始长度，可能需要根据编码因子调整
            var estLength = (input.Length * 3 + 1) / 2;
            var outBuf = new byte[estLength]; // 创建输出缓冲区

            // 2. 初始化霍夫曼树的根节点
            var treeNode = HuffmanTree.Root;

            // 3. 循环遍历输入字节
            for (int inputByteOffset = 0; inputByteOffset < input.Length; inputByteOffset++)
            {
                var bt = input[inputByteOffset]; // 获取当前字节

                // 4. 从字节的高位到低位读取每一位
                for (int inputBitOffset = 7; inputBitOffset >= 0; inputBitOffset--)
                {
                    // 获取当前位
                    var bit = (bt & 1 << inputBitOffset) >> inputBitOffset;

                    // 5. 根据当前位跟随霍夫曼树的分支
                    if (bit != 0)
                    {
                        treeNode = treeNode.Child1; // 如果是1，走右子树
                    }
                    else
                    {
                        treeNode = treeNode.Child0; // 如果是0，走左子树
                    }

                    // 6. 检查当前节点是否有效
                    if (treeNode == null)
                    {
                        throw new Exception("无效的霍夫曼编码"); // 抛出异常
                    }

                    // 7. 检查是否到达叶子节点（即解码符号）
                    if (treeNode.Value != -1)
                    {
                        // 8. 如果值为EOS（结束符），表示解码完成
                        if (treeNode.Value == 256)
                        {
                            return Encoding.UTF8.GetString(outBuf, 0, byteCount); // 返回解码后的字符串
                        }

                        // 9. 向输出缓冲区添加解码的字节
                        if (byteCount >= outBuf.Length)
                        {
                            // 如果输出缓冲区已满，调整大小
                            Array.Resize(ref outBuf, outBuf.Length * 2); // 扩大输出缓冲区
                        }

                        // 10. 添加当前解码的字节到输出缓冲区
                        outBuf[byteCount++] = (byte)treeNode.Value; // 将当前值存入输出缓冲区
                        treeNode = HuffmanTree.Root; // 重置树节点到根
                    }
                }
            }

            // 11. 返回解码后的字符串
            return Encoding.UTF8.GetString(outBuf, 0, byteCount);
        }

        private static string DecodeValue(Span<byte> data, ref int index)
        {
            int length = data[index];
            bool isHuffmanEncoded = (length & 0x80) == 0x80; // 检查是否使用哈夫曼编码
            length &= 0x7F; // 去除最高位，得到实际长度
            index++;

            // 根据编码方式进行解码
            var valueBytes = data.Slice(index, length).ToArray();
            index += length;
            return isHuffmanEncoded ? HuffmanDecode(valueBytes) : Encoding.ASCII.GetString(valueBytes);
        }


        private static int ParseInteger(Span<byte> data, ref int i, int w, int firstByte)
        {
            int mask = (1 << w) - 1;
            int length = firstByte & mask;
            if (length < mask)
            {
                return length;
            }
            int shift = 0;
            while (i < data.Length)
            {
                byte currentByte = data[i++];
                length += (currentByte & 0x7F) << shift;
                if ((currentByte & 0x80) == 0)
                {
                    break;
                }
                shift += 7;

            }
            return length;
        }

        private static byte DecodeIndexedField(Span<byte> data, ref int i, List<HeaderField> headers, int index, HpackDynamicTable DynamicTable)
        {
            //Console.WriteLine("Indexed Header Field Representation：" + "index:" + index);
            if (index > 61)
            {
                if (DynamicTable != null && index - 61 <= DynamicTable.GetSize())
                {
                    var entry = DynamicTable.GetEntry(index - 62);

                    headers.Add(new HeaderField(entry.Name, entry.Value, IndexType.Index));
                    //Console.WriteLine(entry.Name + ":" + entry.Value);
                }
                else
                {
                    //Console.WriteLine("index:" + index + "动态表为null或动态表索引超出范围"); 
                    return 0x1;
                }

            }
            else
            {
                var entry = HpackStaticTable.GetEntry(index - 1);
                headers.Add(new HeaderField(entry.Name, entry.Value, IndexType.Index));
                //Console.WriteLine(entry.Name + ":" + entry.Value);
            }

            return 0x0;
        }

        private static void DecodeLiteralWithIncrementalIndexing(Span<byte> data, ref int i, List<HeaderField> headers, HpackDynamicTable DynamicTable, int index)
        {
            //Console.Write("Literal Header Field with Incremental Indexing:");
            string name = "";
            string value = DecodeNameValuePair(data, ref i, ref name, index, DynamicTable);
            headers.Add(new HeaderField(name, value, IndexType.IndexedName));
            DynamicTable.AddEntry(name, value);
            //Console.WriteLine(name + ":" + value);
        }

        private static void DecodeLiteralWithoutIndexing(Span<byte> data, ref int i, List<HeaderField> headers, HpackDynamicTable DynamicTable, int index)
        {
            //Console.Write("Literal Header Field without Indexing:");
            string name = "";
            string value = DecodeNameValuePair(data, ref i, ref name, index, DynamicTable);
            headers.Add(new HeaderField(name, value, IndexType.NoIndexing));
            //Console.WriteLine(name + ":" + value);
        }

        private static void DecodeLiteralNeverIndexed(Span<byte> data, ref int i, List<HeaderField> headers, HpackDynamicTable DynamicTable, int index)
        {
            //Console.Write("Literal Header Field Never Indexed:");
            string name = "";
            string value = DecodeNameValuePair(data, ref i, ref name, index, DynamicTable);
            headers.Add(new HeaderField(name, value, IndexType.NeverIndexed));
            //Console.WriteLine(name + ":" + value);
        }

        private static string DecodeNameValuePair(Span<byte> data, ref int i, ref string name, int index, HpackDynamicTable DynamicTable)
        {
            if (index == 0)
            {
                //Console.WriteLine("– New Name");
                name = DecodeString(data, ref i);
            }
            else
            {
                //Console.WriteLine("– Indexed");
                name = index <= 61 ? HpackStaticTable.GetEntry(index - 1).Name : DynamicTable.GetEntry(index - 62).Name;
            }
            return DecodeString(data, ref i);
        }

        private static string DecodeString(Span<byte> data, ref int i)
        {
            int length = data[i++];
            bool isHuffmanEncoded = (length & 0x80) == 0x80;
            length &= 0x7F;
            if (length == 127)
            {
                int shift = 0;
                while (true)
                {
                    byte nextByte = data[i++];
                    length += (nextByte & 0x7F) << shift;
                    if ((nextByte & 0x80) == 0) break;
                    shift += 7;
                }
            }
            if (i + length > data.Length) throw new ArgumentOutOfRangeException("长度超出数据范围");
            var strData = data.Slice(i, length);
            i += length;

            return isHuffmanEncoded ? HuffmanDecode(strData) : Encoding.UTF8.GetString(strData);
        }

    }
}
