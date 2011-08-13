#Minotaur - *XML Assertions Library for .Net*

##Elevator pitch
You're working on a project. You've got lots of XML flying around. You're a good TDD/BDD developer and so you want to test that you're generating the right XML. 

**BUT** testing XML in .Net can be frustrating. You can't just compare strings as they might have different insignificant whitespace, different ordering of attributes or even different encodings. And trying to compare parsed XML documents can add ugly code complexity into your tests, hiding the actual assertions you're trying to make.

Wouldn't it be better to be able to say

    ```C#
	// actualXml & expectedXml can be strings / XDocuments / XmlNodes...
	actualXml.ShouldBeEquivalentTo(expectedXml)
	```
	
Or,

    ```C#
	// Notice nice fluent XPath style syntax
	
	// Third paragraph should have 'quote' style
	actualXml.x("document").x("para")[3].x("style").ShouldEqual("quote");
	
	// There should be no 'ul' elements without 'li' children
	actualXml.elements("ul").where("not(.//li)").ShouldBeEmpty();
	```
	
Huh?

## Current status

Still in very early alpha development.

Get in touch if you have suggestions for syntax or assertions you'd like included.