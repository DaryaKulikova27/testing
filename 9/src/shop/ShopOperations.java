package shop;

import io.restassured.RestAssured;
import io.restassured.http.ContentType;
import io.restassured.response.Response;

import java.io.IOException;
import java.util.Arrays;
import java.util.List;
import java.util.Objects;

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

    private static <T> T print(T t) {
        System.out.println(t);
        return t;
    }

    private static <T> T[] print(T... t) {
        System.out.println(Arrays.toString(t));
        return t;
    }

    private void initAPI() {
        RestAssured.baseURI = baseUrl;
        RestAssured.basePath = basePath;
    }

    public List<ProductData> getProducts() throws IOException {
        Response response;
        String jsonAsString;

        response = when().get("products").then().contentType(ContentType.JSON).extract().response();

        jsonAsString = response.asString();
        try {
            ProductData[] jsonAsArray = Converter.toObject(jsonAsString, ProductData[].class);
            return Arrays.asList(jsonAsArray);
        } catch (IOException e) {
            e.printStackTrace();
            throw e;
        }
    }

    public ApiResponse addProduct(ProductData product) {
        try {
            Response response = given().header("Content-Type", "application/json").body(Converter.toJSON(product)).post("addproduct");
            print(response.getBody().asString());

            if (response.getStatusCode() >= 300) return new ApiResponse(-1, false);
            return Converter.toObject(response.asString(), ApiResponse.class);
        } catch (Exception e) {
            return new ApiResponse(-1, false);
        }
    }

    public ApiResponse updateProduct(ProductData product) {
        try {
            Response response = given().header("Content-Type", "application/json").body(Converter.toJSON(product)).post("editproduct");
            print(response.getBody().asString());

            if (response.getStatusCode() >= 300) return new ApiResponse(-1, false);
            return Converter.toObject(response.asString(), ApiResponse.class);
        } catch (Exception e) {
            return new ApiResponse(-1, false);
        }
    }

    public ApiResponse deleteProduct(int productId) {
        try {
            Response response = given().header("Content-Type", "application/json").delete(String.format("deleteproduct?id=%s", productId));
            print(response.getBody().asString());

            if (response.getStatusCode() >= 300) return new ApiResponse(-1, false);
            return Converter.toObject(response.asString(), ApiResponse.class);
        } catch (Exception e) {
            return new ApiResponse(-1, false);
        }
    }

    // Перенести
}
