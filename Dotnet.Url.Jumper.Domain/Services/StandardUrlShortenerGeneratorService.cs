﻿/*
 * ShortURL (https://github.com/delight-im/ShortURL)
 * Copyright (c) delight.im (https://www.delight.im/)
 * Licensed under the MIT License (https://opensource.org/licenses/MIT)
 */
using Dotnet.Url.Jumper.Domain.Exceptions;
using System.Linq;
using System.Text;

namespace Dotnet.Url.Jumper.Domain.Services
{
    /**
     * ShortURL: Bijective conversion between natural numbers (IDs) and short strings
     *
     * ShortURL.Encode() takes an ID and turns it into a short string
     * ShortURL.Decode() takes a short string and turns it into an ID
     *
     * Features:
     * + large alphabet (51 chars) and thus very short resulting strings
     * + proof against offensive words (removed 'a', 'e', 'i', 'o' and 'u')
     * + unambiguous (removed 'I', 'l', '1', 'O' and '0')
     *
     * Example output:
     * 123456789 <=> pgK8p
     */

    public class StandardUrlShortenerService : IUrlShortenerGeneratorService
    {       
        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private static readonly int Base = Alphabet.Length;

        public StandardUrlShortenerService()
        {
        }
        
        public string Encode(int num)
        {
            var sb = new StringBuilder();
            while (num > 0)
            {
                sb.Insert(0, Alphabet.ElementAt(num % Base));
                num = num / Base;
            }
            return sb.ToString();
        }

        public int Decode(string str)
        {
            var num = 0;
            for (var i = 0; i < str.Length; i++)
            {
                num = num * Base + Alphabet.IndexOf(str.ElementAt(i));
            }
            return num;
        }
    }
}
