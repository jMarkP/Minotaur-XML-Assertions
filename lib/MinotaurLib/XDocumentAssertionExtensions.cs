using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace MinotaurLib
{
    public static class XDocumentAssertionExtensions
    {
        public static void ShouldBeEquivalentTo(this XDocument actual, XDocument expected)
        {
            string message = string.Empty;

            if (!CompareXmlNodes(actual.Root, expected.Root, ref message))
            {
                // TODO
            }
        }

        /// <summary>
        /// Recursively compares two XML nodes.
        /// </summary>
        /// <param name="expected">The expected element.</param>
        /// <param name="actual">The actual element.</param>
        /// <param name="message">The error message.</param>
        /// <returns>true if the two elements are the same (ignoring differences in ID fields etc.)</returns>
        private static bool CompareXmlNodes(XElement expected, XElement actual, ref string message)
        {
            // Is either null?
            if (expected == null || actual == null)
            {
                message = string.Format("Element null: {0}", expected.Name.LocalName);
                return false;
            }

            // If it's a text node, does the text match?
            if (expected.NodeType == XmlNodeType.Text && expected.Value != actual.Value)
            {
                message = string.Format("Text different - {0}({1}) != {2}({3})", expected.Name.LocalName, expected.Value, actual.Name.LocalName, actual.Value);
                return false;
            }

            // Check all the attributes
            foreach (var attr in expected.Attributes())
            {
                var actualAttr = actual.Attributes(attr.Name).FirstOrDefault();

                if (actualAttr != null && attr.Value != actualAttr.Value)
                {
                    message = string.Format(
                        "Attribute different - {0}/{1}({2}) != {3}/{4}({5})",
                        expected.Name.LocalName,
                        attr.Name.LocalName,
                        attr.Value,
                        actual.Name.LocalName,
                        actualAttr.Name.LocalName,
                        actualAttr.Value);
                    return false;
                }
            }

            // Recurse over the children
            foreach (var expectedChild in expected.Elements())
            {
                var foundMatch = false;
                foreach (var actualChild in actual.Elements(expectedChild.Name))
                {
                    if (CompareXmlNodes(expectedChild, actualChild, ref message))
                    {
                        foundMatch = true;
                        continue;
                    }

                    if (expected.Elements(expectedChild.Name).Count() == 1)
                    {
                        return false;
                    }
                }

                if (foundMatch)
                {
                    continue;
                }

                message = string.Format(
                    "No match for {0} ({1}) found", expectedChild.Name.LocalName, expectedChild.Value);
                return false;
            }

            return true;
        }
    }
}
