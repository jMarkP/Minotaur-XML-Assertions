// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.7.0.0
//      SpecFlow Generator Version:1.7.0.0
//      Runtime Version:4.0.30319.235
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace MinotaurSpecs
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.7.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("In order to test my code\nAs a programmer\nI want to be able to test if two xml doc" +
        "uments are equivalent")]
    public partial class ComparingXMLFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ComparingXml.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Comparing XML", "In order to test my code\nAs a programmer\nI want to be able to test if two xml doc" +
                    "uments are equivalent", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Comparing matching XDocuments")]
        public virtual void ComparingMatchingXDocuments()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Comparing matching XDocuments", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "element",
                        "id",
                        "value"});
            table1.AddRow(new string[] {
                        "item",
                        "1",
                        "Item one"});
            table1.AddRow(new string[] {
                        "item",
                        "2",
                        "Item two"});
            table1.AddRow(new string[] {
                        "summary",
                        "3",
                        "Two items"});
#line 7
 testRunner.Given("I have a reference XDocument with root \"root\" and elements:", ((string)(null)), table1);
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "element",
                        "id",
                        "value"});
            table2.AddRow(new string[] {
                        "item",
                        "1",
                        "Item one"});
            table2.AddRow(new string[] {
                        "item",
                        "2",
                        "Item two"});
            table2.AddRow(new string[] {
                        "summary",
                        "3",
                        "Two items"});
#line 12
 testRunner.And("I have a test XDocument with root \"root\" and elements:", ((string)(null)), table2);
#line 17
 testRunner.When("I assert the test XDocument should be equivalent to the reference XDocument");
#line 18
 testRunner.Then("no exception should be thrown");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Comparing XDocuments with different content")]
        public virtual void ComparingXDocumentsWithDifferentContent()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Comparing XDocuments with different content", ((string[])(null)));
#line 20
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                        "element",
                        "id",
                        "value"});
            table3.AddRow(new string[] {
                        "item",
                        "1",
                        "Item one"});
            table3.AddRow(new string[] {
                        "item",
                        "2",
                        "Item two"});
            table3.AddRow(new string[] {
                        "summary",
                        "3",
                        "Two items"});
#line 21
 testRunner.Given("I have a reference XDocument with root \"root\" and elements:", ((string)(null)), table3);
#line hidden
            TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                        "element",
                        "id",
                        "value"});
            table4.AddRow(new string[] {
                        "item",
                        "1",
                        "Item one"});
            table4.AddRow(new string[] {
                        "item",
                        "2",
                        "Item different!"});
            table4.AddRow(new string[] {
                        "summary",
                        "3",
                        "Two items"});
#line 26
 testRunner.And("I have a test XDocument with root \"root\" and elements:", ((string)(null)), table4);
#line 31
 testRunner.When("I assert the test XDocument should be equivalent to the reference XDocument");
#line 32
 testRunner.Then("an AssertionException should be thrown");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#endregion
