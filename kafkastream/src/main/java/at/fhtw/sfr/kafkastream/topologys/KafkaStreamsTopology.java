package at.fhtw.sfr.kafkastream.topologys;

import SFR.AvroModels.V1.ClothingAdAvro;
import at.fhtw.sfr.kafkastream.constants.Topics;
import at.fhtw.sfr.kafkastream.serdes.SerdesUtils;
import org.apache.kafka.common.serialization.Serdes;
import org.apache.kafka.common.utils.Bytes;
import org.apache.kafka.streams.StreamsBuilder;
import org.apache.kafka.streams.kstream.Consumed;
import org.apache.kafka.streams.kstream.Grouped;
import org.apache.kafka.streams.kstream.KStream;
import org.apache.kafka.streams.kstream.Materialized;
import org.apache.kafka.streams.kstream.Produced;
import org.apache.kafka.streams.state.KeyValueStore;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import static java.lang.ProcessBuilder.Redirect.to;

public class KafkaStreamsTopology {

    private static final Logger log = LoggerFactory.getLogger(KafkaStreamsTopology.class);

    public static KStream<String, ClothingAdAvro> topology(StreamsBuilder streamsBuilder) {

        log.info("Building Kafka Streams topology...");

        KStream<String, ClothingAdAvro> stream = streamsBuilder
                .stream(Topics.CLOTHING_AD_INPUT_TOPIC, Consumed.with(Serdes.String(), SerdesUtils.getValueSerdes()));

        stream
                .peek((key, value) -> log.info("🔥 RECEIVED: key={}, value={}", key, value))
                .groupBy(
                        (key, value) -> value.getCategory().toString(),
                        Grouped.with(Serdes.String(), SerdesUtils.getValueSerdes())
                )
                .count(
                        Materialized.<String, Long, KeyValueStore<Bytes, byte[]>>as("category-counts-store")
                                .withKeySerde(Serdes.String())
                                .withValueSerde(Serdes.Long())
                )
                .toStream()
                .to(Topics.CATEGORY_COUNT_OUTPUT_TOPIC, Produced.with(Serdes.String(), Serdes.Long()));
                //.to(Topics.CATEGORY_COUNT_OUTPUT_TOPIC, Produced.with(Serdes.String(), specificAvroSerde));

        log.info("Kafka Streams topology built successfully.");
        return stream;
    }

    private KafkaStreamsTopology() {}
}