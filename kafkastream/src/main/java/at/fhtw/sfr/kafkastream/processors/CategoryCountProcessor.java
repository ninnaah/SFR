package at.fhtw.sfr.kafkastream.processors;

import com.sfr.avro.ClothingAdAvro;
import org.apache.kafka.common.serialization.Serdes;
import org.apache.kafka.streams.StreamsBuilder;
import org.apache.kafka.streams.kstream.KStream;
import org.apache.kafka.streams.kstream.Materialized;
import org.apache.kafka.streams.kstream.Produced;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.stereotype.Component;

@Component
public class CategoryCountProcessor {

    @Value("${input.topic}")
    private String inputTopic;

    @Value("${output.topic}")
    private String outputTopic;

    // ML TODO: Tests schreiben,

    @Bean
    public KStream<String, Long> process(StreamsBuilder streamsBuilder) {
        KStream<String, ClothingAdAvro> stream = streamsBuilder.stream(inputTopic);

        KStream<String, Long> categoryCounts = stream
                .groupBy((key, value) -> value.getCategory().toString()) // Konvertierung zu String
                .count(Materialized.as("category-counts"))
                .toStream();

        categoryCounts.to(outputTopic, Produced.with(Serdes.String(), Serdes.Long()));

        return categoryCounts;
    }
}