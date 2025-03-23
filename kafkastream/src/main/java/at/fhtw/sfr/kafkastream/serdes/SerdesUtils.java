package at.fhtw.sfr.kafkastream.serdes;

import io.confluent.kafka.streams.serdes.avro.SpecificAvroSerde;
import org.apache.avro.specific.SpecificRecord;

import java.util.Map;

/** Utility class for Serdes. */
public class SerdesUtils {

    private static Map<String, String> serdesConfig;

    /**
     * Create a SpecificAvroSerde for the value.
     *
     * @param <T> The type of the value.
     * @return The SpecificAvroSerde.
     */
    public static <T extends SpecificRecord> SpecificAvroSerde<T> getValueSerdes() {
        SpecificAvroSerde<T> serdes = new SpecificAvroSerde<>();
        serdes.configure(serdesConfig, false);
        return serdes;
    }

    public static void setSerdesConfig(Map<String, String> config) {
        serdesConfig = config;
    }

    /** Private constructor. */
    private SerdesUtils() {}
}
