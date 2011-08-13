using System;
using System.Xml.Linq;
using MinotaurLib;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace MinotaurSpecs.StepDefinitions
{
    [Binding]
    public class ComparingXmlSteps
    {
#region Test fields
        private readonly XDocument referenceDocument = new XDocument();
        private readonly XDocument testDocument = new XDocument();
#endregion

        [StepArgumentTransformation]
        public XDocument NamedXDocumentTransform(string xdocumentName)
        {
            switch (xdocumentName)
            {
                case "reference":
                    return referenceDocument;
                case "test":
                    return testDocument;
            }

            return null;
        }

        [Given(@"I have a (.*) XDocument with root ""(.*)"" and elements:")]
        public void GivenIHaveAReferenceXDocumentWithElements(XDocument document, string rootElement, Table table)
        {
            var root = new XElement(rootElement);

            foreach (var row in table.Rows)
            {
                var newItem = 
                    new XElement(row["element"],
                        new XAttribute("id", row["id"]),
                        new XText(row["value"]));

                root.Add(newItem);
            }

            document.Add(root);
        }

        [When(@"I assert the (.*) XDocument should be equivalent to the (.*) XDocument")]
        public void WhenIAssertTheTestXDocumentShouldBeEquivalentToTheReferenceXDocument(XDocument actual, XDocument expected)
        {
            try
            {
                actual.ShouldBeEquivalentTo(expected);
            }
            catch(Exception ex)
            {
                ScenarioContext.Current.Add("exception", ex);
                return;
            }

            ScenarioContext.Current.Add("exception", null);
        }

        [Then(@"no exception should be thrown")]
        public void ThenNoExceptionShouldBeThrown()
        {
            var exception = ScenarioContext.Current["exception"];
            Assert.That(exception, Is.Null);
        }

        [Then(@"an AssertionException should be thrown")]
        public void ThenAnAssertionExceptionShouldBeThrown()
        {
            var exception = ScenarioContext.Current["exception"];
            Assert.AreEqual(exception.GetType(), typeof (AssertionException));
        }


    }
}
