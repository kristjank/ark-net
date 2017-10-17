﻿/*
 * Copyright (c) 1999, 2008, Oracle and/or its affiliates. All rights reserved.
 * DO NOT ALTER OR REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 *
 * This code is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License version 2 only, as
 * published by the Free Software Foundation.  Oracle designates this
 * particular file as subject to the "Classpath" exception as provided
 * by Oracle in the LICENSE file that accompanied this code.
 *
 * This code is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 * FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
 * version 2 for more details (a copy is included in the LICENSE file that
 * accompanied this code).
 *
 * You should have received a copy of the GNU General Public License version
 * 2 along with this work; if not, write to the Free Software Foundation,
 * Inc., 51 Franklin St, Fifth Floor, Boston, MA 02110-1301 USA.
 *
 * Please contact Oracle, 500 Oracle Parkway, Redwood Shores, CA 94065 USA
 * or visit www.oracle.com if you need additional information or have any
 * questions.
 */

using System;
using System.Text;

namespace ArkNet.Utils
{
    /// <summary>
    /// Ported from Java's nio.LongBuffer
    /// </summary>
#if FEATURE_SERIALIZABLE
    [Serializable]
#endif
    public abstract class Int64Buffer : Buffer, IComparable<Int64Buffer>
    {
        // These fields are declared here rather than in Heap-X-Buffer in order to
        // reduce the number of virtual method invocations needed to access these
        // values, which is especially costly when coding small buffers.
        //
        internal readonly long[] hb;                  // Non-null only for heap buffers
        internal readonly int offset;
#pragma warning disable CS0649
        internal bool isReadOnly;                 // Valid only for heap buffers

        /// <summary>
        /// Creates a new buffer with the given mark, position, limit, capacity, backing array, and array offset
        /// </summary>
        public Int64Buffer(int mark, int pos, int lim, int cap,
            long[] hb, int offset) 
            : base(mark, pos, lim, cap)
        {
            this.hb = hb;
            this.offset = offset;
        }

        /// <summary>
        /// Creates a new buffer with the given mark, position, limit, and capacity
        /// </summary>
        public Int64Buffer(int mark, int pos, int lim, int cap)
            : this(mark, pos, lim, cap, null, 0)
        {
        }

        public static Int64Buffer Allocate(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException();
            return new HeapInt64Buffer(capacity, capacity);
        }


        public static Int64Buffer Wrap(long[] array,
                                    int offset, int length)
        {
            try
            {
                return new HeapInt64Buffer(array, offset, length);
            }
#pragma warning disable 168
            catch (ArgumentException x)
#pragma warning restore 168
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public static Int64Buffer Wrap(long[] array)
        {
            return Wrap(array, 0, array.Length);
        }


        public abstract Int64Buffer Slice();

        public abstract Int64Buffer Duplicate();

        public abstract Int64Buffer AsReadOnlyBuffer();

        public abstract long Get();

        public abstract Int64Buffer Put(long l);

        public abstract long Get(int index);

        public abstract Int64Buffer Put(int index, long l);

        // -- Bulk get operations --

        public virtual Int64Buffer Get(long[] dst, int offset, int length)
        {
            CheckBounds(offset, length, dst.Length);
            if (length > Remaining)
                throw new BufferUnderflowException();
            int end = offset + length;
            for (int i = offset; i < end; i++)
                dst[i] = Get();
            return this;
        }

        public virtual Int64Buffer Get(long[] dst)
        {
            return Get(dst, 0, dst.Length);
        }

        // -- Bulk put operations --

        public virtual Int64Buffer Put(Int64Buffer src)
        {
            if (src == this)
                throw new ArgumentException();
            if (IsReadOnly)
                throw new ReadOnlyBufferException();
            int n = src.Remaining;
            if (n > Remaining)
                throw new BufferOverflowException();
            for (int i = 0; i < n; i++)
                Put(src.Get());
            return this;
        }

        public virtual Int64Buffer Put(long[] src, int offset, int length)
        {
            CheckBounds(offset, length, src.Length);
            if (length > Remaining)
                throw new BufferOverflowException();
            int end = offset + length;
            for (int i = offset; i < end; i++)
                this.Put(src[i]);
            return this;
        }

        public Int64Buffer Put(long[] src)
        {
            return Put(src, 0, src.Length);
        }

        public override bool HasArray
        {
            get
            {
                return (hb != null) && !isReadOnly;
            }
        }

        public override object Array
        {
            get
            {
                if (hb == null)
                    throw new InvalidOperationException();
                if (isReadOnly)
                    throw new ReadOnlyBufferException();
                return hb;
            }
        }

        public override int ArrayOffset
        {
            get
            {
                if (hb == null)
                    throw new InvalidOperationException();
                if (isReadOnly)
                    throw new ReadOnlyBufferException();
                return offset;
            }
        }

        public abstract Int64Buffer Compact();

        //public override bool IsDirect { get; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetType().Name);
            sb.Append("[pos=");
            sb.Append(Position);
            sb.Append(" lim=");
            sb.Append(Limit);
            sb.Append(" cap=");
            sb.Append(Capacity);
            sb.Append("]");
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            int h = 1;
            int p = Position;
            for (int i = Limit - 1; i >= p; i--)
            {
                h = 31 * h + (int)Get(i);
            }
            return h;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (!(obj is Int64Buffer))
            return false;
            Int64Buffer that = (Int64Buffer)obj;
            if (this.Remaining != that.Remaining)
                return false;
            int p = this.Position;
            for (int i = this.Limit - 1, j = that.Limit - 1; i >= p; i--, j--)
                if (!Equals(this.Get(i), that.Get(j)))
                    return false;
            return true;
        }

