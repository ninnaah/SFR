package at.fhtw.sfr.kafkastream.config;

import at.fhtw.sfr.kafkastream.constants.Topics;
import at.fhtw.sfr.kafkastream.topologys.KafkaStreamsTopology;
import com.sfr.avro.ClothingAdAvro;
import org.apache.kafka.streams.StreamsBuilder;
import org.apache.kafka.streams.kstream.KStream;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.kafka.annotation.EnableKafkaStreams;

@Configuration
@EnableKafkaStreams
public class KafkaStreamsConfig {

    @Bean
    public KStream<String, ClothingAdAvro> kStream(StreamsBuilder streamsBuilder) {
        return KafkaStreamsTopology.topology(streamsBuilder);
    }
}