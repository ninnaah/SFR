using Avro;
using Avro.Specific;

namespace SFR.AvroModels.V1
{
    public class ClothingAdAvro : ISpecificRecord
    {
        public static Schema _SCHEMA = Schema.Parse(
            @"{
                ""type"": ""record"",
                ""name"": ""ClothingAdAvro"",
                ""namespace"": ""SFR.AvroModels.V1"",
                ""fields"": [
                    { ""name"": ""id"", ""type"": ""string"" },
                    { ""name"": ""title"", ""type"": ""string"" },
                    { ""name"": ""description"", ""type"": ""string"" },
                    { ""name"": ""category"", ""type"": ""string"" },
                    { ""name"": ""condition"", ""type"": ""string"" },
                    { ""name"": ""size"", ""type"": ""string"" },
                    { ""name"": ""color"", ""type"": { ""type"": ""array"", ""items"": ""string"" } },
                    { ""name"": ""material"", ""type"": { ""type"": ""array"", ""items"": ""string"" } },
                    { ""name"": ""price"", ""type"": ""double"" },
                    { ""name"": ""currency"", ""type"": ""string"" },
                    { ""name"": ""location"", ""type"": ""string"" },
                    { ""name"": ""sellerId"", ""type"": ""string"" },
                    { ""name"": ""photoUrls"", ""type"": { ""type"": ""array"", ""items"": ""string"" } },
                    { ""name"": ""publishedAt"", ""type"": ""string"" },
                    { ""name"": ""source"", ""type"": ""string"" }
                ]
            }"
        );

        public virtual Schema Schema => _SCHEMA;

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Condition { get; set; }
        public string Size { get; set; }
        public IList<string> Color { get; set; }
        public IList<string> Material { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Location { get; set; }
        public string SellerId { get; set; }
        public IList<string> PhotoUrls { get; set; }
        public string PublishedAt { get; set; }
        public string Source { get; set; }

        public ClothingAdAvro()
        {
            Color = new List<string>();
            Material = new List<string>();
            PhotoUrls = new List<string>();
        }

        public virtual object Get(int fieldPos)
        {
            return fieldPos switch
            {
                0 => Id,
                1 => Title,
                2 => Description,
                3 => Category,
                4 => Condition,
                5 => Size,
                6 => Color,
                7 => Material,
                8 => Price,
                9 => Currency,
                10 => Location,
                11 => SellerId,
                12 => PhotoUrls,
                13 => PublishedAt,
                14 => Source,
                _ => throw new AvroRuntimeException("Bad index " + fieldPos)
            };
        }

        public virtual void Put(int fieldPos, object fieldValue)
        {
            switch (fieldPos)
            {
                case 0: Id = (string)fieldValue; break;
                case 1: Title = (string)fieldValue; break;
                case 2: Description = (string)fieldValue; break;
                case 3: Category = (string)fieldValue; break;
                case 4: Condition = (string)fieldValue; break;
                case 5: Size = (string)fieldValue; break;
                case 6: Color = (IList<string>)fieldValue; break;
                case 7: Material = (IList<string>)fieldValue; break;
                case 8: Price = (double)fieldValue; break;
                case 9: Currency = (string)fieldValue; break;
                case 10: Location = (string)fieldValue; break;
                case 11: SellerId = (string)fieldValue; break;
                case 12: PhotoUrls = (IList<string>)fieldValue; break;
                case 13: PublishedAt = (string)fieldValue; break;
                case 14: Source = (string)fieldValue; break;
                default: throw new AvroRuntimeException("Bad index " + fieldPos); 
            }
        }
    }
}