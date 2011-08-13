using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Schema;

namespace MinotaurLib
{
    public static class XmlDeepComparer
    {
        private static class Xsi
        {
            private static XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";

            public static XName schemaLocation = xsi + "schemaLocation";
            public static XName noNamespaceSchemaLocation = xsi + "noNamespaceSchemaLocation";
        }

        /// <summary>
        /// Deep compares the two XML content fields.
        /// </summary>
        /// <param name="leftDocument">The left document.</param>
        /// <param name="rightDocument">The right document.</param>
        /// <param name="schema">(Optional) The schema set to validate against</param>
        /// <returns>True if the two documents are equal after XML normalisation</returns>
        public static bool DeepCompareXmlContent(
            string leftDocument, string rightDocument, XmlSchemaSet schema)
        {
            return DeepCompareXmlContent(
                XDocument.Parse(leftDocument),
                XDocument.Parse(rightDocument),
                schema);
        }

        /// <summary>
        /// Deep compares the two XML content fields.
        /// </summary>
        /// <param name="leftDocument">The left document.</param>
        /// <param name="rightDocument">The right document.</param>
        /// <param name="schema">(Optional) The schema set to validate against</param>
        /// <returns>True if the two documents are equal after XML normalisation</returns>
        public static bool DeepCompareXmlContent(
            XDocument leftDocument,
            XDocument rightDocument, 
            XmlSchemaSet schema)
        {
            XDocument d1;
            XDocument d2;

            try
            {
                d1 = Normalize(leftDocument, schema);
                d2 = Normalize(rightDocument, schema);
            }
            catch (Exception ex)
            {
                return false;
            }

            // TODO: Roll our own DeepEquals method to provide feedback on what's different
            return XNode.DeepEquals(d1, d2);
        }

        private static XDocument Normalize(XDocument source, XmlSchemaSet schema)
        {
            bool havePSVI = false;
            // validate, throw errors, add PSVI information
            if (schema != null)
            {
                source.Validate(schema, null, true);
                havePSVI = true;
            }
            return new XDocument(
                source.Declaration,
                source.Nodes().Select(n =>
                {
                    // Remove comments, processing instructions, and text nodes that are
                    // children of XDocument.  Only white space text nodes are allowed as
                    // children of a document, so we can remove all text nodes.
                    if (n is XComment || n is XProcessingInstruction || n is XText)
                        return null;
                    var e = n as XElement;
                    if (e != null)
                        return NormalizeElement(e, havePSVI);
                    return n;
                }
                )
            );
        }

        private static bool DeepEqualsWithNormalization(XDocument doc1, XDocument doc2,
            XmlSchemaSet schemaSet)
        {
            XDocument d1 = Normalize(doc1, schemaSet);
            XDocument d2 = Normalize(doc2, schemaSet);
            return XNode.DeepEquals(d1, d2);
        }

        private static IEnumerable<XAttribute> NormalizeAttributes(XElement element,
            bool havePSVI)
        {
            return element.Attributes()
                    .Where(a => !a.IsNamespaceDeclaration &&
                        a.Name != Xsi.schemaLocation &&
                        a.Name != Xsi.noNamespaceSchemaLocation)
                    .OrderBy(a => a.Name.NamespaceName)
                    .ThenBy(a => a.Name.LocalName)
                    .Select(
                        a =>
                        {
                            if (havePSVI)
                            {
                                var dt = a.GetSchemaInfo().SchemaType.TypeCode;
                                switch (dt)
                                {
                                    case XmlTypeCode.Boolean:
                                        return new XAttribute(a.Name, (bool)a);
                                    case XmlTypeCode.DateTime:
                                        return new XAttribute(a.Name, (DateTime)a);
                                    case XmlTypeCode.Decimal:
                                        return new XAttribute(a.Name, (decimal)a);
                                    case XmlTypeCode.Double:
                                        return new XAttribute(a.Name, (double)a);
                                    case XmlTypeCode.Float:
                                        return new XAttribute(a.Name, (float)a);
                                    case XmlTypeCode.HexBinary:
                                    case XmlTypeCode.Language:
                                        return new XAttribute(a.Name,
                                            ((string)a).ToLower());
                                }
                            }
                            return a;
                        }
                    );
        }

        private static XNode NormalizeNode(XNode node, bool havePSVI)
        {
            // trim comments and processing instructions from normalized tree
            if (node is XComment || node is XProcessingInstruction)
                return null;
            XElement e = node as XElement;
            if (e != null)
                return NormalizeElement(e, havePSVI);
            // Only thing left is XCData and XText, so clone them
            return node;
        }

        private static XElement NormalizeElement(XElement element, bool havePSVI)
        {
            if (havePSVI)
            {
                var dt = element.GetSchemaInfo();
                switch (dt.SchemaType.TypeCode)
                {
                    case XmlTypeCode.Boolean:
                        return new XElement(element.Name,
                            NormalizeAttributes(element, havePSVI),
                            (bool)element);
                    case XmlTypeCode.DateTime:
                        return new XElement(element.Name,
                            NormalizeAttributes(element, havePSVI),
                            (DateTime)element);
                    case XmlTypeCode.Decimal:
                        return new XElement(element.Name,
                            NormalizeAttributes(element, havePSVI),
                            (decimal)element);
                    case XmlTypeCode.Double:
                        return new XElement(element.Name,
                            NormalizeAttributes(element, havePSVI),
                            (double)element);
                    case XmlTypeCode.Float:
                        return new XElement(element.Name,
                            NormalizeAttributes(element, havePSVI),
                            (float)element);
                    case XmlTypeCode.HexBinary:
                    case XmlTypeCode.Language:
                        return new XElement(element.Name,
                            NormalizeAttributes(element, havePSVI),
                            ((string)element).ToLower());
                    default:
                        return new XElement(element.Name,
                            NormalizeAttributes(element, havePSVI),
                            element.Nodes().Select(n => NormalizeNode(n, havePSVI))
                        );
                }
            }
            else
            {
                return new XElement(element.Name,
                    NormalizeAttributes(element, havePSVI),
                    element.Nodes().Select(n => NormalizeNode(n, havePSVI))
                );
            }
        }
    }
}
