package at.fhtw.sfr.kafkastream.topologys;

import SFR.AvroModels.V1.ClothingAdAvro;
import at.fhtw.sfr.kafkastream.constants.Topics;
import at.fhtw.sfr.kafkastream.serdes.SerdesUtils;
import io.confluent.kafka.streams.serdes.avro.SpecificAvroSerde;
import org.apache.kafka.common.serialization.Serdes;
import org.apache.kafka.streams.StreamsBuilder;
import org.apache.kafka.streams.StreamsConfig;
import org.apache.kafka.streams.TopologyTestDriver;
import org.apache.kafka.streams.TestInputTopic;
import org.apache.kafka.streams.TestOutputTopic;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.junit.jupiter.MockitoExtension;

import java.util.List;
import java.util.Map;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.*;
@ExtendWith(MockitoExtension.class)
class KafkaStreamsTopologyTest {

    private TopologyTestDriver testDriver;
    public TestInputTopic<String, ClothingAdAvro> inputTopic;
    private TestOutputTopic<String, Long> outputTopic;

    @BeforeEach
    void setup() {

        SerdesUtils.setSerdesConfig(Map.of("schema.registry.url", "mock://"));

        StreamsBuilder builder = new StreamsBuilder();
        KafkaStreamsTopology.topology(builder);

        Properties props = new Properties();
        props.put(StreamsConfig.APPLICATION_ID_CONFIG, "test-app");
        props.put(StreamsConfig.BOOTSTRAP_SERVERS_CONFIG, "dummy:9092");
        props.put(StreamsConfig.DEFAULT_KEY_SERDE_CLASS_CONFIG, Serdes.String().getClass());
        props.put(StreamsConfig.DEFAULT_VALUE_SERDE_CLASS_CONFIG, SpecificAvroSerde.class);
        props.put("schema.registry.url", "mock://");

        testDriver = new TopologyTestDriver(builder.build(), props);

        Map<String, String> serdeConfig = Map.of("schema.registry.url", "mock://");

        SpecificAvroSerde<ClothingAdAvro> avroSerde = new SpecificAvroSerde<>();
        avroSerde.configure(serdeConfig, false);

        inputTopic = testDriver.createInputTopic(
                Topics.CLOTHING_AD_INPUT_TOPIC,
                Serdes.String().serializer(),
                avroSerde.serializer()
        );

        outputTopic = testDriver.createOutputTopic(
                Topics.CATEGORY_COUNT_OUTPUT_TOPIC,
                Serdes.String().deserializer(),
                Serdes.Long().deserializer()
        );
    }

    @AfterEach
    void tearDown() {
        testDriver.close();
    }

    @Test
    void shouldCountCategories() {
        ClothingAdAvro ad1 = buildTestAd("1", "Nike Shirt", "T-Shirts");
        ClothingAdAvro ad2 = buildTestAd("2", "Adidas Shirt", "T-Shirts");

        inputTopic.pipeInput("key1", ad1);
        inputTopic.pipeInput("key2", ad2);

        var result1 = outputTopic.readKeyValue();
        var result2 = outputTopic.readKeyValue();

        assertEquals("T-Shirts", result1.key);
        assertEquals(1L, result1.value);

        assertEquals("T-Shirts", result2.key);
        assertEquals(2L, result2.value);
    }

    private ClothingAdAvro buildTestAd(String id, String title, String category) {
        ClothingAdAvro ad = new ClothingAdAvro();
        ad.setId(id);
        ad.setTitle(title);
        ad.setDescription("Testbeschreibung");
        ad.setCategory(category);
        ad.setCondition("Neu");
        ad.setSize("M");
        ad.setColor(List.of("Blau"));
        ad.setMaterial(List.of("Baumwolle"));
        ad.setPrice(19.99);
        ad.setCurrency("EUR");
        ad.setLocation("Wien");
        ad.setSellerId("seller-123");
        ad.setPhotoUrls(List.of("https://example.com/image.jpg"));
        ad.setPublishedAt("2025-03-24T00:00:00Z");
        ad.setSource("Test");
        return ad;
    }
}