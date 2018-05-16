// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArkSettings.cs" company="Ark">
//   MIT License
//   // 
//   // Copyright (c) 2017 Kristjan Košič
//   // 
//   // Permission is hereby granted, free of charge, to any person obtaining a copy
//   // of this software and associated documentation files (the "Software"), to deal
//   // in the Software without restriction, including without limitation the rights
//   // to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   // copies of the Software, and to permit persons to whom the Software is
//   // furnished to do so, subject to the following conditions:
//   // 
//   // The above copyright notice and this permission notice shall be included in all
//   // copies or substantial portions of the Software.
//   // 
//   // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//   // IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//   // FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//   // AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//   // LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//   // OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//   // SOFTWARE.
// </copyright>

// Thanks to Ray Booysen 
// https://stackoverflow.com/questions/479410/enum-tostring-with-user-friendly-strings

using System;
using System.ComponentModel;

namespace ArkNet.Utils.Enum
{
    public static class EnumExtension
    {

        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", nameof(enumerationValue));
            }

            //Tries to find a DescriptionAttribute for a potential friendly name for the enum
            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length <= 0) return enumerationValue.ToString();
            var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            // Pull out the description value
            return attrs.Length > 0 ? ((DescriptionAttribute) attrs[0]).Description : enumerationValue.ToString();

            //If we have no description attribute, just return the ToString of the enum
        }

        // Network Type Labels
        public static string MainNetLabel => NetworkType.MainNet.GetDescription();
        public static string DevNetLabel => NetworkType.DevNet.GetDescription();

        // Transaction Type Labels
        public static string SendArkLabel => TransactionType.SendArk.GetDescription();
        public static string VoteDelegateLabel => TransactionType.VoteDelegate.GetDescription();
        public static string CreateDelegateLabel => TransactionType.CreateDelegate.GetDescription();
        public static string MultisignatureLabel => TransactionType.MultiSignature.GetDescription();
    }
}