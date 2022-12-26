import shop.*;

import java.io.IOException;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.stream.Collectors;


public class Main {
    public static void main(String[] args) throws IOException {
        ShopOperations shopOperations = getShopOperations();
        List<ProductData> products = shopOperations.getProducts();
        print(products.stream().map(Object::toString)
                .collect(Collectors.joining("\n")));

        ProductData newProduct;
        newProduct = new ProductData("1", "2", "EXAMPLE-2", "example-1", "content",
                "23113", "34113", "status", "keywords-example", "description", "0");
        ApiResponse addedProduct = shopOperations.addProduct(newProduct);
        print("Added product", addedProduct.status);

        List<ProductData> products1 = shopOperations.getProducts();
        print(products1.stream().map(Object::toString)
                .collect(Collectors.joining("\n")));
        print("Find product", shopOperations.getProductWithID(String.valueOf(addedProduct.id)));
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