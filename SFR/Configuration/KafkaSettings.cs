namespace SFR.Configuration
{
    public static class KafkaSettings
    {
        public const string BootstrapServers = "localhost:29092";
        public const string SchemaRegistryUrl = "http://localhost:8081";

        public const string ClothingAdAvroTopic = "clothing-ad-avro";
        public const string CategoryCountTopic = "category-counts";
        public const int DefaultMessageCount = 10;
    }
}