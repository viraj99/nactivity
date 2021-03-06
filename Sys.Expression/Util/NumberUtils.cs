#region License

/*
 * Copyright © 2002-2011 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

#region Imports

using System;
using System.CodeDom;
using System.ComponentModel;

#endregion

namespace Spring.Util
{
    /// <summary>
    /// Various utility methods relating to numbers.
    /// </summary>
    /// <remarks>
    /// <p>
    /// Mainly for internal use within the framework.
    /// </p>
    /// </remarks>
    /// <author>Aleksandar Seovic</author>
    public sealed class NumberUtils
    {
        /// <summary>
        /// Determines whether the supplied <paramref name="number"/> is an integer.
        /// </summary>
        /// <param name="number">The object to check.</param>
        /// <returns>
        /// <see lang="true"/> if the supplied <paramref name="number"/> is an integer.
        /// </returns>
        public static bool IsInteger(object number)
        {
            return number is Int32 || number is Int16 || number is Int64 || number is UInt32
                || number is UInt16 || number is UInt64 || number is Byte || number is SByte;
        }

        /// <summary>
        /// Determines whether the supplied <paramref name="number"/> is a decimal number.
        /// </summary>
        /// <param name="number">The object to check.</param>
        /// <returns>
        /// <see lang="true"/> if the supplied <paramref name="number"/> is a decimal number.
        /// </returns>
        public static bool IsDecimal(object number)
        {

            return number is Single || number is Double || number is Decimal;
        }

        /// <summary>
        /// Determines whether the supplied <paramref name="number"/> is of numeric type.
        /// </summary>
        /// <param name="number">The object to check.</param>
        /// <returns>
        /// 	<c>true</c> if the specified object is of numeric type; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNumber(object number)
        {
            return IsInteger(number) || IsDecimal(number);
        }
        public static decimal ToDecimal(object number)
        {
            return Convert.ToDecimal(number);
        }

        /// <summary>
        /// Determines whether the supplied <paramref name="number"/> can be converted to an integer.
        /// </summary>
        /// <param name="number">The object to check.</param>
        /// <returns>
        /// <see lang="true"/> if the supplied <paramref name="number"/> can be converted to an integer.
        /// </returns>
        public static bool CanConvertToInteger(object number)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(number);
            return converter.CanConvertTo(typeof(Int32))
                || converter.CanConvertTo(typeof(Int16))
                || converter.CanConvertTo(typeof(Int64))
                || converter.CanConvertTo(typeof(UInt16))
                || converter.CanConvertTo(typeof(UInt64))
                || converter.CanConvertTo(typeof(Byte))
                || converter.CanConvertTo(typeof(SByte))
                   ;
        }

        /// <summary>
        /// Determines whether the supplied <paramref name="number"/> can be converted to an integer.
        /// </summary>
        /// <param name="number">The object to check.</param>
        /// <returns>
        /// <see lang="true"/> if the supplied <paramref name="number"/> can be converted to an integer.
        /// </returns>
        public static bool CanConvertToDecimal(object number)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(number);
            return converter.CanConvertTo(typeof(Single))
                || converter.CanConvertTo(typeof(Double))
                || converter.CanConvertTo(typeof(Decimal))
                   ;
        }

        /// <summary>
        /// Determines whether the supplied <paramref name="number"/> can be converted to a number.
        /// </summary>
        /// <param name="number">The object to check.</param>
        /// <returns>
        /// 	<c>true</c> if the specified object is decimal number; otherwise, <c>false</c>.
        /// </returns>
        public static bool CanConvertToNumber(object number)
        {
            return CanConvertToInteger(number) || CanConvertToDecimal(number);
        }

        internal static bool TryConvertTo(ref object from, ref object to)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(to.GetType());
            if (!converter.CanConvertFrom(from.GetType()))
            {
                return false;
            }

            TypeCode toCode = Convert.GetTypeCode(to);

            try
            {
                if (from.ToString().Contains("."))
                {
                    if (toCode == TypeCode.Single || toCode == TypeCode.Double || toCode == TypeCode.Decimal)
                    {
                        converter = TypeDescriptor.GetConverter(to.GetType());
                        from = converter.ConvertTo(from, to.GetType());
                    }
                    else
                    {
                        converter = TypeDescriptor.GetConverter(typeof(decimal));
                        from = converter.ConvertFrom(from);
                        to = converter.ConvertFrom(to.ToString());
                    }
                }
                else
                {
                    from = converter.ConvertTo(from, to.GetType());
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Is the supplied <paramref name="number"/> equal to zero (0)?
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <returns>
        /// <see lang="true"/> id the supplied <paramref name="number"/> is equal to zero (0).
        /// </returns>
        public static bool IsZero(object number)
        {
            if (number is Int32)
                return (Convert.ToInt32(number)) == 0;
            else if (number is Int16)
                return (Convert.ToInt16(number)) == 0;
            else if (number is Int64)
                return (Convert.ToInt64(number)) == 0;
            else if (number is UInt16)
                return (Convert.ToUInt16(number)) == 0;
            else if (number is UInt32)
                return (Convert.ToUInt32(number)) == 0;
            else if (number is UInt64)
                return Convert.ToDecimal(number) == 0;
            else if (number is Byte)
                return (Convert.ToByte(number)) == 0;
            else if (number is SByte)
                return (Convert.ToSByte(number)) == 0;
            else if (number is Single)
                return (Convert.ToSingle(number)) == 0f;
            else if (number is Double)
                return (Convert.ToDouble(number)) == 0d;
            else if (number is Decimal)
                return (Convert.ToDecimal(number)) == 0m;
            return false;
        }

        /// <summary>
        /// Negates the supplied <paramref name="number"/>.
        /// </summary>
        /// <param name="number">The number to negate.</param>
        /// <returns>The supplied <paramref name="number"/> negated.</returns>
        /// <exception cref="System.ArgumentException">
        /// If the supplied <paramref name="number"/> is not a supported numeric type.
        /// </exception>
        public static object Negate(object number)
        {
            if (number is Int32)
                return -Convert.ToInt32(number);
            else if (number is Int16)
                return -Convert.ToInt16(number);
            else if (number is Int64)
                return -Convert.ToInt64(number);
            else if (number is UInt16)
                return -Convert.ToInt32(number);
            else if (number is UInt32)
                return -Convert.ToInt64(number);
            else if (number is UInt64)
                return -Convert.ToDecimal(number);
            else if (number is Byte)
                return -Convert.ToInt16(number);
            else if (number is SByte)
                return -Convert.ToInt16(number);
            else if (number is Single)
                return -Convert.ToSingle(number);
            else if (number is Double)
                return -Convert.ToDouble(number);
            else if (number is Decimal)
                return -Convert.ToDecimal(number);
            else
            {
                throw new ArgumentException(string.Format("'{0}' is not one of the supported numeric types.", number));
            }
        }

        /// <summary>
        /// Returns the bitwise not (~) of the supplied <paramref name="number"/>.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <returns>The value of ~<paramref name="number"/>.</returns>
        /// <exception cref="System.ArgumentException">
        /// If the supplied <paramref name="number"/> is not a supported numeric type.
        /// </exception>
        public static object BitwiseNot(object number)
        {
            if (number is bool)
                return !Convert.ToBoolean(number);
            else if (number is Int32)
                return ~Convert.ToInt32(number);
            else if (number is Int16)
                return ~Convert.ToInt16(number);
            else if (number is Int64)
                return ~Convert.ToInt64(number);
            else if (number is UInt16)
                return ~Convert.ToUInt16(number);
            else if (number is UInt32)
                return ~Convert.ToUInt32(number);
            else if (number is UInt64)
                return ~Convert.ToUInt64(number);
            else if (number is Byte)
                return ~Convert.ToByte(number);
            else if (number is SByte)
                return ~Convert.ToSByte(number);
            else
            {
                throw new ArgumentException(string.Format("'{0}' is not one of the supported integer types.", number));
            }
        }

        /// <summary>
        /// Bitwise ANDs (&amp;) the specified integral values.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        /// <exception cref="System.ArgumentException">
        /// If one of the supplied arguments is not a supported integral types.
        /// </exception>
        public static object BitwiseAnd(object m, object n)
        {
            CoerceTypes(ref m, ref n);

            if (n is bool)
                return Convert.ToBoolean(m) & Convert.ToBoolean(n);
            else if (n is Int32)
                return Convert.ToInt32(m) & Convert.ToInt32(n);
            else if (n is Int16)
                return Convert.ToInt16(m) & Convert.ToInt16(n);
            else if (n is Int64)
                return Convert.ToInt64(m) & Convert.ToInt64(n);
            else if (n is UInt16)
                return Convert.ToUInt16(m) & Convert.ToUInt16(n);
            else if (n is UInt32)
                return Convert.ToUInt32(m) & Convert.ToUInt32(n);
            else if (n is UInt64)
                return Convert.ToUInt64(m) & Convert.ToUInt64(n);
            else if (n is Byte)
                return Convert.ToByte(m) & Convert.ToByte(n);
            else if (n is SByte)
                return Convert.ToSByte(m) & Convert.ToSByte(n);
            else
            {
                throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported integral types.", m, n));
            }
        }

        /// <summary>
        /// Bitwise ORs (|) the specified integral values.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        /// <exception cref="System.ArgumentException">
        /// If one of the supplied arguments is not a supported integral types.
        /// </exception>
        public static object BitwiseOr(object m, object n)
        {
            CoerceTypes(ref m, ref n);

            if (n is bool)
                return (bool)m | (bool)n;
            else if (n is Int32)
                return Convert.ToInt32(m) | Convert.ToInt32(n);
            else if (n is Int16)
                return Convert.ToInt16(m) | Convert.ToInt16(n);
            else if (n is Int64)
                return Convert.ToInt64(m) | Convert.ToInt64(n);
            else if (n is UInt16)
                return Convert.ToUInt16(m) | Convert.ToUInt16(n);
            else if (n is UInt32)
                return Convert.ToUInt32(m) | Convert.ToUInt32(n);
            else if (n is UInt64)
                return Convert.ToUInt64(m) | Convert.ToUInt64(n);
            else if (n is Byte)
                return Convert.ToByte(m) | Convert.ToByte(n);
            else if (n is SByte)
            {
                if (SystemUtils.MonoRuntime)
                {
                    SByte x = Convert.ToSByte(n);
                    SByte y = Convert.ToSByte(m);
                    int result = (int)x | (int)y;
                    return SByte.Parse(result.ToString());
                }
                return (SByte)(Convert.ToSByte(m) | Convert.ToSByte(n));
            }
            throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported integral types.", m, n));
        }

        /// <summary>
        /// Bitwise XORs (^) the specified integral values.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        /// <exception cref="System.ArgumentException">
        /// If one of the supplied arguments is not a supported integral types.
        /// </exception>
        public static object BitwiseXor(object m, object n)
        {
            CoerceTypes(ref m, ref n);

            if (n is bool)
                return (bool)m ^ (bool)n;
            else if (n is Int32)
                return Convert.ToInt32(m) ^ Convert.ToInt32(n);
            else if (n is Int16)
                return Convert.ToInt16(m) ^ Convert.ToInt16(n);
            else if (n is Int64)
                return Convert.ToInt64(m) ^ Convert.ToInt64(n);
            else if (n is UInt16)
                return Convert.ToUInt16(m) ^ Convert.ToUInt16(n);
            else if (n is UInt32)
                return Convert.ToUInt32(m) ^ Convert.ToUInt32(n);
            else if (n is UInt64)
                return Convert.ToUInt64(m) ^ Convert.ToUInt64(n);
            else if (n is Byte)
                return Convert.ToByte(m) ^ Convert.ToByte(n);
            else if (n is SByte)
                return Convert.ToSByte(m) ^ Convert.ToSByte(n);
            else
            {
                throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported integral types.", m, n));
            }
        }

        /// <summary>
        /// Adds the specified numbers.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        public static object Add(object m, object n)
        {
            CoerceTypes(ref m, ref n);

            if (n is Int32)
                return Convert.ToInt32(m) + Convert.ToInt32(n);
            else if (n is Int16)
                return Convert.ToInt16(m) + Convert.ToInt16(n);
            else if (n is Int64)
                return Convert.ToInt64(m) + Convert.ToInt64(n);
            else if (n is UInt16)
                return Convert.ToUInt16(m) + Convert.ToUInt16(n);
            else if (n is UInt32)
                return Convert.ToUInt32(m) + Convert.ToUInt32(n);
            else if (n is UInt64)
                return Convert.ToUInt64(m) + Convert.ToUInt64(n);
            else if (n is Byte)
                return Convert.ToByte(m) + Convert.ToByte(n);
            else if (n is SByte)
                return Convert.ToSByte(m) + Convert.ToSByte(n);
            else if (n is Single)
                return Convert.ToSingle(m) + Convert.ToSingle(n);
            else if (n is Double)
                return Convert.ToDouble(m) + Convert.ToDouble(n);
            else if (n is Decimal)
                return Convert.ToDecimal(m) + Convert.ToDecimal(n);
            else
            {
                throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
            }
        }

        /// <summary>
        /// Subtracts the specified numbers.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        public static object Subtract(object m, object n)
        {
            CoerceTypes(ref m, ref n);

            if (n is Int32)
                return Convert.ToInt32(m) - Convert.ToInt32(n);
            else if (n is Int16)
                return Convert.ToInt16(m) - Convert.ToInt16(n);
            else if (n is Int64)
                return Convert.ToInt64(m) - Convert.ToInt64(n);
            else if (n is UInt16)
                return Convert.ToUInt16(m) - Convert.ToUInt16(n);
            else if (n is UInt32)
                return Convert.ToUInt32(m) - Convert.ToUInt32(n);
            else if (n is UInt64)
                return Convert.ToUInt64(m) - Convert.ToUInt64(n);
            else if (n is Byte)
                return Convert.ToByte(m) - Convert.ToByte(n);
            else if (n is SByte)
                return Convert.ToSByte(m) - Convert.ToSByte(n);
            else if (n is Double || n is Decimal || n is Single)
                return Convert.ToDecimal(m) - Convert.ToDecimal(n);
            else
            {
                throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
            }
        }

        /// <summary>
        /// Multiplies the specified numbers.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        public static object Multiply(object m, object n)
        {
            CoerceTypes(ref m, ref n);

            if (n is Int32)
                return Convert.ToInt32(m) * Convert.ToInt32(n);
            else if (n is Int16)
                return Convert.ToInt16(m) * Convert.ToInt16(n);
            else if (n is Int64)
                return Convert.ToInt64(m) * Convert.ToInt64(n);
            else if (n is UInt16)
                return Convert.ToUInt16(m) * Convert.ToUInt16(n);
            else if (n is UInt32)
                return Convert.ToUInt32(m) * Convert.ToUInt32(n);
            else if (n is UInt64)
                return Convert.ToUInt64(m) * Convert.ToUInt64(n);
            else if (n is Byte)
                return Convert.ToByte(m) * Convert.ToByte(n);
            else if (n is SByte)
                return Convert.ToSByte(m) * Convert.ToSByte(n);
            else if (n is Single)
                return Convert.ToSingle(m) * Convert.ToSingle(n);
            else if (n is Double)
                return Convert.ToDouble(m) * Convert.ToDouble(n);
            else if (n is Decimal)
                return Convert.ToDecimal(m) * Convert.ToDecimal(n);
            else
            {
                throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
            }
        }

        /// <summary>
        /// Divides the specified numbers.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        /// <param name="forceDecimal">强制使用小数计算</param>
        public static object Divide(object m, object n, bool forceDecimal = false)
        {
            CoerceTypes(ref m, ref n);

            if (forceDecimal)
            {
                return ToDecimal(m) / ToDecimal(n);
            }

            if (n is Int32 || n is Int16)
            {
                var l = Convert.ToInt32(m);
                var r = Convert.ToInt32(n);
                if (l % r != 0)
                {
                    return Convert.ToDecimal(l) / r;
                }
                return l / r;
            }
            else if (n is Int64)
            {
                var l = Convert.ToInt64(m);
                var r = Convert.ToInt64(n);
                if (l % r != 0)
                {
                    return Convert.ToDecimal(l) / r;
                }
                return l / r;
            }
            else if (n is UInt16 || n is UInt32)
            {
                var l = (uint)m;
                var r = (uint)n;
                if (l % r != 0)
                {
                    return Convert.ToDecimal(l) / r;
                }
                return l / r;
            }
            else if (n is UInt64)
            {
                var l = Convert.ToUInt64(m);
                var r = Convert.ToUInt64(n);
                if (l % r != 0)
                {
                    return Convert.ToDecimal(l) / r;
                }
                return l / r;
            }
            else if (n is Byte)
                return Convert.ToByte(m) / Convert.ToByte(n);
            else if (n is SByte)
                return Convert.ToSByte(m) / Convert.ToSByte(n);
            else if (n is Single)
                return Convert.ToSingle(m) / Convert.ToSingle(n);
            else if (n is Double)
                return Convert.ToDouble(m) / Convert.ToDouble(n);
            else if (n is Decimal)
                return Convert.ToDecimal(m) / Convert.ToDecimal(n);
            else
            {
                throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
            }
        }

        /// <summary>
        /// Calculates remainder for the specified numbers.
        /// </summary>
        /// <param name="m">The first number (dividend).</param>
        /// <param name="n">The second number (divisor).</param>
        public static object Modulus(object m, object n)
        {
            CoerceTypes(ref m, ref n);

            if (n is Int32)
                return Convert.ToInt32(m) % Convert.ToInt32(n);
            else if (n is Int16)
                return Convert.ToInt16(m) % Convert.ToInt16(n);
            else if (n is Int64)
                return Convert.ToInt64(m) % Convert.ToInt64(n);
            else if (n is UInt16)
                return Convert.ToUInt16(m) % Convert.ToUInt16(n);
            else if (n is UInt32)
                return Convert.ToUInt32(m) % Convert.ToUInt32(n);
            else if (n is UInt64)
                return Convert.ToUInt64(m) % Convert.ToUInt64(n);
            else if (n is Byte)
                return Convert.ToByte(m) % Convert.ToByte(n);
            else if (n is SByte)
                return Convert.ToSByte(m) % Convert.ToSByte(n);
            else if (n is Single)
                return Convert.ToSingle(m) % Convert.ToSingle(n);
            else if (n is Double)
                return Convert.ToDouble(m) % Convert.ToDouble(n);
            else if (n is Decimal)
                return Convert.ToDecimal(m) % Convert.ToDecimal(n);
            else
            {
                throw new ArgumentException(string.Format("'{0}' and/or '{1}' are not one of the supported numeric types.", m, n));
            }
        }

        /// <summary>
        /// Raises first number to the power of the second one.
        /// </summary>
        /// <param name="m">The first number.</param>
        /// <param name="n">The second number.</param>
        public static object Power(object m, object n)
        {
            return Math.Pow(Convert.ToDouble(m), Convert.ToDouble(n));
        }

        /// <summary>
        /// Coerces the types so they can be compared.
        /// </summary>
        /// <param name="m">The right.</param>
        /// <param name="n">The left.</param>
        public static void CoerceTypes(ref object m, ref object n)
        {
            if (m.ToString().Contains(".") && !n.ToString().Contains("."))
            {
                m = Convert.ChangeType(m, m is string ? typeof(decimal) : m.GetType());
                n = Convert.ChangeType(n, m.GetType());
            }
            else if (n.ToString().Contains(".") && !m.ToString().Contains("."))
            {
                n = Convert.ChangeType(n, n is string ? typeof(decimal) : n.GetType());
                m = Convert.ChangeType(m, n.GetType());
            }
            else
            {
                TypeCode leftTypeCode = Convert.GetTypeCode(m);
                TypeCode rightTypeCode = Convert.GetTypeCode(n);

                if (leftTypeCode == TypeCode.String)
                {
                    m = Convert.ChangeType(m, rightTypeCode);
                }
                else if (rightTypeCode == TypeCode.String)
                {
                    n = Convert.ChangeType(n, leftTypeCode);
                }
                else if (leftTypeCode > rightTypeCode)
                {
                    n = Convert.ChangeType(n, leftTypeCode);
                }
                else
                {
                    m = Convert.ChangeType(m, rightTypeCode);
                }
            }
        }

        #region Constructor (s) / Destructor

        // CLOVER:OFF

        /// <summary>
        /// Creates a new instance of the <see cref="Spring.Util.NumberUtils"/> class.
        /// </summary>
        /// <remarks>
        /// <p>
        /// This is a utility class, and as such exposes no public constructors.
        /// </p>
        /// </remarks>
        private NumberUtils()
        {
        }

        // CLOVER:ON

        #endregion
    }
}