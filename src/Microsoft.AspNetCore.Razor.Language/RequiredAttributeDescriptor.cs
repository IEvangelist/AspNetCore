﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.AspNetCore.Razor.Language
{
    [DebuggerDisplay("{DisplayName,nq}")]
    public abstract class RequiredAttributeDescriptor : IEquatable<RequiredAttributeDescriptor>
    {
        public string Name { get; protected set; }

        public NameComparisonMode NameComparison { get; protected set; }

        public string Value { get; protected set; }

        public ValueComparisonMode ValueComparison { get; protected set; }

        public string DisplayName { get; protected set; }

        public IReadOnlyList<RazorDiagnostic> Diagnostics { get; protected set; }

        public bool HasErrors
        {
            get
            {
                var errors = Diagnostics.Any(diagnostic => diagnostic.Severity == RazorDiagnosticSeverity.Error);

                return errors;
            }
        }
        
        public bool Equals(RequiredAttributeDescriptor other)
        {
            return RequiredAttributeDescriptorComparer.Default.Equals(this, other);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RequiredAttributeDescriptor);
        }

        public override int GetHashCode()
        {
            return RequiredAttributeDescriptorComparer.Default.GetHashCode(this);
        }

        /// <summary>
        /// Acceptable <see cref="RequiredAttributeDescriptor.Name"/> comparison modes.
        /// </summary>
        public enum NameComparisonMode
        {
            /// <summary>
            /// HTML attribute name case insensitively matches <see cref="RequiredAttributeDescriptor.Name"/>.
            /// </summary>
            FullMatch,

            /// <summary>
            /// HTML attribute name case insensitively starts with <see cref="RequiredAttributeDescriptor.Name"/>.
            /// </summary>
            PrefixMatch,
        }

        /// <summary>
        /// Acceptable <see cref="RequiredAttributeDescriptor.Value"/> comparison modes.
        /// </summary>
        public enum ValueComparisonMode
        {
            /// <summary>
            /// HTML attribute value always matches <see cref="RequiredAttributeDescriptor.Value"/>.
            /// </summary>
            None,

            /// <summary>
            /// HTML attribute value case sensitively matches <see cref="RequiredAttributeDescriptor.Value"/>.
            /// </summary>
            FullMatch,

            /// <summary>
            /// HTML attribute value case sensitively starts with <see cref="RequiredAttributeDescriptor.Value"/>.
            /// </summary>
            PrefixMatch,

            /// <summary>
            /// HTML attribute value case sensitively ends with <see cref="RequiredAttributeDescriptor.Value"/>.
            /// </summary>
            SuffixMatch,
        }
    }
}
