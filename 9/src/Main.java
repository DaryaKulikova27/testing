import com.fasterxml.jackson.databind.ObjectMapper;
import shop.*;

import java.io.IOException;
import java.util.Arrays;


public class Main {
    public static void main(String[] args) throws IOException {
        ShopOperations shopOperations = getShopOperations();
        print(shopOperations.getProducts());

        ProductData newProduct;
        newProduct = new ProductData("1", "2", "EXAMPLE", "example-1", "content",
                "23113", "34113", "status", "keywords-example", "description", "0");
        ApiResponse addedProduct = shopOperations.addProduct(newProduct);
        print("Added product", addedProduct.status);
        print("Deleted product", shopOperations.deleteProduct(addedProduct.id).status);
    }

    private static ShopOperations getShopOperations() throws IOException {
        APIConfig apiConfig = Converter.kMainMapper.readValue(Main.class.getResourceAsStream("configFile.json"), APIConfig.class);
        return new ShopOperations(apiConfig.baseUri, apiConfig.basePath);
    }

    private static <T> T print(T t) {
        System.out.println(t);
        return t;
    }

    private static <T> T[] print(T... t) {
        System.out.println(Arrays.toString(t));
        return t;
    }
}