        private static bool Equals(long x, long y)
        {
            return x == y;
        }

        public int CompareTo(Int64Buffer other)
        {
            int n = this.Position + Math.Min(this.Remaining, other.Remaining);
            for (int i = this.Position, j = other.Position; i < n; i++, j++)
            {
                int cmp = Compare(this.Get(i), other.Get(j));
                if (cmp != 0)
                    return cmp;
            }
            return this.Remaining - other.Remaining;
        }

        private static int Compare(long x, long y)
        {
            // from Long.compare(x, y)
            return (x < y) ? -1 : ((x == y) ? 0 : 1);
        }

        // -- Other char stuff --

        // (empty)

        // -- Other byte stuff: Access to binary data --

        public abstract ByteOrder Order { get; }


        /// <summary>
        /// NOTE: This was HeapLongBuffer in the JDK
        /// </summary>
#if FEATURE_SERIALIZABLE
        [Serializable]
#endif
        public class HeapInt64Buffer : Int64Buffer
        {
            // For speed these fields are actually declared in X-Buffer;
            // these declarations are here as documentation
            /*

            protected final long[] hb;
            protected final int offset;

            */

            internal HeapInt64Buffer(int cap, int lim)
                : base(-1, 0, lim, cap, new long[cap], 0)
            {
                /*
                hb = new long[cap];
                offset = 0;
                */
            }

            internal HeapInt64Buffer(long[] buf, int off, int len)
                : base(-1, off, off + len, buf.Length, buf, 0)
            {
                /*
                hb = buf;
                offset = 0;
                */
            }

            protected HeapInt64Buffer(long[] buf,
                                   int mark, int pos, int lim, int cap,
                                   int off)
                : base(mark, pos, lim, cap, buf, off)
            {
                /*
                hb = buf;
                offset = off;
                */
            }

            public override Int64Buffer Slice()
            {
                return new HeapInt64Buffer(hb,
                                        -1,
                                        0,
                                        this.Remaining,
                                        this.Remaining,
                                        this.Position + offset);
            }

            public override Int64Buffer Duplicate()
            {
                return new HeapInt64Buffer(hb,
                                        this.MarkValue,
                                        this.Position,
                                        this.Limit,
                                        this.Capacity,
                                        offset);
            }

            public override Int64Buffer AsReadOnlyBuffer()
            {
                throw new NotImplementedException();
                //return new HeapLongBufferR(hb,
                //                     this.MarkValue(),
                //                     this.Position,
                //                     this.Limit,
                //                     this.Capacity,
                //                     offset);
            }

            protected virtual int Ix(int i)
            {
                return i + offset;
            }

            public override long Get()
            {
                return hb[Ix(NextGetIndex())];
            }

            public override long Get(int index)
            {
                return hb[Ix(CheckIndex(index))];
            }

            public override Int64Buffer Get(long[] dst, int offset, int length)
            {
                CheckBounds(offset, length, dst.Length);
                if (length > Remaining)
                    throw new BufferUnderflowException();
                System.Array.Copy(hb, Ix(Position), dst, offset, length);
                SetPosition(Position + length);
                return this;
            }


            public override bool IsDirect
            {
                get
                {
                    return false;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return false;
                }
            }

            public override Int64Buffer Put(long l)
            {
                hb[Ix(NextPutIndex())] = l;
                return this;
            }

            public override Int64Buffer Put(int index, long l)
            {
                hb[Ix(CheckIndex(index))] = l;
                return this;
            }

            public override Int64Buffer Put(long[] src, int offset, int length)
            {

                CheckBounds(offset, length, src.Length);
                if (length > Remaining)
                    throw new BufferOverflowException();
                System.Array.Copy(src, offset, hb, Ix(Position), length);
                SetPosition(Position + length);
                return this;
            }

            public override Int64Buffer Put(Int64Buffer src)
            {

                if (src is HeapInt64Buffer) {
                    if (src == this)
                        throw new ArgumentException();
                    HeapInt64Buffer sb = (HeapInt64Buffer)src;
                    int n = sb.Remaining;
                    if (n > Remaining)
                        throw new BufferOverflowException();
                    System.Array.Copy(sb.hb, sb.Ix(sb.Position),
                                     hb, Ix(Position), n);
                    sb.SetPosition(sb.Position + n);
                    SetPosition(Position + n);
                } else if (src.IsDirect)
                {
                    int n = src.Remaining;
                    if (n > Remaining)
                        throw new BufferOverflowException();
                    src.Get(hb, Ix(Position), n);
                    SetPosition(Position + n);
                }
                else
                {
                    base.Put(src);
                }
                return this;
            }

            public override Int64Buffer Compact()
            {
                System.Array.Copy(hb, Ix(Position), hb, Ix(0), Remaining);
                SetPosition(Remaining);
                SetLimit(Capacity);
                DiscardMark();
                return this;
            }

            public override ByteOrder Order
            {
                get
                {
                    throw new NotImplementedException();
                    //return ByteOrder.nativeOrder();
                }
            }
        }
    }
}
