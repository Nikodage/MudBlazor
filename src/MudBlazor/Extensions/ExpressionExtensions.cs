﻿// Copyright (c) MudBlazor 2021
// MudBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MudBlazor
{
    public static class ExpressionExtensions
    {
        public static string GetFullPathOfMember<T>(this Expression<Func<T>> property)
        {
            var resultingString = string.Empty;
            var p = property.Body as MemberExpression;

            while (p != null)
            {
                if (p.Expression is MemberExpression)
                {
                    resultingString = p.Member.Name + (resultingString != string.Empty ? "." : string.Empty) + resultingString;
                }
                p = p.Expression as MemberExpression;
            }
            return resultingString;
        }

        /// <summary>
        /// Returns the display name attribute of the provided field property as a string. If this attribute is missing, the member name will be returned.
        /// </summary>
        public static string GetDisplayNameString<T>(this Expression<Func<T>> expression)
        {
            var memberExpression = (MemberExpression)expression.Body;
            var propertyInfo = memberExpression.Expression?.Type.GetProperty(memberExpression.Member.Name);
            return propertyInfo?.GetCustomAttributes(typeof(DisplayAttribute), true).Cast<DisplayAttribute>().FirstOrDefault()?.Name ?? string.Empty;
        }
    }
}
