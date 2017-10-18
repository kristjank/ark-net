﻿using System;
using System.Globalization;

namespace ArkNet.Utils
{
    /*
	 * Licensed to the Apache Software Foundation (ASF) under one or more
	 * contributor license agreements.  See the NOTICE file distributed with
	 * this work for additional information regarding copyright ownership.
	 * The ASF licenses this file to You under the Apache License, Version 2.0
	 * (the "License"); you may not use this file except in compliance with
	 * the License.  You may obtain a copy of the License at
	 *
	 *     http://www.apache.org/licenses/LICENSE-2.0
	 *
	 * Unless required by applicable law or agreed to in writing, software
	 * distributed under the License is distributed on an "AS IS" BASIS,
	 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	 * See the License for the specific language governing permissions and
	 * limitations under the License.
	 */

    public class NumberFormat
    {
        protected readonly CultureInfo locale;

        //private int maximumIntegerDigits;
        //private int minimumIntegerDigits;
        //private int maximumFractionDigits;
        //private int minimumFractionDigits;

        public NumberFormat(CultureInfo locale)
        {
            this.locale = locale;
        }

        public virtual string Format(object number)
        {
            string format = GetNumberFormat();

            if (number is int)
            {
                return ((int)number).ToString(format, locale);
            }
            else if (number is long)
            {
                return ((long)number).ToString(format, locale);
            }
            else if (number is short)
            {
                return ((short)number).ToString(format, locale);
            }
            else if (number is float)
            {
                return ((float)number).ToString(format, locale);
            }
            else if (number is double)
            {
                return ((double)number).ToString(format, locale);
            }
            else if (number is decimal)
            {
                return ((decimal)number).ToString(format, locale);
            }

            throw new ArgumentException("Cannot format given object as a Number");
        }

        public virtual string Format(double number)
        {
            string format = GetNumberFormat();
            return number.ToString(format, locale);
        }

        public virtual string Format(long number)
        {
            string format = GetNumberFormat();
            return number.ToString(format, locale);
        }

        protected virtual string GetNumberFormat()
        {
            return null;
        }

        public virtual /*Number*/ object Parse(string source)
        {
            return decimal.Parse(source, locale);
        }

        public override string ToString()
        {
            return base.ToString() + " - " + GetNumberFormat() + " - " + locale.ToString();
        }

        // LUCENENET TODO: Add additional functionality to edit the NumberFormatInfo
        // properties, which provides somewhat similar functionality to the below Java
        // getters and setters.

        //public virtual int MaximumIntegerDigits
        //{
        //    get { return this.maximumIntegerDigits; }
        //}

        //public virtual void SetMaximumIntegerDigits(int newValue)
        //{
        //    this.maximumIntegerDigits = Math.Max(0, newValue);
        //    if (maximumIntegerDigits < minimumIntegerDigits)
        //    {
        //        minimumIntegerDigits = maximumIntegerDigits;
        //    }
        //}

        //public virtual int MinimumIntegerDigits
        //{
        //    get { return this.minimumIntegerDigits; }
        //}

        //public virtual void SetMinimumIntegerDigits(int newValue)
        //{
        //    this.minimumIntegerDigits = Math.Max(0, newValue);
        //    if (minimumIntegerDigits > maximumIntegerDigits)
        //    {
        //        maximumIntegerDigits = minimumIntegerDigits;
        //    }
        //}

        //public virtual int MaximumFractionDigits
        //{
        //    get { return this.maximumFractionDigits; }
        //}

        //public virtual void SetMaximumFractionDigits(int newValue)
        //{
        //    maximumFractionDigits = Math.Max(0, newValue);
        //    if (maximumFractionDigits < minimumFractionDigits)
        //    {
        //        minimumFractionDigits = maximumFractionDigits;
        //    }
        //}

        //public virtual int MinimumFractionDigits
        //{
        //    get { return this.minimumFractionDigits; }
        //}

        //public void SetMinimumFractionDigits(int newValue)
        //{
        //    minimumFractionDigits = Math.Max(0, newValue);
        //    if (maximumFractionDigits < minimumFractionDigits)
        //    {
        //        maximumFractionDigits = minimumFractionDigits;
        //    }
        //}
    }
}
