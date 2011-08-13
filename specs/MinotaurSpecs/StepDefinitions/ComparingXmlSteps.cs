using System.Xml.Linq;
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

        [When(@"I assert the XDocument under test should be equivalent to the reference XDocument")]
        public void WhenIAssertTheXDocumentUnderTestShouldBeEquivalentToTheReferenceXDocument()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"no exception should be thrown")]
        public void ThenNoExceptionShouldBeThrown()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
