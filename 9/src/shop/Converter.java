package shop;

import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.IOException;

public class Converter {
    private final static String baseFile = "product.json";
    public final static ObjectMapper kMainMapper = new ObjectMapper();

    public static <T> T toObject(String str, Class<T> clazz) throws IOException {
        return kMainMapper.readValue(str, clazz);
    }

    public static String toJSON(ProductData product) {
        try {
            return kMainMapper.writeValueAsString(product);
        } catch (Exception e) {
            e.printStackTrace();
            return "";
        }
    }

    private static <T> T print(T t) {
        System.out.println(t);
        return t;
    }

//    public static ProductData toJavaObject() throws IOException {
//        ObjectMapper mapper = new ObjectMapper();
//        return mapper.readValue(new File(baseFile), ProductData.class);
//    }

}
