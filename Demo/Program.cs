﻿using System;
using LF2BitConverter;
using LF2BitConverter.ConvertMemberAttributeNS;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var mock = new Mock
            {
                A = new Foo
                {
                    A = 1,
                    B = 2
                },
                B = 3,
                C = new[]
                {
                    new Foo
                    {
                        A=4,
                        B=5
                    },
                    new Foo
                    {
                        A=6,
                        B=7
                    }
                },
                D = new[] { 7, 8, 9, 10 },
                E = new[] { Bar.A, Bar.B }
            };
            var byets = BitConverterEX.LittleEndian.GetBytes(mock);
            var index = 0;
            var obj = BitConverterEX.LittleEndian.ToObject<Mock>(byets, ref index);
        }
    }

    class Mock
    {
        [Ignore]
        public Foo A;
        public Int32 B;
        [ConvertArray(CountBy.Item, LengthFrom = nameof(B))]
        public Foo[] C;
        [ConvertArray(CountBy.Item, Length = 4)]
        public Int32[] D;
        [ConvertAs(typeof(Byte))]
        [ConvertArray(CountBy.Byte, Length = 2)]
        public Bar[] E;
    }

    class Foo
    {
        public Int32 A;
        public Int32 B;
    }

    enum Bar
    {
        A, B
    }
}
