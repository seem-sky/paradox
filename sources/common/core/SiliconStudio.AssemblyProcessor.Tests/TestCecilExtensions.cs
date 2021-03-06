﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
using System;
using System.Collections.Generic;
using Mono.Cecil;
using NUnit.Framework;

namespace SiliconStudio.AssemblyProcessor.Tests
{
    public class TestCecilExtensions
    {
        public class Nested
        {
        }

        private BaseAssemblyResolver assemblyResolver = new DefaultAssemblyResolver();

        public string GenerateNameCecil(Type type)
        {
            var typeReference = type.GenerateTypeCecil(assemblyResolver);

            return typeReference.ConvertAssemblyQualifiedName();
        }

        public static string GenerateNameDotNet(Type type)
        {
            return type.AssemblyQualifiedName;
        }

        public void CheckGeneratedNames(Type type)
        {
            var nameCecil = GenerateNameCecil(type);
            var nameDotNet = GenerateNameDotNet(type);
            Assert.That(nameCecil, Is.EqualTo(nameDotNet));
        }

        [Test]
        public void TestCecilDotNetAssemblyQualifiedNames()
        {
            // Primitive value type
            CheckGeneratedNames(typeof(bool));

            // Primitive class
            CheckGeneratedNames(typeof(string));

            // User class
            CheckGeneratedNames(typeof(TestCecilExtensions));

            // Closed generics
            CheckGeneratedNames(typeof(Dictionary<string, object>));

            // Open generics
            CheckGeneratedNames(typeof(Dictionary<,>));

            // Nested types
            CheckGeneratedNames(typeof(Nested));

            // Arrays
            CheckGeneratedNames(typeof(string[]));
            CheckGeneratedNames(typeof(Dictionary<string, object>[]));

            // Nullable
            CheckGeneratedNames(typeof(bool?));
        }
    }
}