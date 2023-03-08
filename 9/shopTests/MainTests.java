import com.fasterxml.jackson.core.type.TypeReference;
import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import shop.*;

import java.io.IOException;
import java.util.*;

public class MainTests extends Assert {
    ShopOperations service;
    ArrayList<Integer> createdIDs = new ArrayList<>();

    Map<String, ProductData> testProducts;



    @Before
    public void init() throws IOException {
        service = getShopOperations();
        TypeReference<HashMap<String, ProductData>> typeRef
                = new TypeReference<HashMap<String, ProductData>>() {};
        testProducts = Converter.kMainMapper.readValue(Main.class.getResourceAsStream("testobjects.json"), typeRef);
    }

    public ProductData getProductWithID(String id) throws IOException {
        return service.getProducts().stream().filter(productData -> Objects.equals(productData.id, id)).findFirst().orElse(null);
    }

    private static ShopOperations getShopOperations() throws java.io.IOException {
        APIConfig apiConfig = Converter.kMainMapper.readValue(Main.class.getResourceAsStream("configFile.json"), APIConfig.class);
        return new ShopOperations(apiConfig.baseUri, apiConfig.basePath);
    }

    @Test
    public void shouldListAllProducts() throws IOException {
        List<ProductData> products = service.getProducts();
        assertNotNull("Список продуктов пуст", products);
    }

    @Test
    public void shouldAddProduct() throws IOException {
        ProductData addedProductExpected = testProducts.get("valid_product_1");
        ApiResponse response = service.addProduct(addedProductExpected);
        createdIDs.add(response.id);
        ProductData addedProduct = getProductWithID(String.valueOf(response.id));

        assertTrue("Ошибка на сервере", response.id != -1);
        assertNotNull("Продукт не был создан", addedProduct);
        assertTrue("Продукт был изменен", addedProductExpected.contentEquals(addedProduct));
    }

    @Test
    public void shouldAddFewProducts() throws IOException {
        ProductData addedProduct2Expected = testProducts.get("valid_product_2");
        ProductData addedProduct3Expected = testProducts.get("valid_product_3");
        ApiResponse response2 = service.addProduct(addedProduct2Expected);
        ApiResponse response3 = service.addProduct(addedProduct3Expected);
        createdIDs.add(response2.id);
        createdIDs.add(response3.id);
        ProductData addedProduct2 = getProductWithID(String.valueOf(response2.id));
        ProductData addedProduct3 = getProductWithID(String.valueOf(response3.id));

        assertTrue("Ошибка на сервере", response2.id != -1);
        assertNotNull("Продукт не был создан", addedProduct2);
        assertTrue("Alias продукта отличается от title", addedProduct2.alias.equalsIgnoreCase(addedProduct2.title));
        assertTrue("Продукт был изменен", addedProduct2Expected.contentEquals(addedProduct2));

        assertTrue("Ошибка на сервере", response3.id != -1);
        assertNotNull("Продукт не был создан", addedProduct3);
        assertTrue("Alias продукта отличается от title", addedProduct3.alias.equalsIgnoreCase(addedProduct3.title));
        assertTrue("Продукт был изменен", addedProduct3Expected.contentEquals(addedProduct3));
    }

    @Test
    public void shouldChangeProductAlias() throws IOException {
        ProductData addedProduct2Expected = testProducts.get("valid_product_1");
        ProductData addedProduct3Expected = testProducts.get("valid_product_1");
        ApiResponse response2 = service.addProduct(addedProduct2Expected);
        ApiResponse response3 = service.addProduct(addedProduct3Expected);
        createdIDs.add(response2.id);
        createdIDs.add(response3.id);
        ProductData addedProduct2 = getProductWithID(String.valueOf(response2.id));
        ProductData addedProduct3 = getProductWithID(String.valueOf(response3.id));

        assertTrue("Ошибка на сервере", response2.id != -1);
        assertNotNull("Продукт не был создан", addedProduct2);

        assertTrue("Ошибка на сервере", response3.id != -1);
        assertNotNull("Продукт не был создан", addedProduct3);

        assertTrue("Alias продукта не отличается на -0", addedProduct3.alias.equalsIgnoreCase(addedProduct2.alias + "-0"));
    }

