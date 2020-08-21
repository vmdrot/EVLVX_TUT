namespace VSUsagesAnalysisHelperLib.NugetSimple
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PackageV3Info
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("@type")]
        public string[] Type { get; set; }

        [JsonProperty("commitId")]
        public Guid CommitId { get; set; }

        [JsonProperty("commitTimeStamp")]
        public DateTimeOffset CommitTimeStamp { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("items")]
        public PackageV3InfoItem[] Items { get; set; }

        [JsonProperty("@context")]
        public Context Context { get; set; }
    }

    public partial class Context
    {
        [JsonProperty("@vocab")]
        public Uri Vocab { get; set; }

        [JsonProperty("catalog")]
        public Uri Catalog { get; set; }

        [JsonProperty("xsd")]
        public Uri Xsd { get; set; }

        [JsonProperty("items")]
        public Dependencies Items { get; set; }

        [JsonProperty("commitTimeStamp")]
        public CommitTimeStamp CommitTimeStamp { get; set; }

        [JsonProperty("commitId")]
        public CommitId CommitId { get; set; }

        [JsonProperty("count")]
        public CommitId Count { get; set; }

        [JsonProperty("parent")]
        public CommitTimeStamp Parent { get; set; }

        [JsonProperty("tags")]
        public Dependencies Tags { get; set; }

        [JsonProperty("packageTargetFrameworks")]
        public Dependencies PackageTargetFrameworks { get; set; }

        [JsonProperty("dependencyGroups")]
        public Dependencies DependencyGroups { get; set; }

        [JsonProperty("dependencies")]
        public Dependencies Dependencies { get; set; }

        [JsonProperty("packageContent")]
        public PackageContent PackageContent { get; set; }

        [JsonProperty("published")]
        public PackageContent Published { get; set; }

        [JsonProperty("registration")]
        public PackageContent Registration { get; set; }
    }

    public partial class CommitId
    {
        [JsonProperty("@id")]
        public string Id { get; set; }
    }

    public partial class CommitTimeStamp
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
    }

    public partial class Dependencies
    {
        [JsonProperty("@container")]
        public string Container { get; set; }

        [JsonProperty("@id")]
        public string Id { get; set; }
    }

    public partial class PackageContent
    {
        [JsonProperty("@type")]
        public string Type { get; set; }
    }

    public partial class PackageV3InfoItem
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("commitId")]
        public Guid CommitId { get; set; }

        [JsonProperty("commitTimeStamp")]
        public DateTimeOffset CommitTimeStamp { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("items")]
        public ItemItem[] Items { get; set; }

        [JsonProperty("parent")]
        public Uri Parent { get; set; }

        [JsonProperty("lower")]
        public string Lower { get; set; }

        [JsonProperty("upper")]
        public string Upper { get; set; }
    }

    public partial class ItemItem
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("commitId")]
        public Guid CommitId { get; set; }

        [JsonProperty("commitTimeStamp")]
        public DateTimeOffset CommitTimeStamp { get; set; }

        [JsonProperty("catalogEntry")]
        public CatalogEntry CatalogEntry { get; set; }

        [JsonProperty("packageContent")]
        public Uri PackageContent { get; set; }

        [JsonProperty("registration")]
        public Uri Registration { get; set; }
    }

    public partial class CatalogEntry
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("authors")]
        public string Authors { get; set; }

        [JsonProperty("dependencyGroups", NullValueHandling = NullValueHandling.Ignore)]
        public DependencyGroup[] DependencyGroups { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }

        [JsonProperty("id")]
        public string CatalogEntryId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("licenseUrl")]
        public Uri LicenseUrl { get; set; }

        [JsonProperty("listed")]
        public bool Listed { get; set; }

        [JsonProperty("minClientVersion")]
        public string MinClientVersion { get; set; }

        [JsonProperty("packageContent")]
        public Uri PackageContent { get; set; }

        [JsonProperty("projectUrl")]
        public Uri ProjectUrl { get; set; }

        [JsonProperty("published")]
        public DateTimeOffset Published { get; set; }

        [JsonProperty("requireLicenseAcceptance")]
        public bool RequireLicenseAcceptance { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public partial class DependencyGroup
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("dependencies")]
        public Dependency[] Dependencies { get; set; }
    }

    public partial class Dependency
    {
        [JsonProperty("@id")]
        public Uri Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string DependencyId { get; set; }

        [JsonProperty("range")]
        public string Range { get; set; }

        [JsonProperty("registration")]
        public Uri Registration { get; set; }
    }

    //public enum Authors { AndrewArnott };

    //public enum Id { DotNetOpenAuthUltimate };

    //public enum Language { EnUs };

    //public enum Title { DotNetOpenAuthUltimate, DotNetOpenAuthUnified };

    //public enum CatalogEntryType { PackageDetails };

    //public enum ItemType { Package };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                //ItemTypeConverter.Singleton,
                //CatalogEntryTypeConverter.Singleton,
                //AuthorsConverter.Singleton,
                //IdConverter.Singleton,
                //LanguageConverter.Singleton,
                //TitleConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    //internal class ItemTypeConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(ItemType) || t == typeof(ItemType?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        if (value == "Package")
    //        {
    //            return ItemType.Package;
    //        }
    //        throw new Exception("Cannot unmarshal type ItemType");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (ItemType)untypedValue;
    //        if (value == ItemType.Package)
    //        {
    //            serializer.Serialize(writer, "Package");
    //            return;
    //        }
    //        throw new Exception("Cannot marshal type ItemType");
    //    }

    //    public static readonly ItemTypeConverter Singleton = new ItemTypeConverter();
    //}

    //internal class CatalogEntryTypeConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(CatalogEntryType) || t == typeof(CatalogEntryType?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        if (value == "PackageDetails")
    //        {
    //            return CatalogEntryType.PackageDetails;
    //        }
    //        throw new Exception("Cannot unmarshal type CatalogEntryType");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (CatalogEntryType)untypedValue;
    //        if (value == CatalogEntryType.PackageDetails)
    //        {
    //            serializer.Serialize(writer, "PackageDetails");
    //            return;
    //        }
    //        throw new Exception("Cannot marshal type CatalogEntryType");
    //    }

    //    public static readonly CatalogEntryTypeConverter Singleton = new CatalogEntryTypeConverter();
    //}

    //internal class AuthorsConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(Authors) || t == typeof(Authors?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        if (value == "Andrew Arnott")
    //        {
    //            return Authors.AndrewArnott;
    //        }
    //        throw new Exception("Cannot unmarshal type Authors");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (Authors)untypedValue;
    //        if (value == Authors.AndrewArnott)
    //        {
    //            serializer.Serialize(writer, "Andrew Arnott");
    //            return;
    //        }
    //        throw new Exception("Cannot marshal type Authors");
    //    }

    //    public static readonly AuthorsConverter Singleton = new AuthorsConverter();
    //}

    //internal class IdConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(Id) || t == typeof(Id?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        if (value == "DotNetOpenAuth.Ultimate")
    //        {
    //            return Id.DotNetOpenAuthUltimate;
    //        }
    //        throw new Exception("Cannot unmarshal type Id");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (Id)untypedValue;
    //        if (value == Id.DotNetOpenAuthUltimate)
    //        {
    //            serializer.Serialize(writer, "DotNetOpenAuth.Ultimate");
    //            return;
    //        }
    //        throw new Exception("Cannot marshal type Id");
    //    }

    //    public static readonly IdConverter Singleton = new IdConverter();
    //}

    //internal class LanguageConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(Language) || t == typeof(Language?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        if (value == "en-US")
    //        {
    //            return Language.EnUs;
    //        }
    //        throw new Exception("Cannot unmarshal type Language");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (Language)untypedValue;
    //        if (value == Language.EnUs)
    //        {
    //            serializer.Serialize(writer, "en-US");
    //            return;
    //        }
    //        throw new Exception("Cannot marshal type Language");
    //    }

    //    public static readonly LanguageConverter Singleton = new LanguageConverter();
    //}

    //internal class TitleConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(Title) || t == typeof(Title?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        switch (value)
    //        {
    //            case "DotNetOpenAuth (unified)":
    //                return Title.DotNetOpenAuthUnified;
    //            case "DotNetOpenAuth Ultimate":
    //                return Title.DotNetOpenAuthUltimate;
    //        }
    //        throw new Exception("Cannot unmarshal type Title");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (Title)untypedValue;
    //        switch (value)
    //        {
    //            case Title.DotNetOpenAuthUnified:
    //                serializer.Serialize(writer, "DotNetOpenAuth (unified)");
    //                return;
    //            case Title.DotNetOpenAuthUltimate:
    //                serializer.Serialize(writer, "DotNetOpenAuth Ultimate");
    //                return;
    //        }
    //        throw new Exception("Cannot marshal type Title");
    //    }

    //    public static readonly TitleConverter Singleton = new TitleConverter();
    //}
}
