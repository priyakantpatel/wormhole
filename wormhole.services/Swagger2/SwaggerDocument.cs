using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace wormhole.services.Swagger2
{
    public class SwaggerDocument
    {
        public SwaggerDocument()
        {
            Extensions = new Dictionary<string, object>();
        }

        public string Swagger
        {
            get { return "2.0"; }
        }

        public Info Info { get; set; }

        public string Host { get; set; }

        public string BasePath { get; set; }

        public List<string> Schemes { get; set; }

        public List<string> Consumes { get; set; }

        public List<string> Produces { get; set; }

        public Dictionary<string, PathItem> Paths { get; set; }

        public Dictionary<string, Schema> Definitions { get; set; }

        public Dictionary<string, NonBodyParameter> Parameters { get; set; }

        public Dictionary<string, Response> Responses { get; set; }

        public Dictionary<string, SecurityScheme> SecurityDefinitions { get; set; }

        public List<Dictionary<string, List<string>>> Security { get; set; }

        public List<Tag> Tags { get; set; }

        public ExternalDocs ExternalDocs { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class Info
    {
        public Info()
        {
            Extensions = new Dictionary<string, object>();
        }

        public string Version { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string TermsOfService { get; set; }

        public Contact Contact { get; set; }

        public License License { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class Contact
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Email { get; set; }
    }

    public class License
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }

    public class PathItem
    {
        public PathItem()
        {
            Extensions = new Dictionary<string, object>();
        }

        [JsonProperty("$ref")]
        public string Ref { get; set; }

        public Operation Get { get; set; }

        public Operation Put { get; set; }

        public Operation Post { get; set; }

        public Operation Delete { get; set; }

        public Operation Options { get; set; }

        public Operation Head { get; set; }

        public Operation Patch { get; set; }

        public List<NonBodyParameter> Parameters { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class Operation
    {
        public Operation()
        {
            Extensions = new Dictionary<string, object>();
        }

        public List<string> Tags { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public ExternalDocs ExternalDocs { get; set; }

        public string OperationId { get; set; }

        public List<string> Consumes { get; set; }

        public List<string> Produces { get; set; }

        public List<NonBodyParameter> Parameters { get; set; }

        public Dictionary<string, Response> Responses { get; set; }

        public List<string> Schemes { get; set; }

        public bool? Deprecated { get; set; }

        public List<Dictionary<string, List<string>>> Security { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class Tag
    {
        public Tag()
        {
            Extensions = new Dictionary<string, object>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public ExternalDocs ExternalDocs { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class ExternalDocs
    {
        public string Description { get; set; }

        public string Url { get; set; }
    }


    public interface IParameter
    {
        string Name { get; set; }

        string In { get; set; }

        string Description { get; set; }

        bool Required { get; set; }

        Dictionary<string, object> Extensions { get; }
    }

    public class NonBodyParameter : PartialSchema, IParameter
    {
        public NonBodyParameter()
        {
            Extensions = new Dictionary<string, object>();
        }

        public string Name { get; set; }

        public string In { get; set; }

        public string Description { get; set; }

        public bool Required { get; set; }

        [JsonProperty("$ref")]
        public string Ref { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class Schema
    {
        public Schema()
        {
            Extensions = new Dictionary<string, object>();
        }

        [JsonProperty("$ref")]
        public string Ref { get; set; }

        public string Format { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public object Default { get; set; }

        public int? MultipleOf { get; set; }

        public int? Maximum { get; set; }

        public bool? ExclusiveMaximum { get; set; }

        public int? Minimum { get; set; }

        public bool? ExclusiveMinimum { get; set; }

        public int? MaxLength { get; set; }

        public int? MinLength { get; set; }

        public string Pattern { get; set; }

        public int? MaxItems { get; set; }

        public int? MinItems { get; set; }

        public bool? UniqueItems { get; set; }

        public int? MaxProperties { get; set; }

        public int? MinProperties { get; set; }

        public List<string> Required { get; set; }

        public List<object> Enum { get; set; }

        public string Type { get; set; }

        public Schema Items { get; set; }

        public List<Schema> AllOf { get; set; }

        public Dictionary<string, Schema> Properties { get; set; }

        public Schema AdditionalProperties { get; set; }

        public string Discriminator { get; set; }

        public bool? ReadOnly { get; set; }

        public Xml Xml { get; set; }

        public ExternalDocs ExternalDocs { get; set; }

        public object Example { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class PartialSchema
    {
        public string Type { get; set; }

        public string Format { get; set; }

        public PartialSchema Items { get; set; }

        public string CollectionFormat { get; set; }

        public object Default { get; set; }

        public int? Maximum { get; set; }

        public bool? ExclusiveMaximum { get; set; }

        public int? Minimum { get; set; }

        public bool? ExclusiveMinimum { get; set; }

        public int? MaxLength { get; set; }

        public int? MinLength { get; set; }

        public string Pattern { get; set; }

        public int? MaxItems { get; set; }

        public int? MinItems { get; set; }

        public bool? UniqueItems { get; set; }

        public List<object> Enum { get; set; }

        public int? MultipleOf { get; set; }
    }

    public class Response
    {
        public string Description { get; set; }

        public Schema Schema { get; set; }

        public Dictionary<string, Header> Headers { get; set; }

        public object Examples { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }

    public class Header : PartialSchema
    {
        public string Description { get; set; }
    }

    public class Xml
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public string Prefix { get; set; }

        public bool? Attribute { get; set; }

        public bool? Wrapped { get; set; }
    }

    public class SecurityScheme
    {
        public SecurityScheme()
        {
            Extensions = new Dictionary<string, object>();
        }

        public string Type { get; set; }

        public string Description { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Extensions { get; private set; }
    }
}
