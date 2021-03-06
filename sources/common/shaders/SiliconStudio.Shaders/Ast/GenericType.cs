// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiliconStudio.Shaders.Ast
{
    /// <summary>
    /// Base class for all generic types.
    /// </summary>
    public abstract class GenericType : TypeBase
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericType"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="parameterCount">
        /// The parameter count.
        /// </param>
        protected GenericType(string name, int parameterCount)
            : base(name)
        {
            ParameterTypes = new List<Type>();
            Parameters = new List<Node>();
            for (int i = 0; i < parameterCount; i++)
            {
                Parameters.Add(null);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(Name).Append("<");
            for (int i = 0; i < Parameters.Count; i++)
            {
                var parameter = Parameters[i];
                if (i > 0)
                {
                    builder.Append(",");
                }

                builder.Append(parameter is TypeBase ? ((TypeBase)parameter).Name : parameter);
            }

            builder.Append(">");

            return builder.ToString();
        }

        /// <summary>
        ///   Gets or sets the parameter types.
        /// </summary>
        /// <value>
        ///   The parameter types.
        /// </value>
        public List<Type> ParameterTypes { get; set; }

        /// <summary>
        ///   Gets or sets the parameters.
        /// </summary>
        /// <value>
        ///   The parameters.
        /// </value>
        public List<Node> Parameters { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">
        /// The other.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="GenericType"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(GenericType other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            //return base.Equals(other) && ParameterTypes.SequenceEqual(other.ParameterTypes) && Parameters.SequenceEqual(other.Parameters);
            return base.Equals(other) && Parameters.SequenceEqual(other.Parameters);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return Equals(obj as GenericType);
        }

        /// <summary>
        /// Gets the child nodes.
        /// </summary>
        /// <returns>
        /// </returns>
        public override IEnumerable<Node> Childrens()
        {
            ChildrenList.Clear();
            foreach (var parameter in Parameters)
            {
                if (parameter is Node)
                {
                    ChildrenList.Add((Node)parameter);
                }
            }

            return ChildrenList;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = base.GetHashCode() * 397;
            foreach (var parameter in Parameters)
            {
                hashCode = (hashCode * 397) ^ (parameter != null ? parameter.GetHashCode() : 0);
            }

            return hashCode;
        }

        #endregion

        #region Operators

        /// <summary>
        ///   Implements the operator ==.
        /// </summary>
        /// <param name = "left">The left.</param>
        /// <param name = "right">The right.</param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator ==(GenericType left, GenericType right)
        {
            return Equals(left, right);
        }

        /// <summary>
        ///   Implements the operator !=.
        /// </summary>
        /// <param name = "left">The left.</param>
        /// <param name = "right">The right.</param>
        /// <returns>
        ///   The result of the operator.
        /// </returns>
        public static bool operator !=(GenericType left, GenericType right)
        {
            return !Equals(left, right);
        }

        #endregion
    }

    /// <summary>
    /// Generic with one parameter.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the parameter 1.
    /// </typeparam>
    public class GenericType<T1> : GenericType
    {
        #region Constants and Fields

        private static readonly List<Type> ParameterTypeT1 = new List<Type> { typeof(T1) };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericType&lt;T1&gt;"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public GenericType(string name)
            : this(name, 1)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "GenericType&lt;T1&gt;" /> class.
        /// </summary>
        public GenericType()
            : this(null, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericType&lt;T1&gt;"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="parameterCount">
        /// The parameter count.
        /// </param>
        protected GenericType(string name, int parameterCount)
            : base(name, parameterCount)
        {
            ParameterTypes = ParameterTypeT1;
        }

        #endregion
    }

    /// <summary>
    /// Generic type with two parameters.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the parameter 1.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the parameter 2.
    /// </typeparam>
    public class GenericType<T1, T2> : GenericType<T1>
    {
        #region Constants and Fields

        private static readonly List<Type> ParameterTypeT1T2 = new List<Type> { typeof(T1), typeof(T2) };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericType&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public GenericType(string name)
            : this(name, 2)
        {
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "GenericType&lt;T1, T2&gt;" /> class.
        /// </summary>
        public GenericType()
            : this(null, 2)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericType&lt;T1, T2&gt;"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="parameterCount">
        /// The parameter count.
        /// </param>
        protected GenericType(string name, int parameterCount)
            : base(name, parameterCount)
        {
            ParameterTypes = ParameterTypeT1T2;
        }

        #endregion
    }

    /// <summary>
    /// Generic type with three parameters.
    /// </summary>
    /// <typeparam name="T1">
    /// The type of the parameter 1.
    /// </typeparam>
    /// <typeparam name="T2">
    /// The type of the parameter 2.
    /// </typeparam>
    /// <typeparam name="T3">
    /// The type of the parameter 3.
    /// </typeparam>
    public class GenericType<T1, T2, T3> : GenericType<T1, T2>
    {
        #region Constants and Fields

        private static readonly List<Type> ParameterTypeT1T2T3 = new List<Type> { typeof(T1), typeof(T2), typeof(T3) };

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericType&lt;T1, T2, T3&gt;"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public GenericType(string name)
            : this(name, 3)
        {
            ParameterTypes = ParameterTypeT1T2T3;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref = "GenericType&lt;T1, T2, T3&gt;" /> class.
        /// </summary>
        public GenericType()
            : this(null, 3)
        {
            ParameterTypes = ParameterTypeT1T2T3;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericType&lt;T1, T2, T3&gt;"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="parameterCount">
        /// The parameter count.
        /// </param>
        protected GenericType(string name, int parameterCount)
            : base(name, parameterCount)
        {
            ParameterTypes = ParameterTypeT1T2T3;
        }

        #endregion
    }
}