    @Test
    public void shouldNotAddInvalidProduct() throws IOException {
        ProductData addedProductExpected = testProducts.get("invalid_product");
        ApiResponse response = service.addProduct(addedProductExpected);
        ProductData addedProduct = getProductWithID(String.valueOf(response.id));

        // Порядок проверок изменить
        assertEquals("Ошибка на сервере", -1, response.id);
        assertFalse("Сервер вернул неверный ответ", response.status);
        assertNull("Продукт был создан", addedProduct);
    }

    @Test
    public void shouldGetProductById() throws IOException {
        ProductData addedProductExpected = testProducts.get("valid_product_1");
        ApiResponse response = service.addProduct(addedProductExpected);
        ProductData addedProduct = getProductWithID(String.valueOf(response.id));
        createdIDs.add(response.id);
        assertNotNull("Продукт не найден", addedProduct);
        assertTrue("Продукты не совпадают", addedProductExpected.contentEquals(addedProduct));
    }

    @Test
    public void shouldNotAddNullProduct() throws IOException {
        ProductData addedProduct = testProducts.get("null_product");
        ApiResponse response = service.addProduct(addedProduct);
        ProductData resultProduct = getProductWithID(String.valueOf(response.id));
        assertNull("Продукт был создан", resultProduct);
        assertFalse("Сервер вернул статус 1", response.status);
        assertEquals("Ошибка сервера", -1, response.id);
    }

    @Test
    public void shouldUpdate() throws IOException {
        ProductData oldProduct = testProducts.get("valid_product_1");
        ProductData newProduct = testProducts.get("valid_update_product_1");

        ApiResponse createResponse = service.addProduct(oldProduct);

        ProductData createdProduct = getProductWithID(String.valueOf(createResponse.id));
        newProduct.id = createdProduct.id;
        newProduct.alias = createdProduct.alias;


        // Наименование теста одно, в тесте делается другое, само проверяемое действие не проверяется
        ApiResponse updateResponse = service.updateProduct(newProduct);
        createdIDs.add(updateResponse.id);

        ProductData updatedProduct = getProductWithID(String.valueOf(createResponse.id));

        assertTrue("Сервер вернул статус 0", createResponse.status);
        assertTrue("Обновлённый продукт отличается от заданного", updatedProduct.contentEquals(newProduct));
    }

    @Test
    public void shouldFailUpdatingWithInvalidProduct() throws IOException {
        ProductData oldProduct = testProducts.get("valid_product_1");
        ProductData newProduct = testProducts.get("invalid_update_product");

        ApiResponse createResponse = service.addProduct(oldProduct);

        ProductData createdProduct = getProductWithID(String.valueOf(createResponse.id));
        newProduct.id = createdProduct.id;
        newProduct.alias = createdProduct.alias;

        ApiResponse updateResponse = service.updateProduct(newProduct);
        createdIDs.add(createResponse.id);

        ProductData updatedProduct = getProductWithID(String.valueOf(createResponse.id));

        assertNotNull("Сервер вернул статус 1", updatedProduct);
        assertFalse("Сервер вернул статус 1", updateResponse.status);
        assertTrue("Продукт изменился", updatedProduct.contentEquals(oldProduct));
        assertTrue("Alias изменился", updatedProduct.alias.equalsIgnoreCase(createdProduct.alias));
    }

    @Test
    public void shouldDelete() throws IOException {
        ProductData addedProductExpected = testProducts.get("valid_product_1");
        ApiResponse responseCreate = service.addProduct(addedProductExpected);
        createdIDs.add(responseCreate.id);

        ApiResponse response = service.deleteProduct(responseCreate.id);

        ProductData addedProduct = getProductWithID(String.valueOf(responseCreate.id));
        assertTrue("Сервер вернул статус 0", response.status);
        assertNull("Продукт не был удалён", addedProduct);
    }

    @Test
    public void shouldDoNothingDeletingInvalidId() {
        ApiResponse response = service.deleteProduct(-1);
        assertFalse("Сервер вернул статус 1", response.status);
    }

    @After
    public void deleteAllCreated() {
        createdIDs.forEach(service::deleteProduct);
    }
}
