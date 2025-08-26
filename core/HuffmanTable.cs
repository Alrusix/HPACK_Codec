using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HPACK_Codec.core
{
    public class SymbolCode
    {
        public string BitSequence { get; set; } // 比特序列
        public int BitLength { get; set; }      // 位长度

        public SymbolCode(string bitSequence, int bitLength)
        {
            BitSequence = bitSequence;
            BitLength = bitLength;
        }
    }
    public static class HuffmanEncodeTable
    {
        public static readonly Dictionary<char, SymbolCode> Table = new Dictionary<char, SymbolCode>
        {
            { ' ', new SymbolCode("010100", 6) },
            { '!', new SymbolCode("1111111000", 10) },
            { '"', new SymbolCode("1111111001", 10) },
            { '#', new SymbolCode("111111111010", 12) },
            { '$', new SymbolCode("1111111111001", 13) },
            { '%', new SymbolCode("010101", 6) },
            { '&', new SymbolCode("11111000", 8) },
            { '\'', new SymbolCode("11111111010", 11) },
            { '(', new SymbolCode("1111111010", 10) },
            { ')', new SymbolCode("1111111011", 10) },
            { '*', new SymbolCode("11111001", 8) },
            { '+', new SymbolCode("11111111011", 11) },
            { ',', new SymbolCode("11111010", 8) },
            { '-', new SymbolCode("010110", 6) },
            { '.', new SymbolCode("010111", 6) },
            { '/', new SymbolCode("011000", 6) },
            { '0', new SymbolCode("00000", 5) },
            { '1', new SymbolCode("00001", 5) },
            { '2', new SymbolCode("00010", 5) },
            { '3', new SymbolCode("011001", 6) },
            { '4', new SymbolCode("011010", 6) },
            { '5', new SymbolCode("011011", 6) },
            { '6', new SymbolCode("011100", 6) },
            { '7', new SymbolCode("011101", 6) },
            { '8', new SymbolCode("011110", 6) },
            { '9', new SymbolCode("011111", 6) },
            { ':', new SymbolCode("1011100", 7) },
            { ';', new SymbolCode("11111011", 8) },
            { '<', new SymbolCode("111111111111100", 15) },
            { '=', new SymbolCode("100000", 6) },
            { '>', new SymbolCode("111111111011", 12) },
            { '?', new SymbolCode("1111111100", 10) },
            { '@', new SymbolCode("11111111111010", 13) },
            { 'A', new SymbolCode("100001", 6) },
            { 'B', new SymbolCode("1011101", 7) },
            { 'C', new SymbolCode("1011110", 7) },
            { 'D', new SymbolCode("1011111", 7) },
            { 'E', new SymbolCode("1100000", 7) },
            { 'F', new SymbolCode("1100001", 7) },
            { 'G', new SymbolCode("1100010", 7) },
            { 'H', new SymbolCode("1100011", 7) },
            { 'I', new SymbolCode("1100100", 7) },
            { 'J', new SymbolCode("1100101", 7) },
            { 'K', new SymbolCode("1100110", 7) },
            { 'L', new SymbolCode("1100111", 7) },
            { 'M', new SymbolCode("1101000", 7) },
            { 'N', new SymbolCode("1101001", 7) },
            { 'O', new SymbolCode("1101010", 7) },
            { 'P', new SymbolCode("1101011", 7) },
            { 'Q', new SymbolCode("1101100", 7) },
            { 'R', new SymbolCode("1101101", 7) },
            { 'S', new SymbolCode("1101110", 7) },
            { 'T', new SymbolCode("1101111", 7) },
            { 'U', new SymbolCode("1110000", 7) },
            { 'V', new SymbolCode("1110001", 7) },
            { 'W', new SymbolCode("1110010", 7) },
            { 'X', new SymbolCode("11111100", 8) },
            { 'Y', new SymbolCode("1110011", 7) },
            { 'Z', new SymbolCode("11111101", 8) },
            { '[', new SymbolCode("11111111111011", 13) },
            { '\\', new SymbolCode("1111111111111110000", 19) },
            { ']', new SymbolCode("11111111111100", 13) },
            { '^', new SymbolCode("111111111111100", 14) },
            { '_', new SymbolCode("100010", 6) },
            { '`', new SymbolCode("1111111111111101", 15) },
            { 'a', new SymbolCode("00011", 5) },
            { 'b', new SymbolCode("100011", 6) },
            { 'c', new SymbolCode("00100", 5) },
            { 'd', new SymbolCode("100100", 6) },
            { 'e', new SymbolCode("00101", 5) },
            { 'f', new SymbolCode("100101", 6) },
            { 'g', new SymbolCode("100110", 6) },
            { 'h', new SymbolCode("100111", 6) },
            { 'i', new SymbolCode("00110", 5) },
            { 'j', new SymbolCode("1110100", 7) },
            { 'k', new SymbolCode("1110101", 7) },
            { 'l', new SymbolCode("101000", 6) },
            { 'm', new SymbolCode("101001", 6) },
            { 'n', new SymbolCode("101010", 6) },
            { 'o', new SymbolCode("00111", 5) },
            { 'p', new SymbolCode("101011", 6) },
            { 'q', new SymbolCode("1110110", 7) },
            { 'r', new SymbolCode("101100", 6) },
            { 's', new SymbolCode("01000", 5) },
            { 't', new SymbolCode("01001", 5) },
            { 'u', new SymbolCode("101101", 6) },
            { 'v', new SymbolCode("1110111", 7) },
            { 'w', new SymbolCode("1111000", 7) },
            { 'x', new SymbolCode("1111001", 7) },
            { 'y', new SymbolCode("1111010", 7) },
            { 'z', new SymbolCode("1111011", 7) },
            { '{', new SymbolCode("1111111111111110", 15) },
            { '|', new SymbolCode("111111111100", 11) },
            { '}', new SymbolCode("111111111111101", 14) },
            { '~', new SymbolCode("11111111111101", 13) }
        };
    }
    public static class HuffmanDecodeTable
    {
        /// <summary>
        /// An entry in the huffman table
        /// </summary>
        public struct Entry
        {
            public int Bin;
            public int Len;
        }

        /// <summary>
        /// Entries in the huffman table
        /// </summary>
        public static readonly Entry[] Entries =
        {
            new Entry { Bin = 0x1ff8, Len = 13},
            new Entry { Bin = 0x7fffd8, Len = 23 },
            new Entry { Bin = 0xfffffe2, Len = 28 },
            new Entry { Bin = 0xfffffe3, Len = 28 },
            new Entry { Bin = 0xfffffe4, Len = 28 },
            new Entry { Bin = 0xfffffe5, Len = 28 },
            new Entry { Bin = 0xfffffe6, Len = 28 },
            new Entry { Bin = 0xfffffe7, Len = 28 },
            new Entry { Bin = 0xfffffe8, Len = 28 },
            new Entry { Bin = 0xffffea, Len = 24 },
            new Entry { Bin = 0x3ffffffc, Len = 30 },
            new Entry { Bin = 0xfffffe9, Len = 28 },
            new Entry { Bin = 0xfffffea, Len = 28 },
            new Entry { Bin = 0x3ffffffd, Len = 30 },
            new Entry { Bin = 0xfffffeb, Len = 28 },
            new Entry { Bin = 0xfffffec, Len = 28 },
            new Entry { Bin = 0xfffffed, Len = 28 },
            new Entry { Bin = 0xfffffee, Len = 28 },
            new Entry { Bin = 0xfffffef, Len = 28 },
            new Entry { Bin = 0xffffff0, Len = 28 },
            new Entry { Bin = 0xffffff1, Len = 28 },
            new Entry { Bin = 0xffffff2, Len = 28 },
            new Entry { Bin = 0x3ffffffe, Len = 30 },
            new Entry { Bin = 0xffffff3, Len = 28 },
            new Entry { Bin = 0xffffff4, Len = 28 },
            new Entry { Bin = 0xffffff5, Len = 28 },
            new Entry { Bin = 0xffffff6, Len = 28 },
            new Entry { Bin = 0xffffff7, Len = 28 },
            new Entry { Bin = 0xffffff8, Len = 28 },
            new Entry { Bin = 0xffffff9, Len = 28 },
            new Entry { Bin = 0xffffffa, Len = 28 },
            new Entry { Bin = 0xffffffb, Len = 28 },
            new Entry { Bin = 0x14, Len =  6 },
            new Entry { Bin = 0x3f8, Len = 10 },
            new Entry { Bin = 0x3f9, Len = 10 },
            new Entry { Bin = 0xffa, Len = 12 },
            new Entry { Bin = 0x1ff9, Len = 13 },
            new Entry { Bin = 0x15, Len =  6 },
            new Entry { Bin = 0xf8, Len =  8 },
            new Entry { Bin = 0x7fa, Len = 11 },
            new Entry { Bin = 0x3fa, Len = 10 },
            new Entry { Bin = 0x3fb, Len = 10 },
            new Entry { Bin = 0xf9, Len =  8 },
            new Entry { Bin = 0x7fb, Len = 11 },
            new Entry { Bin = 0xfa, Len =  8 },
            new Entry { Bin = 0x16, Len =  6 },
            new Entry { Bin = 0x17, Len =  6 },
            new Entry { Bin = 0x18, Len =  6 },
            new Entry { Bin = 0x0, Len =  5 },
            new Entry { Bin = 0x1, Len =  5 },
            new Entry { Bin = 0x2, Len =  5 },
            new Entry { Bin = 0x19, Len =  6 },
            new Entry { Bin = 0x1a, Len =  6 },
            new Entry { Bin = 0x1b, Len =  6 },
            new Entry { Bin = 0x1c, Len =  6 },
            new Entry { Bin = 0x1d, Len =  6 },
            new Entry { Bin = 0x1e, Len =  6 },
            new Entry { Bin = 0x1f, Len =  6 },
            new Entry { Bin = 0x5c, Len =  7 },
            new Entry { Bin = 0xfb, Len =  8 },
            new Entry { Bin = 0x7ffc, Len = 15 },
            new Entry { Bin = 0x20, Len =  6 },
            new Entry { Bin = 0xffb, Len = 12 },
            new Entry { Bin = 0x3fc, Len = 10 },
            new Entry { Bin = 0x1ffa, Len = 13 },
            new Entry { Bin = 0x21, Len =  6 },
            new Entry { Bin = 0x5d, Len =  7 },
            new Entry { Bin = 0x5e, Len =  7 },
            new Entry { Bin = 0x5f, Len =  7 },
            new Entry { Bin = 0x60, Len =  7 },
            new Entry { Bin = 0x61, Len =  7 },
            new Entry { Bin = 0x62, Len =  7 },
            new Entry { Bin = 0x63, Len =  7 },
            new Entry { Bin = 0x64, Len =  7 },
            new Entry { Bin = 0x65, Len =  7 },
            new Entry { Bin = 0x66, Len =  7 },
            new Entry { Bin = 0x67, Len =  7 },
            new Entry { Bin = 0x68, Len =  7 },
            new Entry { Bin = 0x69, Len =  7 },
            new Entry { Bin = 0x6a, Len =  7 },
            new Entry { Bin = 0x6b, Len =  7 },
            new Entry { Bin = 0x6c, Len =  7 },
            new Entry { Bin = 0x6d, Len =  7 },
            new Entry { Bin = 0x6e, Len =  7 },
            new Entry { Bin = 0x6f, Len =  7 },
            new Entry { Bin = 0x70, Len =  7 },
            new Entry { Bin = 0x71, Len =  7 },
            new Entry { Bin = 0x72, Len =  7 },
            new Entry { Bin = 0xfc, Len =  8 },
            new Entry { Bin = 0x73, Len =  7 },
            new Entry { Bin = 0xfd, Len =  8 },
            new Entry { Bin = 0x1ffb, Len = 13 },
            new Entry { Bin = 0x7fff0, Len = 19 },
            new Entry { Bin = 0x1ffc, Len = 13 },
            new Entry { Bin = 0x3ffc, Len = 14 },
            new Entry { Bin = 0x22, Len =  6 },
            new Entry { Bin = 0x7ffd, Len = 15 },
            new Entry { Bin = 0x3, Len =  5 },
            new Entry { Bin = 0x23, Len =  6 },
            new Entry { Bin = 0x4, Len =  5 },
            new Entry { Bin = 0x24, Len =  6 },
            new Entry { Bin = 0x5, Len =  5 },
            new Entry { Bin = 0x25, Len =  6 },
            new Entry { Bin = 0x26, Len =  6 },
            new Entry { Bin = 0x27, Len =  6 },
            new Entry { Bin = 0x6, Len =  5 },
            new Entry { Bin = 0x74, Len =  7 },
            new Entry { Bin = 0x75, Len =  7 },
            new Entry { Bin = 0x28, Len =  6 },
            new Entry { Bin = 0x29, Len =  6 },
            new Entry { Bin = 0x2a, Len =  6 },
            new Entry { Bin = 0x7, Len =  5 },
            new Entry { Bin = 0x2b, Len =  6 },
            new Entry { Bin = 0x76, Len =  7 },
            new Entry { Bin = 0x2c, Len =  6 },
            new Entry { Bin = 0x8, Len =  5 },
            new Entry { Bin = 0x9, Len =  5 },
            new Entry { Bin = 0x2d, Len =  6 },
            new Entry { Bin = 0x77, Len =  7 },
            new Entry { Bin = 0x78, Len =  7 },
            new Entry { Bin = 0x79, Len =  7 },
            new Entry { Bin = 0x7a, Len =  7 },
            new Entry { Bin = 0x7b, Len =  7 },
            new Entry { Bin = 0x7ffe, Len = 15 },
            new Entry { Bin = 0x7fc, Len = 11 },
            new Entry { Bin = 0x3ffd, Len = 14 },
            new Entry { Bin = 0x1ffd, Len = 13 },
            new Entry { Bin = 0xffffffc, Len = 28 },
            new Entry { Bin = 0xfffe6, Len = 20 },
            new Entry { Bin = 0x3fffd2, Len = 22 },
            new Entry { Bin = 0xfffe7, Len = 20 },
            new Entry { Bin = 0xfffe8, Len = 20 },
            new Entry { Bin = 0x3fffd3, Len = 22 },
            new Entry { Bin = 0x3fffd4, Len = 22 },
            new Entry { Bin = 0x3fffd5, Len = 22 },
            new Entry { Bin = 0x7fffd9, Len = 23 },
            new Entry { Bin = 0x3fffd6, Len = 22 },
            new Entry { Bin = 0x7fffda, Len = 23 },
            new Entry { Bin = 0x7fffdb, Len = 23 },
            new Entry { Bin = 0x7fffdc, Len = 23 },
            new Entry { Bin = 0x7fffdd, Len = 23 },
            new Entry { Bin = 0x7fffde, Len = 23 },
            new Entry { Bin = 0xffffeb, Len = 24 },
            new Entry { Bin = 0x7fffdf, Len = 23 },
            new Entry { Bin = 0xffffec, Len = 24 },
            new Entry { Bin = 0xffffed, Len = 24 },
            new Entry { Bin = 0x3fffd7, Len = 22 },
            new Entry { Bin = 0x7fffe0, Len = 23 },
            new Entry { Bin = 0xffffee, Len = 24 },
            new Entry { Bin = 0x7fffe1, Len = 23 },
            new Entry { Bin = 0x7fffe2, Len = 23 },
            new Entry { Bin = 0x7fffe3, Len = 23 },
            new Entry { Bin = 0x7fffe4, Len = 23 },
            new Entry { Bin = 0x1fffdc, Len = 21 },
            new Entry { Bin = 0x3fffd8, Len = 22 },
            new Entry { Bin = 0x7fffe5, Len = 23 },
            new Entry { Bin = 0x3fffd9, Len = 22 },
            new Entry { Bin = 0x7fffe6, Len = 23 },
            new Entry { Bin = 0x7fffe7, Len = 23 },
            new Entry { Bin = 0xffffef, Len = 24 },
            new Entry { Bin = 0x3fffda, Len = 22 },
            new Entry { Bin = 0x1fffdd, Len = 21 },
            new Entry { Bin = 0xfffe9, Len = 20 },
            new Entry { Bin = 0x3fffdb, Len = 22 },
            new Entry { Bin = 0x3fffdc, Len = 22 },
            new Entry { Bin = 0x7fffe8, Len = 23 },
            new Entry { Bin = 0x7fffe9, Len = 23 },
            new Entry { Bin = 0x1fffde, Len = 21 },
            new Entry { Bin = 0x7fffea, Len = 23 },
            new Entry { Bin = 0x3fffdd, Len = 22 },
            new Entry { Bin = 0x3fffde, Len = 22 },
            new Entry { Bin = 0xfffff0, Len = 24 },
            new Entry { Bin = 0x1fffdf, Len = 21 },
            new Entry { Bin = 0x3fffdf, Len = 22 },
            new Entry { Bin = 0x7fffeb, Len = 23 },
            new Entry { Bin = 0x7fffec, Len = 23 },
            new Entry { Bin = 0x1fffe0, Len = 21 },
            new Entry { Bin = 0x1fffe1, Len = 21 },
            new Entry { Bin = 0x3fffe0, Len = 22 },
            new Entry { Bin = 0x1fffe2, Len = 21 },
            new Entry { Bin = 0x7fffed, Len = 23 },
            new Entry { Bin = 0x3fffe1, Len = 22 },
            new Entry { Bin = 0x7fffee, Len = 23 },
            new Entry { Bin = 0x7fffef, Len = 23 },
            new Entry { Bin = 0xfffea, Len = 20 },
            new Entry { Bin = 0x3fffe2, Len = 22 },
            new Entry { Bin = 0x3fffe3, Len = 22 },
            new Entry { Bin = 0x3fffe4, Len = 22 },
            new Entry { Bin = 0x7ffff0, Len = 23 },
            new Entry { Bin = 0x3fffe5, Len = 22 },
            new Entry { Bin = 0x3fffe6, Len = 22 },
            new Entry { Bin = 0x7ffff1, Len = 23 },
            new Entry { Bin = 0x3ffffe0, Len = 26 },
            new Entry { Bin = 0x3ffffe1, Len = 26 },
            new Entry { Bin = 0xfffeb, Len = 20 },
            new Entry { Bin = 0x7fff1, Len = 19 },
            new Entry { Bin = 0x3fffe7, Len = 22 },
            new Entry { Bin = 0x7ffff2, Len = 23 },
            new Entry { Bin = 0x3fffe8, Len = 22 },
            new Entry { Bin = 0x1ffffec, Len = 25 },
            new Entry { Bin = 0x3ffffe2, Len = 26 },
            new Entry { Bin = 0x3ffffe3, Len = 26 },
            new Entry { Bin = 0x3ffffe4, Len = 26 },
            new Entry { Bin = 0x7ffffde, Len = 27 },
            new Entry { Bin = 0x7ffffdf, Len = 27 },
            new Entry { Bin = 0x3ffffe5, Len = 26 },
            new Entry { Bin = 0xfffff1, Len = 24 },
            new Entry { Bin = 0x1ffffed, Len = 25 },
            new Entry { Bin = 0x7fff2, Len = 19 },
            new Entry { Bin = 0x1fffe3, Len = 21 },
            new Entry { Bin = 0x3ffffe6, Len = 26 },
            new Entry { Bin = 0x7ffffe0, Len = 27 },
            new Entry { Bin = 0x7ffffe1, Len = 27 },
            new Entry { Bin = 0x3ffffe7, Len = 26 },
            new Entry { Bin = 0x7ffffe2, Len = 27 },
            new Entry { Bin = 0xfffff2, Len = 24 },
            new Entry { Bin = 0x1fffe4, Len = 21 },
            new Entry { Bin = 0x1fffe5, Len = 21 },
            new Entry { Bin = 0x3ffffe8, Len = 26 },
            new Entry { Bin = 0x3ffffe9, Len = 26 },
            new Entry { Bin = 0xffffffd, Len = 28 },
            new Entry { Bin = 0x7ffffe3, Len = 27 },
            new Entry { Bin = 0x7ffffe4, Len = 27 },
            new Entry { Bin = 0x7ffffe5, Len = 27 },
            new Entry { Bin = 0xfffec, Len = 20 },
            new Entry { Bin = 0xfffff3, Len = 24 },
            new Entry { Bin = 0xfffed, Len = 20 },
            new Entry { Bin = 0x1fffe6, Len = 21 },
            new Entry { Bin = 0x3fffe9, Len = 22 },
            new Entry { Bin = 0x1fffe7, Len = 21 },
            new Entry { Bin = 0x1fffe8, Len = 21 },
            new Entry { Bin = 0x7ffff3, Len = 23 },
            new Entry { Bin = 0x3fffea, Len = 22 },
            new Entry { Bin = 0x3fffeb, Len = 22 },
            new Entry { Bin = 0x1ffffee, Len = 25 },
            new Entry { Bin = 0x1ffffef, Len = 25 },
            new Entry { Bin = 0xfffff4, Len = 24 },
            new Entry { Bin = 0xfffff5, Len = 24 },
            new Entry { Bin = 0x3ffffea, Len = 26 },
            new Entry { Bin = 0x7ffff4, Len = 23 },
            new Entry { Bin = 0x3ffffeb, Len = 26 },
            new Entry { Bin = 0x7ffffe6, Len = 27 },
            new Entry { Bin = 0x3ffffec, Len = 26 },
            new Entry { Bin = 0x3ffffed, Len = 26 },
            new Entry { Bin = 0x7ffffe7, Len = 27 },
            new Entry { Bin = 0x7ffffe8, Len = 27 },
            new Entry { Bin = 0x7ffffe9, Len = 27 },
            new Entry { Bin = 0x7ffffea, Len = 27 },
            new Entry { Bin = 0x7ffffeb, Len = 27 },
            new Entry { Bin = 0xffffffe, Len = 28 },
            new Entry { Bin = 0x7ffffec, Len = 27 },
            new Entry { Bin = 0x7ffffed, Len = 27 },
            new Entry { Bin = 0x7ffffee, Len = 27 },
            new Entry { Bin = 0x7ffffef, Len = 27 },
            new Entry { Bin = 0x7fffff0, Len = 27 },
            new() { Bin = 0x3ffffee, Len = 26 },
            new() { Bin = 0x3fffffff, Len = 30 },
        };
    }

    static class HuffmanTree
    {
        /// <summary>
        /// A node in the binary huffman tree
        /// </summary>
        public class TreeNode
        {
            /// <summary>Tree branch that is taken when decoder encounters 0</summary>
            public TreeNode Child0;
            /// <summary>Tree branch that is taken when decoder encounters 1</summary>
            public TreeNode Child1;
            /// <summary>
            /// The value of the node. This is only set in leaf nodes.
            /// Might also be ushort, since the max value is 256.
            /// However currently -1 depicts that no value is set for the node.
            /// </summary>
            public int Value;
        }

        /// <summary>The root of the tree</summary>
        public static readonly TreeNode Root;

        static HuffmanTree()
        {
            // Create a tree structure out of the huffman table
            Root = new TreeNode
            {
                Child0 = null,
                Child1 = null,
                Value = -1,
            };

            var i = 0;
            foreach (var entry in HuffmanDecodeTable.Entries)
            {
                InsertIntoTree(Root, i, entry.Bin, entry.Len);
                i++;
            }
        }

        private static void InsertIntoTree(TreeNode tree, int value, int bin, int len)
        {
            // Check the leftmost bit of the tree
            var firstBit = bin >> len - 1;
            var child = firstBit == 1 ? tree.Child1 : tree.Child0;
            if (len == 1)
            {
                // Leaf node
                if (child != null) throw new Exception("TreeNode alreay occupied");
                child = new TreeNode { Child0 = null, Child1 = null, Value = value };
                if (firstBit == 1) tree.Child1 = child;
                else tree.Child0 = child;
            }
            else
            {
                // Value must be inserted deeper within the tree
                // Reset the first bit
                var rem = bin & (1 << len - 1) - 1;
                if (child == null)
                {
                    // Create a new branch if necessary
                    child = new TreeNode { Child0 = null, Child1 = null, Value = -1 };
                    if (firstBit == 1) tree.Child1 = child;
                    else tree.Child0 = child;
                }

                InsertIntoTree(child, value, rem, len - 1);
            }
        }
    }

}

