package com.sfr.streams;

import org.apache.kafka.common.serialization.Serdes;
import org.apache.kafka.streams.*;
import org.apache.kafka.streams.kstream.*;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.Properties;

public class StreamProcessorApp {

    /**
     * Fragen:
     * Wo sollte man das Objekt mappen?
     * Wo erstelle ich mir die neuen Objekte?
     * AVRO verwenden, damit mans mal gesehen hat oder ist json auch in Ordnung? -> AVRO nutzen
     *
     */

    // DOKU: https://kafka.apache.org/documentation/streams/
    private static final Logger logger = LoggerFactory.getLogger(StreamProcessorApp.class);

    public static void main(String[] args) {
        Properties props = new Properties();
        props.put(StreamsConfig.APPLICATION_ID_CONFIG, "auto-key-aggregator");
        props.put(StreamsConfig.BOOTSTRAP_SERVERS_CONFIG, "localhost:29092");
        props.put(StreamsConfig.DEFAULT_KEY_SERDE_CLASS_CONFIG, Serdes.String().getClass());
        props.put(StreamsConfig.DEFAULT_VALUE_SERDE_CLASS_CONFIG, Serdes.String().getClass());

        StreamsBuilder builder = new StreamsBuilder();

        KStream<String, String> inputStream = builder.stream("combined-items");

        KTable<String, Long> countPerKey = inputStream
                .groupByKey()
                .count();

        countPerKey.toStream().to("combined-items-counts", Produced.with(Serdes.String(), Serdes.Long()));

        KafkaStreams streams = new KafkaStreams(builder.build(), props);

        streams.start();
        logger.info("Auto-Key Aggregator running...");

        // Shutdown hook zum sauberen Schließen
        Runtime.getRuntime().addShutdownHook(new Thread(() -> {
            logger.info("Shutting down stream...");
            streams.close();
        }));
    }
}