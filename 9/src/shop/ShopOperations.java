package shop;

import io.restassured.RestAssured;
import io.restassured.http.ContentType;
import io.restassured.response.Response;

import java.io.IOException;
import java.util.Arrays;
import java.util.List;

import static io.restassured.RestAssured.given;
import static io.restassured.RestAssured.when;

public class ShopOperations {
    private String baseUrl;
    private String basePath;

    public ShopOperations(String baseUrl, String basePath) {
        this.baseUrl = baseUrl;
        this.basePath = basePath;
        initAPI();
    }

    private void initAPI() {
        RestAssured.baseURI = baseUrl;
        RestAssured.basePath = basePath;
    }

//    private Response doGetRequest(String endpoint) {
//        RestAssured.defaultParser = Parser.JSON;
//
//        return
//                given().headers("Content-Type", ContentType.JSON, "Accept", ContentType.JSON).
//                        when().get(endpoint).
//                        then().contentType(ContentType.JSON).extract().response();
//    }
//    public static List<ProductData> getAllProducts() {
//        List<ProductData> products = new ArrayList<ProductData>();
//        Response response = doGetRequest("http://shop.qatl.ru/api/products");
//
//        List<ProductData> jsonResponse = response.jsonPath().getList("$");
//
//        System.out.println(jsonResponse.size());
//        System.out.println(jsonResponse);
//

//        return products;
//    }

    public List<ProductData> getProducts() throws IOException {
        Response response;
        String jsonAsString;

        response =
                when().
                        get("products").
                        then().
                        contentType(ContentType.JSON).  // check that the content type return from the API is JSON
                        extract().response(); // extract the response

        jsonAsString = response.asString();
        try {
            ProductData[] jsonAsArray =Converter.toObject(jsonAsString, ProductData[].class);
            return Arrays.asList(jsonAsArray);
        } catch (IOException e) {
            e.printStackTrace();
            throw e;
        }
    }

    public ApiResponse addProduct(ProductData product) {
        try {
            Response response = RestAssured.given()
                    .header("Content-Type", "application/json")
                    .body(Converter.toJSON(product))
                    .post("http://shop.qatl.ru/api/addproduct");
            print(response.getBody().asString());

            if (response.getStatusCode() >= 300)
                return new ApiResponse(-1, false);
            return Converter.toObject(response.asString(), ApiResponse.class);
        } catch (Exception e) {
            return new ApiResponse(-1, false);
        }
    }

    public ApiResponse deleteProduct(int productId) {
        try {
            Response response = RestAssured.given()
                    .header("Content-Type", "application/json")
                    .delete(String.format("deleteproduct?id=%s", productId));
            print(response.getBody().asString());

            if (response.getStatusCode() >= 300)
                return new ApiResponse(-1, false);
            return Converter.toObject(response.asString(), ApiResponse.class);
        } catch (Exception e) {
            return new ApiResponse(-1, false);
        }
    }

    public ProductData getProductWithID(String id) throws IOException {
        return getProducts().stream().filter(productData -> productData.id == id).findFirst().get();
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
